
using Getnet.Entities.Commom;
using Getnet.Entities.Request;
using Getnet.Entities.Response;

namespace Getnet.Services.Interfaces;

/// <summary>
/// Interface que define os métodos para interação com a API da Getnet.
/// </summary>
public interface IGetnetService
{
    /// <summary>
    /// Obtém um token de autenticação para acessar a API da Getnet.
    /// Este token deve ser utilizado em todas as requisições subsequentes à API.
    /// </summary>
    /// <returns>Um objeto <see cref="TokenResponse"/>contendo o token de acesso.</returns>
    Task<TokenResponse> GetTokenAsync();

    /// <summary>
    /// Gera um token para umcartão com base nos dados fornecidos.
    /// </summary>
    /// <param name="card">Objeto contendo os dados do cartão.</param>
    /// <param name="token">Token de autenticação da API.</param>
    /// <returns>Um objeto <see cref="TokenCardResponse"/> contendo o token do cartão.</returns>
    Task<TokenCardResponse> GetTokenCard(TokenCard card, string token);

    /// <summary>
    /// Gera um token para a bandeira do cartão com base nos dados fornecidos.
    /// </summary>
    /// <param name="tokenFlag">Objeto contendo os dados necssários para identificar a bandeira.</param>
    /// <param name="token">Token de autenticação da API.</param>
    /// <returns>Um objeto  <see cref="TokenFlagResponse" /> contendo o token da bandeira.</returns>
    Task<TokenFlagResponse> GetTokenFlag(TokenFlag tokenFlag, string token);

    /// <summary>
    /// Gera um criptograma para o cartão com base nos dados fornecidos.
    /// </summary>
    /// <param name="cardCryptogram">Objeto contendo os dados necessários para gerar criptograma.</param>
    /// <param name="token">Token de autenticação da API.</param>
    /// <returns>Um objeto <see cref="CardCryptogramResponse"/> contendo o criptograma para transação.</returns>
    Task<CardCryptogramResponse> GetCardCryptogram(CardCryptogram cardCryptogram, string token);

    /// <summary>
    /// Realiza a verificação do cartão com base nos dados fornecidos.
    /// Este método verifica se o cartão é válido e se os dados estão corretos.
    /// </summary>
    /// <param name="cardVerification">Dados do cartão</param>
    /// <param name="token">Token de autenticação da API.</param>
    /// <param name="sellerId">Código de verificação do e-commerce.</param>
    /// <returns>Um objeto <see cref="CardVerificationResponse"/> contendo status, verification_id, authorization_id, transaction_id.</returns>
    Task<CardVerificationResponse> GetCardVerification(CardVerification cardVerification, string token, string? sellerId);

    /// <summary>
    /// Realiza uma transação de pagamento por cartão de crédito com os dados informados.
    /// </summary>
    /// <param name="payment">Objeto contendo as informações da transação de crédito, como valor, número de parcelas, dados do cartão e demais configurações.</param>
    /// <param name="token">Token de autenticação da API.</param>
    /// <returns>Um objeto <see cref="PaymentCreditResponse"/> contendo os dados da resposta da transação de crédito, como status, código de autorização, e detalhes do pagamento.</returns>
    Task<PaymentCreditResponse> Transaction(PaymentCredit payment, string token);

    /// <summary>
    /// Obtém o ID do vendedor associado à conta Getnet.
    /// </summary>
    /// <returns>Uma string <see cref="SellerResponse"/> contendo a indentificação do vendedor junto a Getnet </returns>
    SellerResponse GetSellerId();
    
}
