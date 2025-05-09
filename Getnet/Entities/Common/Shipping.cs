
using System.Text.Json.Serialization;

namespace Getnet.Entities.Commom;

/// <summary>
/// Conjunto de dados referentes ao endereço de entrega
/// </summary>
public class Shipping
{
    /// <summary>
    /// Primeiro nome do comprador.
    /// </summary>
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = string.Empty;

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
    /// Telefone do comprador. (sem mascará)
    /// </summary>
    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Valor do frete em centavos.
    /// </summary>
    [JsonPropertyName("shipping_amount")]
    public int ShippingAmount { get; set; }

    /// <summary>
    /// Conjunto de dados referentes ao endereço do comprador.
    /// </summary>
    [JsonPropertyName("address")]
    public List<Address> Address { get; set; } = new List<Address> { }; 
}
