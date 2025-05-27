
using System.Text.Json.Serialization;
using Getnet.Enums;

namespace Getnet.Entities.Commom;

public class Tokenization
{
    /// <summary>
    /// Tipo de tokenização.
    /// </summary>
    [JsonPropertyName("type")]
    public TokenizationType Type { get; set; }

    /// <summary>
    /// Valor do criptograma gerado pelo TSP (Token Service Provider)
    /// </summary>
    [JsonPropertyName("cryptogram")]
    public string Cryptogram { get; set; } = string.Empty;

    /// <summary>
    /// Indicador ECI (Eletronic Commerce Indicator)
    /// </summary>
    [JsonPropertyName("eci")]
    public Eci Eci { get; set; } 

    /// <summary>
    /// Código identifcador do Token Requestor
    /// </summary>
    [JsonPropertyName("requestor_id")]
    public string? RequestorId { get; set; } = string.Empty;
}
