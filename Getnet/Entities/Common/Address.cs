
using System.Text.Json.Serialization;

namespace Getnet.Entities.Commom;

/// <summary>
/// Conjunto de dados referentes ao endereço de entrega de cobrança.
/// </summary>
public class Address
{
    /// <summary>
    /// Logradouro.
    /// </summary>
    [JsonPropertyName("street")]
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Número do logradouro.
    /// </summary>
    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Complemento do logradouro.
    /// </summary>
    [JsonPropertyName("complement")]
    public string Complement { get; set; } = string.Empty;

    /// <summary>
    /// Bairro.
    /// </summary>
    [JsonPropertyName("district")]
    public string District { get; set; } = string.Empty;

    /// <summary>
    /// Cidade.
    /// </summary>
    [JsonPropertyName("city")]
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Estado(UF).
    /// </summary>
    [JsonPropertyName("state")]
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// País.
    /// </summary>
    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Código Postal, CEP no Brasil ou ZIP nos Estados Unidos. (sem máscara)
    /// </summary>
    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; } = string.Empty;
}
