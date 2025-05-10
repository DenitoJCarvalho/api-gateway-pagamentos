using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Tipo de documento.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentType
{
    [EnumMember(Value = "CPF")]
    CPF,

    [EnumMember(Value = "CNPJ")]
    CNPJ
}
