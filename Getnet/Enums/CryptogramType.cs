using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Getnet.Infrastructure.Configurations.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Indentificador da Bandeira do Cartão
/// </summary>
[JsonConverter(typeof(JsonEnumMemberConverter<CryptogramType>))]
public enum CryptogramType
{
    /// <summary>
    /// Valor de verficação do token VISA
    /// </summary>
    [EnumMember(Value = "VISA_TAV")]
    VisaTav,

    /// <summary>
    /// Controle remoto seguro digital Mastercard
    /// </summary>
    [EnumMember(Value = "MC_DSRP_LONG")]
    McDsrpLong
}
