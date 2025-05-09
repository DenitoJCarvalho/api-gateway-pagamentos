
using System.Text.Json.Serialization;
using Getnet.Enums;

namespace Getnet.Entities.Commom;

/// <summary>
/// Para que a Getnet possa cumprir com as regras das Bandeiras e Arranjos, as Leis Federais e determinações do BACEN (Banco Central do Brasil) para identificação das entidades finais (subcomércios) que fazem as transações financeiras, os Facilitadores de Pagamento devem enviar os dados de identificação de seus clientes a cada transação enviada à Getnet.
/// </summary>
public class SubMerchant
{
    /// <summary>
    /// ID do sub comércio.
    /// </summary>
    [JsonPropertyName("identification_code")]
    public string IdentificationCode { get; set; } = string.Empty;

    /// <summary>
    /// Nome do recebedor ou Razão Social do Subcomércio. (Deve corresponder ao cadastro existente no site gov.br)
    /// </summary>
    [JsonPropertyName("business_name")]
    public string BusinessName { get; set; } = string.Empty;

    /// <summary>
    /// CNPJ ou CPF do Subcomércio.
    /// </summary>
    [JsonPropertyName("document_type")]
    public DocumentType DocumentType { get; set; }

    /// <summary>
    /// CNPJ ou CPF do Subcomércio.
    /// </summary>
    [JsonPropertyName("document_number")]
    public string DocumentNumber { get; set; } = string.Empty;

    /// <summary>
    /// Tipos de Submerchants.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ForeignType ForeignType { get; set; }

    /// <summary>
    /// Logradouro do Subcomércio
    /// </summary>
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Cidade do Subcomércio.
    /// </summary>
    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Estado do Subcomércio.
    /// </summary>
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// Código Postal (CEP) do Subcomércio.
    /// </summary>
    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; } = string.Empty; 
} 
