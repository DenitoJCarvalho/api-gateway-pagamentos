using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace Getnet.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CurrencyCode
{
    [EnumMember(Value = "BRL")]
    BRL = 986
}
