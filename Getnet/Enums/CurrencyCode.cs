using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Getnet.Infrastructure.Configurations.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Códigos de moeda utillizados em transações (ISO4217).
/// </summary>
[JsonConverter(typeof(JsonEnumMemberConverter<CurrencyCode>))]
public enum CurrencyCode
{
    [EnumMember(Value = "BRL")]
    BRL
}
