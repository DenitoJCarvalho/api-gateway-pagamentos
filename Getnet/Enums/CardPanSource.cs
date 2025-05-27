
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Getnet.Infrastructure.Configurations.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Origem do PAN do cartão.
/// </summary>
[JsonConverter(typeof(JsonEnumMemberConverter<CardPanSource>))]
public enum CardPanSource
{
    /// <summary>
    /// O solicitante do token já possui os dados PAN em seus registros.
    /// </summary>
    [EnumMember(Value = "ON_FILE")]
    OnFile,

    /// <summary>
    /// Os dados do PAN foram digitados pelo titular do cartão.
    /// </summary>
    [EnumMember(Value = "MANUALLY_ENTERED")]
    ManuallyEntered,

    /// <summary>
    /// Os dados PAN (ou referência) foram fornecidos por um Aplicativo Emissor.
    /// </summary>
    [EnumMember(Value = "VIA_APPLICATION")]
    ViaApplication,

}
