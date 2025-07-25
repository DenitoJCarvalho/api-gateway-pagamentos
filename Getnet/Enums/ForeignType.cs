using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Getnet.Infrastructure.Configurations.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Tipos de subcomércio.
/// </summary>
[JsonConverter(typeof(JsonEnumMemberConverter<ForeignType>))]
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
