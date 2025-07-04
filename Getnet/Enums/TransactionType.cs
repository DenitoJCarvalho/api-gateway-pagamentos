using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Getnet.Infrastructure.Configurations.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Tipo de transação.
/// </summary>
[JsonConverter(typeof(JsonEnumMemberConverter<TransactionType>))]
public enum TransactionType
{
    /// <summary>
    /// Pagamento à vista.
    /// </summary>
    [EnumMember(Value = "FULL")]
    Full,

    /// <summary>
    /// Parcelado sem juros.
    /// </summary>
    [EnumMember(Value = "INSTALL_NO_INTEREST")]
    InstallNoInterest,

    /// <summary>
    /// Parcelado com juros.
    /// </summary>
    [EnumMember(Value = "INSTALL_WITH_INTEREST")]
    InstallWithInterest
}
