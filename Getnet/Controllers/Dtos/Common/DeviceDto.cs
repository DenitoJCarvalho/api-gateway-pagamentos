

using System.Text.Json.Serialization;

namespace Getnet.Controllers.Dtos.Common;

public class DeviceDto
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
