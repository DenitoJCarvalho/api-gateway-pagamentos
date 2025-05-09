
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Getnet.Enums;

namespace Getnet.Controllers.Dtos.Common;

public class SubMerchantDto
{
    /// <summary>
    /// ID do sub comércio.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1,15, ErrorMessage = "Campo deve ter entre 1 a 15 caracteres.")]
    [JsonPropertyName("identification_code")]
    public string IdentificationCode { get; set; } = string.Empty;

    /// <summary>
    /// Nome do recebedor ou Razão Social do Subcomércio. (Deve corresponder ao cadastro existente no site gov.br)
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1,80, ErrorMessage = "Campo deve ter entre 1 a 80 caracteres.")]
    [JsonPropertyName("business_name")]
    public string BusinessName { get; set; } = string.Empty;

    /// <summary>
    /// CNPJ ou CPF do Subcomércio.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("document_type")]
    public DocumentType DocumentType { get; set; }

    /// <summary>
    /// CNPJ ou CPF do Subcomércio.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(11,14, ErrorMessage = "Campo deve ter entre 11 a 14 caracteres.")]
    [JsonPropertyName("document_number")]
    public string DocumentNumber { get; set; } = string.Empty;

    /// <summary>
    /// Tipos de Submerchants.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    public ForeignType ForeignType { get; set; }

    /// <summary>
    /// Logradouro do Subcomércio
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1,40, ErrorMessage = "Campo deve ter entre 1 a 40 caracteres.")]
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Cidade do Subcomércio.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1,13, ErrorMessage = "Campo deve ter entre 1 a 13 caracteres.")]
    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Estado do Subcomércio.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(1,2, ErrorMessage = "Campo deve ter entre 1 a 2 caracteres.")]
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Código Postal (CEP) do Subcomércio.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(8,8, ErrorMessage = "Campo deve ter entre 1 a 8 caracteres.")]
    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; } = string.Empty; 
}
