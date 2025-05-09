using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Getnet.Controllers.Dtos.Common;
public class AddressDto
{
    /// <summary>
    /// Logradouro.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1,60, ErrorMessage = "Campo deve ter entre 1 a 60 caracteres.")]
    [JsonPropertyName("street")]
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Número do logradouro.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1,10, ErrorMessage = "Campo deve ter entre 1 a 10 caracteres.")]
    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Complemento do logradouro.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1,60, ErrorMessage = "Campo deve ter entre 1 a 60 caracteres.")]
    [JsonPropertyName("complement")]
    public string Complement { get; set; } = string.Empty;

    /// <summary>
    /// Bairro.
    /// </summary>
    [Length(1,40, ErrorMessage = "Campo deve ter entre 1 a 40 caracteres.")]
    [JsonPropertyName("district")]
    public string District { get; set; } = string.Empty;

    /// <summary>
    /// Cidade.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1,40, ErrorMessage = "Campo deve ter entre 1 a 40 caracteres.")]
    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Estado(UF).
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1,20, ErrorMessage = "Campo deve ter entre 1 a 20 caracteres.")]
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// País.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1,20, ErrorMessage = "Campo deve ter entre 1 a 20 caracteres.")]
    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Código Postal, CEP no Brasil ou ZIP nos Estados Unidos. (sem máscara)
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1,8, ErrorMessage = "Campo deve ter entre 1 e 8 caracteres.")]
    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; } = string.Empty;
}
