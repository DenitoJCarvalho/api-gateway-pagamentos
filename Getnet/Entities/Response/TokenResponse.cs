
namespace Getnet.Entities.Response;

/// <summary>
/// Representa a resposta do token de autenticação da Getnet.
/// </summary>

public class TokenResponse
{
    /// <summary>
    /// Token de acesso gerado pela Getnet.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Tipo do token gerado pela Getnet.
    /// </summary>
    public string TokenType { get; set; } = string.Empty;

    /// <summary>
    /// Tempo de expiração do token em segundos.
    /// </summary>
    public int ExpiresIn { get; set; }

    /// <summary>
    /// Escopo do token gerado pela Getnet.
    /// </summary>
    public string Scope { get; set; } = string.Empty;
}
