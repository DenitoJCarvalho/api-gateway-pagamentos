
using System.Text.Json.Serialization;
using Getnet.Enums;

namespace Getnet.Controllers.Dtos.Common;

/// <summary>
/// Tipo de criptograma utilizado pelo TSP (Token Service Provider). Os valores podem ser - TAVV Para cartões da bandeira Visa e ELO; - UCAF Para cartões da bandeira Mastercard.
/// </summary>
public class TokenizationDto
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
    /// Código identifcador do Token Requestor
    /// </summary>
    [JsonPropertyName("requestor_id")]
    public string? RequestorId { get; set; } = string.Empty;
}
