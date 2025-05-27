

using System.Text.Json.Serialization;
using Getnet.Enums;

namespace Getnet.Entities.Request;

public class CardCryptogram
{
    /// <summary>
    /// Token obtido em GetTokenFlag.
    /// </summary>
    [JsonPropertyName("network_token_id")]
    public string NetworkTokenId { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de critptografia.
    /// </summary>
    [JsonPropertyName("transaction_type")]
    public CryptogramTransactionType TransactionType { get; set; } 

    /// <summary>
    /// Identificador da Bandeira do cartão.
    /// </summary>
    [JsonPropertyName("cryptogram_type")]
    public CryptogramType CryptogramType { get; set; } 

    /// <summary>
    /// Valor monetário da transação em centavos
    /// </summary>
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    /// <summary>
    /// É a identificação que você utiliza para o seu cliente (E-mail, CPF, ID do Cliente, entre  outros).
    /// </summary>
    [JsonPropertyName("customer_id")]
    public string CustomerId { get; set; } = string.Empty;

    /// <summary>
    /// Email do comprador.
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

      /// <summary>
    /// Identificador da Bandeira do cartão.
    /// Valores possíveis: VISA e MASTERCARD.
    /// </summary>
    [JsonPropertyName("card_brand")]
    public CardBrand CardBrand { get; set; } 
}
