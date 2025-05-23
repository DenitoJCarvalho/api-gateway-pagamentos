
using System.Text.Json.Serialization;

namespace Getnet.Entities.Response;

/// <summary>
/// Representa a resposta do token de autenticação da Getnet.
/// </summary>

public class TokenResponse
{
    /// <summary>
    /// Token de acesso gerado pela Getnet.
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Tipo do token gerado pela Getnet.
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = string.Empty;

    /// <summary>
    /// Tempo de expiração do token em segundos.
    /// </summary>
    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }

    /// <summary>
    /// Escopo do token gerado pela Getnet.
    /// </summary>
    [JsonPropertyName("scope")]
    public string Scope { get; set; } = string.Empty;
}
