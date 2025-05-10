
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Getnet.Entities.Dto;

/// <summary>
/// Conjunto de dados do cartão.
/// </summary>
public class CardVerificationDto
{
    /// <summary>
    /// Número do cartão tokenizado. Gerado previamente por meio do endpoint /v1/tokens/card.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(128, 128, ErrorMessage = "O campo deve ter 128 caracteres.")]
    [JsonPropertyName("number_token")]
    public string NumberToken { get; set; } = string.Empty;

    /// <summary>
    /// Valores possíveis: "Mastercard", "Visa", "Amex", "Elo", "Hipercard".
    /// Bandeira do cartão. Preenchido automaticamente pela API caso não seja informado.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1, 50, ErrorMessage = "O campo deve ter entre 1 e 50 caracteres.")]
    [JsonPropertyName("brand")]
    public string Brand { get; set; } = string.Empty;

    /// <summary>
    /// Nome do comprador impresso no cartão.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1, 26, ErrorMessage = "O campo deve ter entre 1 e 26 caracteres.")]
    [JsonPropertyName("cardholder_name")]
    public string CardHolderName { get; set; } = string.Empty;

    /// <summary>
    /// Mês de expiração do cartão com dois dígitos.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(2, 2, ErrorMessage = "O campo deve ter 2 caracteres.")]
    [JsonPropertyName("expiration_month")]
    public string ExpirationMonth { get; set; } = string.Empty;

    /// <summary>
    /// Ano de expiração do cartão com dois dígitos.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(2, 2, ErrorMessage = "O campo deve ter 2 caracteres.")]
    [JsonPropertyName("expiration_year")]
    public string ExpirationYear { get; set; } = string.Empty;

    /// <summary>
    /// Código de segurança. CVV ou CVC.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(3, 4, ErrorMessage = "O campo deve ter entre 3 e 4 caracteres.")]
    [JsonPropertyName("security_code")]
    public string SecurityCode { get; set; } = string.Empty;

    /// <summary>
    /// Merchant Payment Gateway ID.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("gateway_id")]
    public string GatewayId { get; set; } = string.Empty;
}
