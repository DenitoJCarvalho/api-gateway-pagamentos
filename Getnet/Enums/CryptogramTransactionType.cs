using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Getnet.Infrastructure.Configurations.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Tipo de criptograma.
/// </summary>
[JsonConverter(typeof(JsonEnumMemberConverter<CryptogramTransactionType>))]
public enum CryptogramTransactionType
{
    /// <summary>
    /// Transaçao iniciada pelo cliente
    /// </summary>
    [EnumMember(Value = "CIT")]
    CIT,

    /// <summary>
    /// Transação iniciada pelo comerciante - pagamento recorrente
    /// </summary>
    [EnumMember(Value = "MIT")]
    MIT
}
