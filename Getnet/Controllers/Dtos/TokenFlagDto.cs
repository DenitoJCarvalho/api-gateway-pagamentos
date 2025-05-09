
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Getnet.Controllers.Dtos;

/// <summary>
/// Entidade que representa as informações para tokenizar a bandeira do cartão.
/// </summary>
public class TokenFlagDto
{
    /// <summary>
    /// Identificador do comprador.
    /// Informação definida pela aplicação que consome a API da Plataforma Getnet.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1, 100, ErrorMessage = "Campo deve ter entre 1 e 100 caracteres")]
    [JsonPropertyName("customer_id")]
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// Número do cartão.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(13, 19, ErrorMessage = "Número do cartão deve ter entre 13 e 19 dígitos")]
    [JsonPropertyName("card_pan")]
    public string CardPan { get; set; } = string.Empty;

    /// <summary>
    /// Valores possíveis:
    /// ONFILE: solicitante do token já possui os dados PAN em seus registros.
    /// MANUALLY_ENTERED: os dados do PAN foram digitados pelo titular do cartão.
    /// VIA_APPLICATION: os dados PAN (ou referência) forma fornecidos por um Aplicativo Emissor.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("card_pan_source")]
    public string CardPanSource { get; set; } = string.Empty;

    /// <summary>
    /// Identificador da Bandeira do cartão.
    /// Valores possíveis: VISA e MASTERCARD.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("card_brand")]
    public string CardBrand { get; set; } = string.Empty;


    /// <summary>
    /// Ano de expiração do cartão.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("expiration_year")]
    public string ExpirationYear { get; set; } = string.Empty;

    /// <summary>
    /// Mês de expiração do cartão.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("expiration_month")]
    public string ExpirationMonth { get; set; } = string.Empty;

    /// <summary>
    ///  CVV do cartão.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("security_code")]
    public string SecurityCode { get; set; } = string.Empty;

    /// <summary>
    /// Email do comprador.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;
}
