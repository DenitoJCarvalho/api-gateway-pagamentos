
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Getnet.Enums;

namespace Getnet.Controllers.Dtos;

public class CardCryptogramDto
{
    /// <summary>
    /// Token obtido em GetTokenFlag.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [JsonPropertyName("network_token_id")]
    public string NetworkTokenId { get; set; } = string.Empty;

    /// <summary>
    /// Identificador da Bandeira do cartão.
    /// </summary>
    [JsonPropertyName("cryptogram_type")]
    public CryptogramType CryptogramType { get; set; }

    /// <summary>
    /// Valor monetário da transação em centavos
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    /// <summary>
    /// É a identificação que você utiliza para o seu cliente (E-mail, CPF, ID do Cliente, entre  outros).
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [JsonPropertyName("customer_id")]
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// Email do comprador.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório")]
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Identificador da Bandeira do cartão.
    /// </summary>
    [JsonPropertyName("card_brand")]
    public CardBrand CardBrand { get; set; } 
    
}
