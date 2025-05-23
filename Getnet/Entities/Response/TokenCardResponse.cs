
using System.Text.Json.Serialization;

namespace Getnet.Entities.Response;

/// <summary>
/// Representa a resposta do serviço de tokenização do cartão.
/// </summary>

public class TokenCardResponse
{
  [JsonPropertyName("number_token")]
  public string NumberToken { get; set; } = string.Empty;
}
