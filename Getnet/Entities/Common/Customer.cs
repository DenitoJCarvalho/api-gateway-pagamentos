

using System.Text.Json.Serialization;
using Getnet.Enums;

namespace Getnet.Entities.Commom;

/// <summary>
/// Conjunto de dados referentes ao comprador.
/// </summary>
public class Customer
{
    /// <summary>
    /// Identificador do comprador.
    /// </summary>
    [JsonPropertyName("customer_id")]
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// Primeiro nome do comprador.
    /// </summary>
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Último nome do comprador.
    /// </summary>
    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Nome completo do comprador.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Email do comprador.
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Tipo do documento de identificação do comprador.
    /// </summary>
    [JsonPropertyName("document_type")]
    public DocumentType DocumentType { get; set; } 

    /// <summary>
    /// Número do documento do comprador sem pontuação.
    /// </summary>
    [JsonPropertyName("document_number")]
    public string DocumentNumber { get; set; } = string.Empty;

    /// <summary>
    /// Telefone do comprador. (sem máscara)
    /// </summary>
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Conjunto de dados referentes ao endereço de cobrança.
    /// </summary>
    [JsonPropertyName("billing_address")]
    public Address BillingAddress { get; set; } = new Address();


}
