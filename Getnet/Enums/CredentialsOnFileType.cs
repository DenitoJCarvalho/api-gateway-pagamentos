using System.Runtime.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Tipo de transação realizada com credenciais armazenadas.
/// </summary>
public enum CredentialsOnFileType
{
    /// <summary>
    /// Usado quando é realizado a primeira transação (parcela) de um pagamento normal.
    /// </summary>
    [EnumMember(Value = "ONE_CLICK")]
    OneClick,

    /// <summary>
    /// Usado quando é realizado transações para as demais parcelas restantes de um pagamento normal. Obs: Necessário informar o transaction_id da primeira transação realizada.
    /// </summary>
    [EnumMember(Value = "ONE_CLICK_PAYMENT")]
    OneClickPayment,

    /// <summary>
    /// Usado quando é realizado a primeira transação (parcela) de um pagamento recorrente.
    /// </summary>
    [EnumMember(Value = "RECURRING")]
    Recurring,

    /// <summary>
    ///  Usado quando é realizado transações para as demais parcelas restantes de um pagamento recorrente. Obs: Necessário informar o transaction_id da primeira transação realizada.
    /// </summary>
    [EnumMember(Value = "RECURRING_PAYMENT")]
    RecurringPayment
}
