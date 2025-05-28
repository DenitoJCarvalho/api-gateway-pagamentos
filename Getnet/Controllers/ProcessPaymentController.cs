using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Getnet.Controllers.Dtos;
using Getnet.Entities.Request;
using Getnet.Entities.Commom;
using Getnet.Services.Interfaces;
using Getnet.Infrastructure.Configurations.Getnet;
using Microsoft.Extensions.Options;
using Getnet.Enums;
using Getnet.Services;
using Getnet.Exceptions;
using System.Text.Json;

namespace Getnet.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProcessPaymentController : ControllerBase
{
    #region Propriedades
    private readonly ILogger<ProcessPaymentController> _logger;
    private readonly LogService _logService;
    private readonly IGetnetService _getnetService;

    private readonly GetnetSettings _settings;
    #endregion

    #region Construtores
    public ProcessPaymentController(
        ILogger<ProcessPaymentController> logger,
        LogService logService,
        IGetnetService getnetService,
         IOptions<GetnetSettings> options
    )
    {
        _logger = logger;
        _logService = logService;
        _getnetService = getnetService;
        _settings = options.Value;
    }
    #endregion

    #region Gerar Token

    /// <summary>
    /// Gera um token de autenticação para uso nas requisições à API Getnet.
    /// </summary>
    /// <returns>Retorna o token de autenticação em caso de sucesso (HTTP 200).
    /// Em caso de erro, retorna um <see cref="ProblemDetails"/> com informações sobre a falha.</returns>

    [AllowAnonymous]
    [HttpPost("generate-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> GetToken()
    {
        try
        {
            var token = await _getnetService.GetTokenAsync();

            return Ok(token);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Erro de conexão com o servidor de autenticação: {ex.Message}");
 
            return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Serviço de autenticação indisponível. Tente novamente mais tarde.");
        }
        catch (GetnetApiExceptions ex)
        {
            var problem = new ProblemDetails
            {
                Title = "Erro ao obter token de autenticação",
                Detail = ex.Message,
                Status = ex.StatusCode,
                Instance = HttpContext.Request.Path,
            };

            if (!string.IsNullOrEmpty(ex.ErrorContent))
            {
                problem.Extensions["getnetError"] = JsonSerializer.Deserialize<object>(ex.ErrorContent);
            }

            problem.Extensions["errorCode"] = "TOKENs_ERROR";
            problem.Extensions["timestamp"] = DateTime.UtcNow;

            _logger.LogError($"Erro da getnet ao obter tooken de autenticação: {ex.Message} - {ex.ErrorContent}");

            return StatusCode(ex.StatusCode, problem);
        }
        catch (ApplicationException ex)
        {
            var problem = new ProblemDetails
            {
                Title = "Erro ao obter token",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = HttpContext.Request.Path
            };

            problem.Extensions["errorCode"] = "TOKEN_ERROR";
            problem.Extensions["timestamp"] = DateTime.UtcNow;

            _logger.LogError($"Erro ao obter token: {ex.Message}");

            return BadRequest(problem);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter token");

            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
        }
    }

    #endregion

    #region Gerar Token do Cartão

    /// <summary>
    /// Gera um token para o cartão de crédito informado, necessário para realizar transações.
    /// </summary>
    /// <param name="card">Dados do cartão e do cliente que serão utilizados para gerar o token.</param>
    /// <returns>Retorna o token do cartão em caso de sucesso (HTTP 201).
    /// Em caso de falha, retorna um <see cref="ProblemDetails"/> com informações sobre o erro.</returns>
    [HttpPost("generate-token-card")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> GetTokenCard([FromBody] TokenCardDto card)
    {
        try
        {
            if (!Request.Headers.TryGetValue("Authorization", out var tokenHeader))
            {
                return Unauthorized($"Token não fornecido no cabeçalho Authorization.");
            }

            string accessToken = tokenHeader.ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                _logger.LogError("Token de autenticação não retornado ou inválido.");

                return StatusCode(StatusCodes.Status502BadGateway, "Erro ao obter token de autenticação.");
            }


            TokenCard cardRequest = new TokenCard
            {
                CardNumber = card.CardNumber,
                CustomerId = card.CustomerId,
                SellerId = _settings.SellerId,
            };

            var tokenCard = await _getnetService.GetTokenCard(cardRequest, accessToken);

            return StatusCode(StatusCodes.Status201Created, tokenCard);
        }
        catch (GetnetApiExceptions ex)
        {
            var problem = new ProblemDetails
            {
                Title = "Erro ao obter token do cartão.",
                Detail = ex.Message,
                Status = ex.StatusCode,
                Instance = HttpContext.Request.Path,
            };

            if (!string.IsNullOrEmpty(ex.ErrorContent))
            {
                problem.Extensions["getnetError"] = JsonSerializer.Deserialize<object>(ex.ErrorContent);
            }

            problem.Extensions["errorCode"] = "TOKEN_CARD_ERROR";
            problem.Extensions["timestamp"] = DateTime.UtcNow;

            _logger.LogError($"Erro da getnet ao obter token do cartão: {ex.Message} - {ex.ErrorContent}");

            return StatusCode(ex.StatusCode, problem);
        }
        catch (ApplicationException ex)
        {

            var problem = new ProblemDetails
            {
                Title = "Erro ao obter token do cartão",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = HttpContext.Request.Path,
            };

            problem.Extensions["errorCode"] = "TOKEN_CARD_ERROR";
            problem.Extensions["timestamp"] = DateTime.UtcNow;

            _logger.LogError($"Erro ao obter token do cartão: {ex.Message}");

            return BadRequest(problem);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Erro de conexão com o servidor de autenticação: {ex.Message}");
 
            return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Serviço de autenticação indisponível. Tente novamente mais tarde.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter token do cartão");

            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
        }
    }
    #endregion

    #region  Gerar Token da Bandeira
    /// <summary>
    /// Gera um token de bandeira para o cartão de crédito informado, necessário para realizar transações com bandeiras específicas.
    /// </summary>
    /// <param name="tokenFlag"></param>
    /// <returns>Retorna um objeto com os dados do token de bandeira em caso de sucesso (HTTP 200),
    /// ou um objeto <see cref="ProblemDetails"/> com informações sobre o erro ocorrido.</returns>
    [HttpPost("generate-token-brand")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> GetTokenFlag([FromBody] TokenFlagDto tokenFlag)
    {
        try
        {
            if (!Request.Headers.TryGetValue("Authorization", out var tokenHeader))
            {
                return Unauthorized($"Token não fornecido no cabeçalho Authorization.");
            }

            string accessToken = tokenHeader.ToString();

            TokenFlag tokenFlagRequest = new TokenFlag
            {
                CustomerId = tokenFlag.CustomerId,
                CardPan = tokenFlag.CardPan,
                CardPanSource = CardPanSource.ManuallyEntered,
                CardBrand = tokenFlag.CardBrand,
                ExpirationYear = tokenFlag.ExpirationYear,
                ExpirationMonth = tokenFlag.ExpirationMonth,
                SecurityCode = tokenFlag.SecurityCode,
                Email = tokenFlag.Email
            };

            var tokenCard = await _getnetService.GetTokenFlag(tokenFlagRequest, accessToken);

            return StatusCode(StatusCodes.Status200OK, tokenCard);
        }
        catch (GetnetApiExceptions ex)
        {
            var problem = new ProblemDetails
            {
                Title = "Erro ao obter token da bandeira.",
                Detail = ex.Message,
                Status = ex.StatusCode,
                Instance = HttpContext.Request.Path,
            };

            if (!string.IsNullOrEmpty(ex.ErrorContent))
            {
                problem.Extensions["getnetError"] = JsonSerializer.Deserialize<object>(ex.ErrorContent);
            }

            problem.Extensions["errorCode"] = "TOKEN_FLAG_ERROR";
            problem.Extensions["timestamp"] = DateTime.UtcNow;

            _logger.LogError($"Erro da getnet ao obter token da bandeira: {ex.Message} - {ex.ErrorContent}");

            return StatusCode(ex.StatusCode, problem);
        }
        catch (ApplicationException ex)
        {
            var problem = new ProblemDetails
            {
                Title = "Erro ao obter token da bandeira",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = HttpContext.Request.Path,
            };

            problem.Extensions["errorCode"] = "TOKEN_FLAG_ERROR";
            problem.Extensions["timestamp"] = DateTime.UtcNow;

            _logger.LogError($"Erro ao obter token da bandeira: {ex.Message}");

            return BadRequest(problem);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Erro de conexão com o servidor de autenticação: {ex.Message}");
    
            return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Serviço de autenticação indisponível. Tente novamente mais tarde.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter token da bandeira");
    
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
        }
    }
    #endregion

    #region Gerar Criptograma do Cartão
    /// <summary>
    /// Gera um criptograma para o cartão de crédito informado, necessário para transações com cartões tokenizados.
    /// </summary>
    /// <param name="cardCryptogram"></param>
    /// <returns>Retorna um objeto com os dados de criptograma em caso de sucesso (HTTP 200),
    /// ou um objeto <see cref="ProblemDetails"/> com informações sobre o erro ocorrido.</returns>
    [HttpPost("generate-card-cryptogram")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> GetCardCryptogram([FromBody] CardCryptogramDto cardCryptogram)
    {
        try
        {
            if (!Request.Headers.TryGetValue("Authorization", out var tokenHeader))
            {
                return Unauthorized($"Token não fornecido no cabeçalho Authorization.");
            }

            string accessToken = tokenHeader.ToString();

            CardCryptogram cardCryptogramRequest = new CardCryptogram
            {
                NetworkTokenId = cardCryptogram.NetworkTokenId,
                TransactionType = CryptogramTransactionType.CIT,
                CryptogramType = cardCryptogram.CryptogramType,
                Amount = cardCryptogram.Amount,
                CustomerId = cardCryptogram.CustomerId,
                Email = cardCryptogram.Email,
                CardBrand = cardCryptogram.CardBrand
            };

            var tokenCard = await _getnetService.GetCardCryptogram(cardCryptogramRequest, accessToken);

            return StatusCode(StatusCodes.Status200OK, tokenCard);
        }
        catch (GetnetApiExceptions ex)
        {
            var problem = new ProblemDetails
            {
                Title = "Erro ao obter criptograma do cartão.",
                Detail = ex.Message,
                Status = ex.StatusCode,
                Instance = HttpContext.Request.Path,
            };

            if (!string.IsNullOrEmpty(ex.ErrorContent))
            {
                problem.Extensions["getnetError"] = JsonSerializer.Deserialize<object>(ex.ErrorContent);
            }

            problem.Extensions["errorCode"] = "CARD_CRYPTOGRAM_ERROR";
            problem.Extensions["timestamp"] = DateTime.UtcNow;

            _logger.LogError($"Erro da getnet ao obter criptograma do cartão: {ex.Message} - {ex.ErrorContent}");

            return StatusCode(ex.StatusCode, problem);
        }
        catch (ApplicationException ex)
        {
            var problem = new ProblemDetails
            {
                Title = "Erro ao obter criptograma do cartão",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = HttpContext.Request.Path,
            };

            problem.Extensions["errorCode"] = "CARD_CRYPTOGRAM_ERROR";
            problem.Extensions["timestamp"] = DateTime.UtcNow;

            _logger.LogError($"Erro ao obter criptograma do cartão: {ex.Message}");

            return BadRequest(problem);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Erro de conexão com o servidor de autenticação: {ex.Message}");
 
            return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Serviço de autenticação indisponível. Tente novamente mais tarde.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter criptograma do cartão");

            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
        }
    }
    #endregion

    #region Realizar Transação

    /// <summary>
    /// Realiza uma transação de pagamento com cartão de crédito via Getnet.
    /// </summary>
    /// <param name="paymentCredit">Objeto contendo os dados da transação, como valor, cliente, e informações do cartão.</param>
    /// <returns>Retorna um objeto com os dados da transação em caso de sucesso (HTTP 200),
    /// ou um objeto <see cref="ProblemDetails"/> com informações sobre o erro ocorrido.</returns>
    [HttpPost("transact")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    public async Task<IActionResult> Transaction([FromBody] PaymentCreditDto paymentCredit)
    {
        try
        {
            if (!Request.Headers.TryGetValue("Authorization", out var tokenHeader))
            {
                return Unauthorized($"Token não fornecido no cabeçalho Authorization.");
            }

            string accessToken = tokenHeader.ToString();

            PaymentCredit payment = new PaymentCredit
            {
                SellerId = _settings.SellerId,
                Amount = paymentCredit.Amount,
                Currency = CurrencyCode.BRL,
                Order = new Order
                {
                    OrderId = paymentCredit.Order.OrderId
                },
                Customer = new Customer
                {
                    CustomerId = paymentCredit.Customer.CustomerId,
                    FirstName = paymentCredit.Customer.FirstName,
                    LastName = paymentCredit.Customer.LastName,
                    Name = paymentCredit.Customer.Name,
                    Email = paymentCredit.Customer.Email,
                    DocumentType = paymentCredit.Customer.DocumentType,
                    DocumentNumber = paymentCredit.Customer.DocumentNumber,
                    PhoneNumber = paymentCredit.Customer.PhoneNumber,
                    BillingAddress = new Address
                    {
                        Street = paymentCredit.Customer.BillingAddress.Street,
                        Number = paymentCredit.Customer.BillingAddress.Number,
                        Complement = paymentCredit.Customer.BillingAddress.Complement,
                        District = paymentCredit.Customer.BillingAddress.District,
                        City = paymentCredit.Customer.BillingAddress.City,
                        State = paymentCredit.Customer.BillingAddress.State,
                        Country = paymentCredit.Customer.BillingAddress.Country,
                        PostalCode = paymentCredit.Customer.BillingAddress.PostalCode
                    }
                },
                Credit = new Credit
                {
                    Delayed = false,
                    PreAuthorization = false,
                    SaveCardData = false,
                    TransactionType = paymentCredit.Credit.TransactionType,
                    NumberInstallments = paymentCredit.Credit.NumberInstallments,
                    TransactionId = paymentCredit.Credit.TransactionId,

                    Card = new CardVerification
                    {
                        NumberToken = paymentCredit.Credit.Card.NumberToken,
                        CardHolderName = paymentCredit.Credit.Card.CardHolderName,
                        ExpirationMonth = paymentCredit.Credit.Card.ExpirationMonth,
                        ExpirationYear = paymentCredit.Credit.Card.ExpirationYear,
                        SecurityCode = paymentCredit.Credit.Card.SecurityCode,
                        Brand = paymentCredit.Credit.Card.Brand,
                    }
                }
            };

            var paymentResponse = await _getnetService.Transaction(payment, accessToken);

            return StatusCode(StatusCodes.Status200OK, paymentResponse);

        }
        catch (GetnetApiExceptions ex)
        {
            var problem = new ProblemDetails
            {
                Title = "Erro ao realizar transação de pagamento.",
                Detail = ex.Message,
                Status = ex.StatusCode,
                Instance = HttpContext.Request.Path,
            };

            if (!string.IsNullOrEmpty(ex.ErrorContent))
            {
                problem.Extensions["getnetError"] = JsonSerializer.Deserialize<object>(ex.ErrorContent);
            }

            problem.Extensions["errorCode"] = "PAYMENT_CREDIT_ERROR";
            problem.Extensions["timestamp"] = DateTime.UtcNow;

            _logger.LogError($"Erro da getnet ao realizar transação de pagamento: {ex.Message} - {ex.ErrorContent}");

            return StatusCode(ex.StatusCode, problem);
        }
        catch (ApplicationException ex)
        {
            var problem = new ProblemDetails
            {
                Title = "Erro ao realizar transaçao de pgamento.",
                Detail = ex.Message,
                Status = StatusCodes.Status400BadRequest,
                Instance = HttpContext.Request.Path,
            };

            problem.Extensions["errorCode"] = "PAYMENT_CREDIT_ERROR";
            problem.Extensions["timestamp"] = DateTime.UtcNow;

            _logger.LogError($"Erro ao realizar transação de pagamento: {ex.Message}");

            return BadRequest(problem);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError($"Erro de conexão com o servidor de autenticação: {ex.Message}");

            return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Serviço de autenticação indisponível. Tente novamente mais tarde.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao realizar transação de pagamento.");

            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
        }
    }
    #endregion

    #region Disponibilizar Seller ID
    /// <summary>
    /// Obtém o Seller ID configurado na aplicação.
    /// </summary>
    /// <returns>Retorna um objeto com o identifcador sellerId.</returns>
    [HttpPost("seller-id")]
    public IActionResult GetSellerId()
    {
        try
        {
            var selllerId = _getnetService.GetSellerId();

            return Ok(selllerId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter Seller ID.");

            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
        }
    }
    #endregion

}
