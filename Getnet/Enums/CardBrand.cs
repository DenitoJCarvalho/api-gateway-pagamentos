

using System.Runtime.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Bandeiras de cartão aceito pela Getnet.
/// </summary>
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
