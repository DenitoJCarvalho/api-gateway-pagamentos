
namespace Getnet.Exceptions;


/// <summary>
/// Classe de exceção personalizada para erros da API do Getnet.
/// </summary>
public class GetnetApiExceptions : ApplicationException
{
    #region Propriedades
    /// <summary>
    /// Código de status HTTP retornado pela API.
    /// Este código pode ser usado para identificar o tipo de erro ocorrido.
    /// </summary>
    public int StatusCode { get; }
    public string? ErrorContent { get; }

    #endregion

    #region Construtores
    public GetnetApiExceptions(string message, int statusCode, string? errorContent = null) : base(message)
    {
        StatusCode = statusCode;
        ErrorContent = errorContent;
    }
    #endregion
}
