
using Microsoft.Extensions.Options;
using System.Text;
using System.Net.Http.Headers;
using System.Text.Json;

using Getnet.Infrastrucutre.Configurations.Getnet;
using Getnet.Services.Interfaces;
using Getnet.Exceptions;
using Getnet.Entities.Request;
using Getnet.Entities.Response;
using Getnet.Entities.Commom;

namespace Getnet.Services;

public class GetnetService : IGetnetService
{
    #region Propriedades

    private readonly HttpClient _httpClient;
    private readonly ILogger<GetnetService> _logger;

    /* FLUXO MÍNIMO PARA UMA TRANSAÇÂO
        O fluxo mínimo para uma transação de crédito na Getnet
        gerar token - /auth/oauth/v2/token
        gerar token do Cartão - /v1/tokens/card
        realizar transação - /v1/payments/credit 

        /v1/tokenization/token - Usado apenas em tokenização via TSP(Apple Apy, Google Pay, etc)
        /v1/tokenization/crypt - Geração de criptograma (TSP)
        /v1/cards/verification - Verifica validade do cartão - não é exigido antes de uma transação.
    */
    private readonly string[] _authTokenUrl = [
        "/auth/oauth/v2/token",
        "/v1/tokens/card",
        "/v1/tokenization/token",
        "/v1/tokenization/crypt",
        "/v1/cards/verification",
        "/v1/payments/credit"
    ];
    private readonly GetnetSettings _settings;
    private readonly string[] _contentType = [
        "application/x-www-form-urlencoded",
        "application/json"
    ];

    private TokenResponse _cachedToken;
    private DateTime _tokenExpiration;
    private readonly SemaphoreSlim _semaphore = new(1,1);


    #endregion

    #region Construtores
    public GetnetService(
        HttpClient httpClient,
        ILogger<GetnetService> logger,
        IOptions<GetnetSettings> options
    )
    {
        _httpClient = httpClient;
        _logger = logger;
        _settings = options.Value;
    }
    #endregion

    #region Gerar Token
    /// <summary>
    /// Método para gerar o token de autenticação.
    /// O token é necessário para realizar operações na API do Getnet.
    /// </summary>
    /// <returns>token</returns>
    /// <exception cref="ApplicationException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<TokenResponse> GetTokenAsync()
    {

        if (!string.IsNullOrEmpty(_cachedToken?.AccessToken) && DateTime.UtcNow < _tokenExpiration)
        {
            return _cachedToken;
        }

        await _semaphore.WaitAsync();

        try
        {

            if (!string.IsNullOrEmpty(_cachedToken?.AccessToken) && DateTime.UtcNow < _tokenExpiration)
            {
                return _cachedToken;
            }

            if (string.IsNullOrEmpty(_settings.ClienteId) || string.IsNullOrEmpty(_settings.ClienteSecret))
            {
                throw new ApplicationException("Credenciais do cliente não configuradas corretamente.");
            }

            var token = new Token
            {
                ClientId = _settings.ClienteId,
                ClientSecret = _settings.ClienteSecret,
            };

            var clientCredentials = $"{token.ClientId}:{token.ClientSecret}";
            var base64ClientCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(clientCredentials));

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _authTokenUrl[0]);
            request.Content = new StringContent($"grant_type={token.GrantType}", Encoding.UTF8, _contentType[0]);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64ClientCredentials);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Erro ao obter token: {(int)response.StatusCode} - {response.ReasonPhrase} - {errorContent}");

                throw new ApplicationException($"Erro ao obter token: {response.StatusCode} - {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (tokenResponse == null)
            {
                throw new Exception($"Resposta inválida da API? {content}");
            }

            _cachedToken = tokenResponse;
            _tokenExpiration = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn - 120);

            return tokenResponse;
        }
        finally
        {
            _semaphore.Release();
        }
    }
    #endregion

    #region Gerar Token Cartão
    public async Task<TokenCardResponse> GetTokenCard(TokenCard card, string token)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _authTokenUrl[1]);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Headers.Add("seller_id", card.SellerId);
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        request.Content = new StringContent(JsonSerializer.Serialize(card), Encoding.UTF8, _contentType[1]);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();

            _logger.LogError($"Erro ao obter token do cartão: {(int)response.StatusCode} - {response.ReasonPhrase} - {errorContent}");

            throw new GetnetApiExceptions(
                $"Erro ao obter token do cartão: {response.StatusCode} - {response.ReasonPhrase} - {errorContent}",
                (int)response.StatusCode
            );
        }

        var content = await response.Content.ReadAsStringAsync();

        var tokenCardResponse = JsonSerializer.Deserialize<TokenCardResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return tokenCardResponse ?? throw new Exception($"Resposta inválida da API: {content}");
    }
    #endregion

    #region Gerar Token Bandeira
    public async Task<TokenFlagResponse> GetTokenFlag(TokenFlag tokenFlag, string token)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _authTokenUrl[2]);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Content = new StringContent(JsonSerializer.Serialize(tokenFlag), Encoding.UTF8, _contentType[1]);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new GetnetApiExceptions(
                $"Erro ao obter token da bandeira: {response.StatusCode} - {response.ReasonPhrase}",
                (int)response.StatusCode
            );
        }

        var content = await response.Content.ReadAsStringAsync();

        var tokenFlagResponse = JsonSerializer.Deserialize<TokenFlagResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return tokenFlagResponse ?? throw new Exception($"Resposta inválida da API: {content}");
    }
    #endregion

    #region Gerar Criptograma
    public async Task<CardCryptogramResponse> GetCardCryptogram(CardCryptogram cardCryptogram, string token)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _authTokenUrl[3]);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Content = new StringContent(JsonSerializer.Serialize(cardCryptogram), Encoding.UTF8, _contentType[1]);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new GetnetApiExceptions(
                $"Erro ao obter criptograma do cartão: {response.StatusCode} - {response.ReasonPhrase}",
                (int)response.StatusCode
            );
        }

        var content = await response.Content.ReadAsStringAsync();

        var cardCryptogramResponse = JsonSerializer.Deserialize<CardCryptogramResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return cardCryptogramResponse ?? throw new Exception($"Resposta inválida da API: {content}");
    }
    #endregion

    #region Verificação de cartão
    public async Task<CardVerificationResponse> GetCardVerification(CardVerification cardVerification, string token, string? sellerId)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _authTokenUrl[4]);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Content = new StringContent(JsonSerializer.Serialize(cardVerification), Encoding.UTF8, _contentType[1]);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new GetnetApiExceptions(
                $"Erro ao fazer verificação do cartão: {response.StatusCode} - {response.ReasonPhrase}",
                (int)response.StatusCode
            );
        }

        var content = await response.Content.ReadAsStringAsync();

        var cardVerificationResponse = JsonSerializer.Deserialize<CardVerificationResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return cardVerificationResponse ?? throw new Exception($"Resposta inválida da API: {content}");
    }

    #endregion

    #region Transação com cartão de crédito
    public async Task<PaymentCreditResponse> Transaction(PaymentCredit payment, string token)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _authTokenUrl[5]);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        request.Content = new StringContent(JsonSerializer.Serialize(payment), Encoding.UTF8, _contentType[1]);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new GetnetApiExceptions(
                $"Erro ao realizar transação: {response.StatusCode} - {response.ReasonPhrase}",
                (int)response.StatusCode
            );
        }

        var content = await response.Content.ReadAsStringAsync();

        var paymentResponse = JsonSerializer.Deserialize<PaymentCreditResponse>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return paymentResponse ?? throw new Exception($"Resposta inválida da API: {content}");

    }
    #endregion

}
