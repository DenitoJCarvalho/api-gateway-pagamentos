
using System.Text.Json.Serialization;

namespace Getnet.Entities.Response;

public class CardCryptogramResponse
{
    /// <summary>
    /// Criptograma do cartão que será utilizado nas transações.
    /// </summary>
    [JsonPropertyName("cryptogram")]
    public string Cryptogram { get; set; } = string.Empty;

    /// <summary>
    /// Novo pan tokenizado.
    /// </summary>
    [JsonPropertyName("token_pan_card")]
    public string TokenPanCard { get; set; } = string.Empty;

    /// <summary>
    /// Novo mês de expiração do pan tokenizado.
    /// </summary>
    /// 
    [JsonPropertyName("token_expiration_month")]
    public string TokenExpirationMonth { get; set; } = string.Empty;

    /// <summary>
    /// Novo ano de expiração do pan tokenizado.
    /// </summary>
    [JsonPropertyName("token_expiration_year")]
    public string TokenExpirationYear { get; set; } = string.Empty;

    /// <summary>
    /// Criptograma do cartão que será utilizado nas transações.
    /// </summary>
    [JsonPropertyName("token_status")]
    public string TokenStatus { get; set; } = string.Empty;
    

}
