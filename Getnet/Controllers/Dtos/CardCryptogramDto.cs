
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Getnet.Controllers.Dtos;

public class CardCryptogramDto
{
    /// <summary>
    /// Token obtido em GetTokenFlag.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [JsonPropertyName("network_token_id")]
    public string NetworkTokenId { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de critptografia.
    /// Valores possíveis: 
    /// CIT: Transação iniciada pelo cliente.
    /// MIT: Transação iniciada pelo comerciante.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [JsonPropertyName("transaction_type")]
    public string TransactionType { get; set; } = string.Empty;

    /// <summary>
    /// Identificador da Bandeira do cartão.
    /// Valores poss[iveis:
    /// VISA_TAV: Valor de verificação de autenticação do token VISA.
    /// MC_DSRP_LONG: controle remoto seguro digital Mastercard.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [JsonPropertyName("cryptogram_type")]
    public string CryptogramType { get; set; } = string.Empty;

    /// <summary>
    /// Valor monetário da transação em centavos
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    /// <summary>
    /// É a identificação que você utiliza para o seu cliente (E-mail, CPF, ID do Cliente, entre  outros).
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [JsonPropertyName("customer_id")]
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// Email do comprador.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Identificador da Bandeira do cartão.
    /// Valores possíveis: VISA e MASTERCARD.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [JsonPropertyName("card_brand")]
    public string CardBrand { get; set; } = string.Empty;
    
}
