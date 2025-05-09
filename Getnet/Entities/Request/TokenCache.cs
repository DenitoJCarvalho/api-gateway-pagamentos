

namespace Getnet.Entities.Request;

public class TokenCache
{
    public string AccessToken { get; set; } = string.Empty;

    public DateTime Expiration { get; set; } = DateTime.MinValue;    
}
