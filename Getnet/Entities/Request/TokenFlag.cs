
using System.Text.Json.Serialization;

namespace Getnet.Entities.Request;

/// <summary>
/// Entidade que representa as informações para tokenizar a bandeira do cartão.
/// </summary>
public class TokenFlag
{
    /// <summary>
    /// Identificador do comprador.
    /// Informação definida pela aplicação que consome a API da Plataforma Getnet.
    /// </summary>
    [JsonPropertyName("customer_id")]
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// Número do cartão.
    /// </summary>
    [JsonPropertyName("card_pan")]
    public string CardPan { get; set; } = string.Empty;

    /// <summary>
    /// Valores possíveis:
    /// ONFILE: solicitante do token já possui os dados PAN em seus registros.
    /// MANUALLY_ENTERED: os dados do PAN foram digitados pelo titular do cartão.
    /// VIA_APPLICATION: os dados PAN (ou referência) forma fornecidos por um Aplicativo Emissor.
    /// </summary>
    [JsonPropertyName("card_pan_source")]
    public string CardPanSource { get; set; } = string.Empty;

    /// <summary>
    /// Identificador da Bandeira do cartão.
    /// Valores possíveis: VISA e MASTERCARD.
    /// </summary>
    [JsonPropertyName("card_brand")]
    public string CardBrand { get; set; } = string.Empty;


    /// <summary>
    /// Ano de expiração do cartão.
    /// </summary>
    [JsonPropertyName("expiration_year")]
    public string ExpirationYear { get; set; } = string.Empty;

    /// <summary>
    /// Mês de expiração do cartão.
    /// </summary>
    [JsonPropertyName("expiration_month")]
    public string ExpirationMonth { get; set; } = string.Empty;

    /// <summary>
    ///  CVV do cartão.
    /// </summary>
    [JsonPropertyName("security_code")]
    public string SecurityCode { get; set; } = string.Empty;

    /// <summary>
    /// Email do comprador.
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}
