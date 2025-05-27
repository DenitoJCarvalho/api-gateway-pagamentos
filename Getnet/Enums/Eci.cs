using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Getnet.Infrastructure.Configurations.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Indicador de Comércio Eletrônico (ECI - Electronic Commerce Indicator).
/// </summary>
[JsonConverter(typeof(JsonEnumMemberConverter<Eci>))]
public enum Eci
{
    /// <summary>
    /// Usado quando o cartão é autenticado com 3D Secure 1.0/2.0 (com senha, biometria, etc).
    /// </summary>
    [EnumMember(Value = "05")]
    ECI05,

    /// <summary>
    /// Usado quando o emissor não exige autenticação, mas a tentativa foi feita.
    /// </summary>
    [EnumMember(Value = "06")]
    ECI06,

    /// <summary>
    /// Usado quando nenhuma autenticação foi feita, geralmente para transações com tokenização simples ou fallback.
    /// </summary>
    [EnumMember(Value = "07")]
    ECI07
}
