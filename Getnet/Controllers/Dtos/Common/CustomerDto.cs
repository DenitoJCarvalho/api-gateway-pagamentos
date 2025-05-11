

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Getnet.Enums;

namespace Getnet.Controllers.Dtos.Common;


/// <summary>
/// Conjunto de dados referentes ao comprador.
/// </summary>
public class CustomerDto
{
    /// <summary>
    /// Identificador do comprador.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1, 100, ErrorMessage = "Campo deve ter entre 1 e 100 caracteres.")]
    [JsonPropertyName("customer_id")]
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// Primeiro nome do comprador.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1, 40, ErrorMessage = "Campo deve ter entre 1 e 40 caracteres.")]
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Último nome do comprador.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1, 80, ErrorMessage = "Campo deve ter entre 1 e 80 caracteres.")]
    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Nome completo do comprador.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1, 100, ErrorMessage = "CAmpo deve ter entre 1 e 100 caracteres.")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email do comprador.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [EmailAddress(ErrorMessage = "Por favor informe um e-mail válido.")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Tipo do documento de identificação do comprador.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1, 26, ErrorMessage = "Campo deve ter entre 1 e 26 caracteres.")]
    [JsonPropertyName("document_type")]
    public DocumentType DocumentType { get; set; } 

    /// <summary>
    /// Número do documento do comprador sem pontuação.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(11, 15, ErrorMessage = "Campo deve ter entre 11 e 15 caracteres.")]
    [JsonPropertyName("document_number")]
    public string DocumentNumber { get; set; } = string.Empty;

    /// <summary>
    /// Telefone do comprador. (sem máscara)
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Phone(ErrorMessage = "Por favor informe um telefone válido.")]
    [Length(10, 15, ErrorMessage = "Telefone deve ter entre 10 e 15 caracteres.")]
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Conjunto de dados referentes ao endereço de cobrança.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("billing_address")]
    public AddressDto BillingAddress { get; set; } = new AddressDto { };
    
}
