
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Getnet.Controllers.Dtos;

 /// <summary>
/// Representa os dados que precisa ser passados para gerar o token do cartão.
/// </summary>
public class TokenCardDto
{
    /// <summary>
    /// Número do cartão.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [Length(13, 19, ErrorMessage = "Número do cartão deve ter entre 13 e 19 dígitos")]
    [JsonPropertyName("card_number")]
    public string CardNumber { get; set; } = string.Empty;

    /// <summary>
    /// É a identificação que você utiliza para o seu cliente (E-mail, CPF, ID do Cliente, entre  outros).
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [Length(1, 100, ErrorMessage = "Campo deve ter entre 1 e 100 caracteres")]
    [JsonPropertyName("customer_id")]
    public string CustomerId { get; set; } = string.Empty;

}
