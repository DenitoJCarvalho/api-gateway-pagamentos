using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Getnet.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ForeignType
{
    /// <summary>
    /// Todos os submerchants são internacionais.
    /// </summary>
    [EnumMember(Value = "F")]
    FullForeign = 'F',

    /// <summary>
    /// Carrinho misto com submerchants internacionais.
    /// </summary>
    [EnumMember(Value = "P")]
    PartialForeign = 'P',

    /// <summary>
    /// Todos os submerchants são nacionais
    /// </summary>
    [EnumMember(Value = "D")]
    FullDomestic = 'D'
}
