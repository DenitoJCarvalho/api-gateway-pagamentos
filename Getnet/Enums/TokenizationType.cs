using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Tipo de criptograma utilizado pelo TSP (Token Service Provider).
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TokenizationType
{
    /// <summary>
    /// UCAF - Universal Cardholder Authentication Filed
    /// Campo padrão usado para transportar dados de autenticação do titular do cartão em transações online.
    /// </summary>
    [EnumMember(Value = "UCAF")]
    UCAF,

    /// <summary>
    /// TAVV - Token Authentication Verification Value
    /// Uma versão do CAVV (Cardholder Authentication Verification Value) usado em ambientes de tokenização.
    /// </summary>
    [EnumMember(Value = "TAVV")]
    TAVV
}
