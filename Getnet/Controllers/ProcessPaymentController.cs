using Microsoft.AspNetCore.Mvc;

using Getnet.Services;
using Getnet.Controllers.Dtos;
using Getnet.Entities.Request;
using Getnet.Entities.Commom;
using Getnet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Getnet.Controllers.ProcessPayment;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProcessPaymentController : ControllerBase
{
    #region Propriedades
    private readonly ILogger<ProcessPaymentController> _logger;
    private readonly IGetnetService _getnetService;
    #endregion

    #region Construtores
    public ProcessPaymentController(
        ILogger<ProcessPaymentController> logger,
        IGetnetService getnetService
    )
    {
        _logger = logger;
        _getnetService = getnetService;
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
    [Produces("application/x-www-form-urlencoded")]
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

    [Authorize]
    [HttpPost("generate-token-card")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> GetTokenCard([FromBody] TokenCardDto card)
    {
        try
        {
            var token = await _getnetService.GetTokenAsync();

            if (string.IsNullOrWhiteSpace(token?.AccessToken))
            {
                _logger.LogError("Token de atutenticação não retornado ou inválido.");

                return StatusCode(StatusCodes.Status502BadGateway, "Erro ao obter token de autenticação.");
            }

            TokenCard cardRequest = new TokenCard
            {
                CardNumber = card.CardNumber,
                CustomerId = card.CustomerId,
                SellerId = card.SellerId
            };

            var tokenCard = await _getnetService.GetTokenCard(cardRequest, token.AccessToken);

            return StatusCode(StatusCodes.Status201Created, tokenCard);
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

    // #region  Gerar Token da Bandeira
    // [Authorize]
    // [HttpPost("gerar-token-bandeira")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [Produces("application/json")]
    // public async Task<IActionResult> GetTokenFlag([FromBody] TokenFlagDto tokenFlag)
    // {
    //     try
    //     {
    //         var token = await _getnetService.GetTokenAsync();

    //         TokenFlag tokenFlagRequest = new TokenFlag
    //         {
    //             CustomerId = tokenFlag.CustomerId,
    //             CardPan = tokenFlag.CardPan,
    //             CardPanSource = tokenFlag.CardPanSource,
    //             CardBrand = tokenFlag.CardBrand,
    //             ExpirationYear = tokenFlag.ExpirationYear,
    //             ExpirationMonth = tokenFlag.ExpirationMonth,
    //             SecurityCode = tokenFlag.SecurityCode,
    //             Email = tokenFlag.Email
    //         };

    //         var tokenCard = await _getnetService.GetTokenFlag(tokenFlagRequest, token.AccessToken);

    //         return StatusCode(StatusCodes.Status200OK, tokenCard);
    //     }
    //     catch (ApplicationException ex)
    //     {
    //         var problem = new ProblemDetails
    //         {
    //             Title = "Erro ao obter token da bandeira",
    //             Detail = ex.Message,
    //             Status = StatusCodes.Status400BadRequest,
    //             Instance = HttpContext.Request.Path,
    //         };

    //         problem.Extensions["errorCode"] = "TOKEN_FLAG_ERROR";
    //         problem.Extensions["timestamp"] = DateTime.UtcNow;

    //         _logger.LogError($"Erro ao obter token da bandeira: {ex.Message}");
    //         return BadRequest(problem);
    //     }
    //     catch (HttpRequestException ex)
    //     {
    //         _logger.LogError($"Erro de conexão com o servidor de autenticação: {ex.Message}");
    //         return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Serviço de autenticação indisponível. Tente novamente mais tarde.");
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Erro ao obter token da bandeira");
    //         return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
    //     }
    // }
    // #endregion

    // #region Gerar Criptograma do Cartão
    // [Authorize]
    // [HttpPost("gerar-criptograma-cartao")]
    // [ProducesResponseType(StatusCodes.Status200OK)]
    // [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    // [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    // [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    // [Produces("application/json")]
    // public async Task<IActionResult> GetCardCryptogram([FromBody] CardCryptogramDto cardCryptogram)
    // {
    //     try
    //     {
    //         var token = await _getnetService.GetTokenAsync();

    //         CardCryptogram cardCryptogramRequest = new CardCryptogram
    //         {
    //             NetworkTokenId = cardCryptogram.NetworkTokenId,
    //             TransactionType = cardCryptogram.TransactionType,
    //             CryptogramType = cardCryptogram.CryptogramType,
    //             Amount = cardCryptogram.Amount,
    //             CustomerId = cardCryptogram.CustomerId,
    //             Email = cardCryptogram.Email,
    //             CardBrand = cardCryptogram.CardBrand
    //         };

    //         var tokenCard = await _getnetService.GetCardCryptogram(cardCryptogramRequest, token.AccessToken);

    //         return StatusCode(StatusCodes.Status200OK, tokenCard);
    //     }
    //     catch (ApplicationException ex)
    //     {
    //         var problem = new ProblemDetails
    //         {
    //             Title = "Erro ao obter criptograma do cartão",
    //             Detail = ex.Message,
    //             Status = StatusCodes.Status400BadRequest,
    //             Instance = HttpContext.Request.Path,
    //         };

    //         problem.Extensions["errorCode"] = "CARD_CRYPTOGRAM_ERROR";
    //         problem.Extensions["timestamp"] = DateTime.UtcNow;

    //         _logger.LogError($"Erro ao obter criptograma do cartão: {ex.Message}");
    //         return BadRequest(problem);
    //     }
    //     catch (HttpRequestException ex)
    //     {
    //         _logger.LogError($"Erro de conexão com o servidor de autenticação: {ex.Message}");
    //         return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Serviço de autenticação indisponível. Tente novamente mais tarde.");
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex, "Erro ao obter criptograma do cartão");
    //         return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
    //     }
    // }
    // #endregion

    #region Realizar Transação

    /// <summary>
    /// Realiza uma transação de pagamento com cartão de crédito via Getnet.
    /// </summary>
    /// <param name="paymentCredit">Objeto contendo os dados da transação, como valor, cliente, e informações do cartão.</param>
    /// <returns>Retorna um objeto com os dados da transação em caso de sucesso (HTTP 200),
    /// ou um objeto <see cref="ProblemDetails"/> com informações sobre o erro ocorrido.</returns>

    [Authorize]
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
            var token = await _getnetService.GetTokenAsync();

            PaymentCredit payment = new PaymentCredit
            {
                SellerId = paymentCredit.SellerId,
                Amount = paymentCredit.Amount,
                Currency = paymentCredit.Currency,
                Order = new Order
                {
                    OrderId = paymentCredit.Order.OrderId
                },
                Customer = new Customer
                {
                    CustomerId = paymentCredit.Customer.CustomerId,
                    FirstName = paymentCredit.Customer.FirstName,
                    LastName = paymentCredit.Customer.LastName,
                    Email = paymentCredit.Customer.Email
                },
                Credit = new Credit
                {
                    Delayed = paymentCredit.Credit.Delayed,
                    SaveCardData = paymentCredit.Credit.SaveCardData,
                    TransactionType = paymentCredit.Credit.TransactionType,
                    NumberInstallments = paymentCredit.Credit.NumberInstallments,
                    SoftDescriptor = paymentCredit.Credit.SoftDescriptor,
                    DynamicMcc = paymentCredit.Credit.DynamicMcc,

                    Card = new CardVerification
                    {
                        NumberToken = paymentCredit.Credit.Card.NumberToken,
                        CardHolderName = paymentCredit.Credit.Card.CardHolderName,
                        ExpirationMonth = paymentCredit.Credit.Card.ExpirationMonth,
                        ExpirationYear = paymentCredit.Credit.Card.ExpirationYear,
                        SecurityCode = paymentCredit.Credit.Card.SecurityCode
                    }
                }
            };

            var paymentResponse = await _getnetService.Transaction(payment, token.AccessToken);

            return StatusCode(StatusCodes.Status200OK, paymentResponse);

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

            problem.Extensions["errorCode"] = "PAYMENT_CREDIT_ERROR";
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

    
}
