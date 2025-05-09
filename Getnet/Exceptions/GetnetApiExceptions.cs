
namespace Getnet.Exceptions;


/// <summary>
/// Classe de exceção personalizada para erros da API do Getnet.
/// </summary>
public class GetnetApiExceptions : Exception
{
    #region Propriedades
    /// <summary>
    /// Código de status HTTP retornado pela API.
    /// Este código pode ser usado para identificar o tipo de erro ocorrido.
    /// </summary>
    public int StatusCode { get; }

    #endregion
    
    #region Construtores
    public GetnetApiExceptions(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
    #endregion
}
