
namespace Getnet.Entities.Request;

/// <summary>
/// /// Representa os dados que precisa ser passados para gerar o token de autenticação.
/// </summary>
public class Token
{
    /// <summary>
    /// Identificador do cliente.
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// Senha do cliente.
    /// </summary>
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// Identificador do escopo.
    /// </summary>
    public string Scope { get; set; } = "oob";

    /// <summary>
    /// Tipo de autenticação.
    /// </summary>
    public string GrantType { get; set; } = "client_credentials";
}
