
using System.Text.Json.Serialization;

namespace Getnet.Entities.Request;

/// <summary>
/// Representa os dados que precisa ser passados para gerar o token do cartão.
/// </summary>
public class TokenCard
{
    /// <summary>
    /// É a identificação que você utiliza para o seu cliente (E-mail, CPF, ID do Cliente, entre  outros).
    /// </summary>
    [JsonPropertyName("customer_id")]
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// Número do cartão.
    /// </summary>
    [JsonPropertyName("card_number")]
    public string CardNumber { get; set; } = string.Empty;

    /// <summary>
    /// Código de indentificação do e-commerce. Obrigatório quando o cliente for tratado como Plataforma.
    /// Identificação do vendedor na Getnet.
    /// </summary>
    [JsonPropertyName("seller_id")]
    public string SellerId { get; set; } = string.Empty;
}
