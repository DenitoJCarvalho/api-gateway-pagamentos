
using System.Text.Json.Serialization;

namespace Getnet.Entities.Response;

public class CardVerificationResponse
{
    /// <summary>
    /// Status da verificação do cartão
    /// Valores possíveis: "VERIFIED", "NOT_VERIFIED", "DENIED", "ERROR".
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Identificação do pedido de verificação na plataforma digital.
    /// </summary>
    [JsonPropertyName("verification_id")]
    public string VerificationId { get; set; } = string.Empty;

    /// <summary>
    /// Número de autorização gerado pelo sistema de verificação de cartão.
    /// </summary>
    [JsonPropertyName("authorization_code")]
    public string AuthorizationCode { get; set; } = string.Empty; 

    /// <summary>
    /// Código de transação da verificação.
    /// </summary>
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; } = string.Empty;
}
