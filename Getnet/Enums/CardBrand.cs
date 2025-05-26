
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Getnet.Infrastructure.Configurations.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Bandeiras de cartão aceito pela Getnet.
/// </summary>
[JsonConverter(typeof(JsonEnumMemberConverter<CardBrand>))]
public enum CardBrand
{
    [EnumMember(Value = "Mastercard")]
    Mastercard,

    [EnumMember(Value = "Visa")]
    Visa,

    [EnumMember(Value = "Amex")]
    Amex,

    [EnumMember(Value = "Elo")]
    Elo,

    [EnumMember(Value = "Hipercard")]
    Hipercard

}
