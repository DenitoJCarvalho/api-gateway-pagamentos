
using System.Text.Json.Serialization;
using Getnet.Entities.Commom;
using Getnet.Enums;

namespace Getnet.Entities.Request;

public class PaymentCredit
{
    /// <summary>
    /// Código de indentificação do e-commerce.
    /// </summary>
    [JsonPropertyName("seller_id")]
    public string SellerId { get; set; } = string.Empty;

    /// <summary>
    /// Valor da compra em centavos.
    /// </summary>
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    /// <summary>
    /// Identificação da moeda.
    /// </summary>
    [JsonPropertyName("currency")]
    public CurrencyCode Currency { get; set; } = CurrencyCode.BRL;

    /// <summary>
    /// Conjunto de dados para identificação da compra.
    /// </summary>
    [JsonPropertyName("order")]
    public Order Order { get; set; } = new Order { };

    /// <summary>
    /// Conjunto de dados referentes ao comprador.
    /// </summary>
    [JsonPropertyName("customer")]
    public Customer Customer { get; set; } = new Customer();

    /// <summary>
    /// Conjunto de dados referentes a transação de crédito.
    /// </summary>
    [JsonPropertyName("credit")]
    public Credit Credit { get; set; } = new Credit();

    /// <summary>
    /// Conjunto de dados referentes ao dispositivo utilizado pelo comprador.
    /// </summary>
    [JsonPropertyName("device")]
    public Device? Device { get; set; }

    /// <summary>
    /// Conjunto de dados referentes ao endereço de entrega.
    /// </summary>
    [JsonPropertyName("shippings")]
    public List<Shipping>? Shippings { get; set; } 

    /// <summary>
    /// Identificação das entidades finais (subcomércio) que fazerm transações financeiras.
    /// </summary>
    [JsonPropertyName("sub_merchant")]
    public SubMerchant? SubMerchant { get; set; }

}
