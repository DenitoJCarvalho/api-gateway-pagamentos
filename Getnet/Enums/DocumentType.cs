using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Getnet.Infrastructure.Configurations.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Tipo de documento.
/// </summary>
[JsonConverter(typeof(JsonEnumMemberConverter<DocumentType>))]
public enum DocumentType
{
    [EnumMember(Value = "CPF")]
    CPF,

    [EnumMember(Value = "CNPJ")]
    CNPJ
}
