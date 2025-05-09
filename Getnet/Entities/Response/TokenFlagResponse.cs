
using System.Text.Json.Serialization;

namespace Getnet.Entities.Response;

public class TokenFlagResponse
{
    /// <summary>
    /// Token do cartão que será utilizado na geração de criptograma.
    /// </summary>
    [JsonPropertyName("network_token_id")]
    public string NetworkTokenId { get; set; } = string.Empty;

    /// <summary>
    /// Status do token.
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;
}
