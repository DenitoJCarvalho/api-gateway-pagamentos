
using System.Text.Json.Serialization;

namespace Getnet.Infrastrucutre.Configurations.Getnet;

public class GetnetSettings
{
    public string ClienteId { get; set; } = string.Empty;
    public string ClienteSecret { get; set; } = string.Empty;

    [JsonPropertyName("HostHomologacao")]
    public string HostHomologacao { get; set; } = string.Empty;

    [JsonPropertyName("HostProducao")]
    public string HostProducao { get; set; } = string.Empty;

    public string SellerId { get; set; } = string.Empty;
    
}
