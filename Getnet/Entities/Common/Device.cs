
using System.Text.Json.Serialization;

namespace Getnet.Entities.Commom;

/// <summary>
/// Conjunto  de dados referentes ao dispositivo utilizado pelo comprador.
/// </summary>
public class Device
{
    /// <summary>
    /// Endereço IP (IPv4) do dispositivo do comprador.
    /// </summary>
    [JsonPropertyName("ip_address")]
    public string IpAddress { get; set; } = string.Empty;

    /// <summary>
    /// Hash de identificação (Device Fingerprint) do dispositivo. Mais informações no tópico Antifraude.
    /// </summary>
    [JsonPropertyName("device_id")]
    public string DeviceId { get; set; } = string.Empty;
}
