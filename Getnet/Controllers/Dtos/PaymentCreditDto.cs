
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Getnet.Controllers.Dtos.Common;
using Getnet.Enums;

namespace Getnet.Controllers.Dtos;


public class PaymentCreditDto
{
    /// <summary>
    /// Código de indentificação do e-commerce.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [Length(36, 36, ErrorMessage = "Campo deve ter 36 caracteres.")]
    [JsonPropertyName("seller_id")]
    public string SellerId { get; set; } = string.Empty;

    /// <summary>
    /// Valor da compra em centavos.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    /// <summary>
    /// Identificação da moeda.
    /// </summary>
    [Length(3, 3, ErrorMessage = "Campo deve ter 3 caracteres.")]
    [JsonPropertyName("currency")]
    public CurrencyCode Currency { get; set; } = CurrencyCode.BRL;

    /// <summary>
    /// Conjunto de dados para identificação da compra.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("order")]
    public OrderDto Order { get; set; } = new OrderDto { };

    /// <summary>
    /// Conjunto de dados referentes ao comprador.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("customer")]
    public CustomerDto Customer { get; set; } = new CustomerDto { };


    /// <summary>
    /// Conjunto de dados referentes a transação de crédito.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("credit")]
    public CreditDto Credit { get; set; } = new CreditDto { };

    /// <summary>
    /// Conjunto de dados referentes ao dispositivo utilizado pelo comprador.
    /// </summary>
    [JsonPropertyName("device")]
    public DeviceDto? Device { get; set; }

    /// <summary>
    /// Conjunto de dados referentes ao endereço de entrega.
    /// </summary>
    [JsonPropertyName("shippings")]
    public List<ShippingDto>? Shippings { get; set; }
    
    /// <summary>
    /// Identificação das entidades finais (subcomércio) que fazerm transações financeiras.
    /// </summary>
    [JsonPropertyName("sub_merchant")]
    public SubMerchantDto? SubMerchant { get; set; }
}
