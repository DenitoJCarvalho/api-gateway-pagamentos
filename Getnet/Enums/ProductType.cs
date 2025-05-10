using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Identificador do tipo de produto vendido.
/// </summary>
[JsonConverter(typeof(StringEnumConverter))]
public enum ProductType
{
    [EnumMember(Value = "cash_carry")]
    CashCarry,

    [EnumMember(Value = "digital_goods")]
    DigitalGoods,

    [EnumMember(Value = "digital_physical")]
    DigitalPhysical,

    [EnumMember(Value = "gift_card")]
    GiftCard,

    [EnumMember(Value = "physical_goods")]
    PhysicalGoods,

    [EnumMember(Value = "renew_subs")]
    RenewSubs,

    [EnumMember(Value = "shareware")]
    Shareware,

    [EnumMember(Value = "service")]
    Service
}
