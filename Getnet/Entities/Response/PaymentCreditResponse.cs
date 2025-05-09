

using System.Text.Json.Serialization;
using Getnet.Entities.Common;

namespace Getnet.Entities.Response;

public class PaymentCreditResponse
{
    /// <summary>
    /// Código de identificação do pagamento.
    /// </summary>
    [JsonPropertyName("payment_id")]
    public string PaymentId { get; set; } = string.Empty;

    /// <summary>
    /// Código de identificação do e-commerce.
    /// </summary>
    [JsonPropertyName("seller_id")]
    public string SellerId { get; set; } = string.Empty;

    /// <summary>
    /// Valor da compra em centavos.
    /// </summary>
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    /// <summary>
    /// Identificação da moeda.
    /// </summary>
    [JsonPropertyName("currency")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Código de identificação da compra utilizado pelo e-commerce.
    /// </summary>
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Status da transação
    /// Valores válidos: "CANCELED""APPROVED""DENIED""AUTHORIZED""CONFIRMED"
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Data e hora do registro de pagamento.
    /// </summary>
    [JsonPropertyName("received_at")]
    public string ReceivedAt { get; set; } = string.Empty;

    /// <summary>
    /// Conjunto de dados referentes a transação.
    /// </summary>
    [JsonPropertyName("credit")]
    public PaymentCreditDetail Credit { get; set; } = new PaymentCreditDetail { };
}
