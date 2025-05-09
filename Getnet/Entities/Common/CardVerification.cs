
using System.Text.Json.Serialization;

namespace Getnet.Entities.Commom;

public class CardVerification
{
    /// <summary>
    /// Número do cartão tokenizado. Gerado previamente por meio do endpoint /v1/tokens/card.
    /// </summary>
    [JsonPropertyName("number_token")]
    public string NumberToken { get; set; } = string.Empty;

    /// <summary>
    /// Valores possíveis: "Mastercard", "Visa", "Amex", "Elo", "Hipercard".
    /// Bandeira do cartão. Preenchido automaticamente pela API caso não seja informado.
    /// </summary>
    [JsonPropertyName("brand")]
    public string Brand { get; set; } = string.Empty;

    /// <summary>
    /// Nome do comprador impresso no cartão.
    /// </summary>
    [JsonPropertyName("cardholder_name")]
    public string CardHolderName { get; set; } = string.Empty;

    /// <summary>
    /// Mês de expiração do cartão com dois dígitos.
    /// </summary>
    [JsonPropertyName("expiration_month")]
    public string ExpirationMonth { get; set; } = string.Empty;

    /// <summary>
    /// Ano de expiração do cartão com dois dígitos.
    /// </summary>
    [JsonPropertyName("expiration_year")]
    public string ExpirationYear { get; set; } = string.Empty;

    /// <summary>
    /// Código de segurança. CVV ou CVC.
    /// </summary>
    [JsonPropertyName("security_code")]
    public string SecurityCode { get; set; } = string.Empty;

    /// <summary>
    /// Merchant Payment Gateway ID.
    /// </summary>
    [JsonPropertyName("gateway_id")]
    public string GatewayId { get; set; } = string.Empty;
}
