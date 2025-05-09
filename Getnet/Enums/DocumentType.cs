using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Getnet.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentType
{
    [EnumMember(Value = "CPF")]
    CPF,

    [EnumMember(Value = "CNPJ")]
    CNPJ
}
