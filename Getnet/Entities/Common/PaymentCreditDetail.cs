
using System.Text.Json.Serialization;

namespace Getnet.Entities.Common;

public class PaymentCreditDetail
{
    /// <summary>
    /// Identifica se o crédito será feito com confirmação tardia.
    /// </summary>
    [JsonPropertyName("delayed")]
    public bool Delayed { get; set; }

    /// <summary>
    /// Código interno da transação.
    /// </summary>
    [JsonPropertyName("authorization_code")]
    public string AuthorizationCode { get; set; } = string.Empty;

    /// <summary>
    /// Data/hora da autorização de crédito.
    /// </summary>
    [JsonPropertyName("authorization_at")]
    public DateTime AuthorizedAt { get; set; }

    /// <summary>
    /// Código de retorno.
    /// </summary>
    [JsonPropertyName("reason_code")]
    public string ReasonCode { get; set; } = string.Empty;

    /// <summary>
    /// Mensagem de retorno.
    /// </summary>
    [JsonPropertyName("reason_message")]
    public string ReasonMessage { get; set; } = string.Empty;

    /// <summary>
    /// Nome do adquirente.
    /// </summary>
    [JsonPropertyName("acquirer")]
    public string Acquirer { get; set; } = string.Empty;

    /// <summary>
    /// Descrição para a fatura do cartão.
    /// </summary>
    [JsonPropertyName("soft_descriptor")]
    public string SoftDescriptor { get; set; } = string.Empty;

    /// <summary>
    /// Bandeira resposável pelo o processamento da transação.
    /// </summary>
    [JsonPropertyName("brand")]
    public string Brand { get; set; } = string.Empty;

    /// <summary>
    /// Código de Autorização gerado pelo Emissor quando a transação é realizada com sucesso.
    /// </summary>
    [JsonPropertyName("terminal_nsu")]
    public string TerminalNsu { get; set; } = string.Empty;

    /// <summary>
    /// Código de transação do adquirente.
    /// </summary>
    [JsonPropertyName("acquirer_transaction_id")]
    public string AcquirerTransactionId { get; set; } = string.Empty;

    /// <summary>
    /// Código de transação.
    /// </summary>
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; } = string.Empty;

    /// <summary>
    /// Valor da primeira parcela.
    /// </summary>
    [JsonPropertyName("first_installment_amount")]
    public string FirstInstallmentAmount { get; set; } = string.Empty;

    /// <summary>
    /// Outro valor da parcela
    /// </summary>
    [JsonPropertyName("other_installment_amount")]
    public string OtherInstallmentAmount { get; set; } = string.Empty;

    /// <summary>
    /// Valor total da parcela.
    /// </summary>
    [JsonPropertyName("total_installment_amount")]
    public string TotalInstallmentAmount { get; set; } = string.Empty;

}
