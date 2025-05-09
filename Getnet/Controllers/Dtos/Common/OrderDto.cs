
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Getnet.Enums;

namespace Getnet.Controllers.Dtos.Common;

/// <summary>
/// Conjunto de dados para identificação da compra.
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Código de identificação de compra utlilizado pelo e-commerce 
    /// </summary>
    [Length(1, 36, ErrorMessage = "Campo precisa ter entre 1 e 36 caracteres.")]
    [JsonPropertyName("order_id")]
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Valor de imposto.
    /// </summary>
    [JsonPropertyName("sales_tax")]
    public int? SalesTax { get; set; }

    /// <summary>
    /// Identificador do tipo de produto vendido dentre as opções.
    /// Valores válidos: cash_carry, digital_content, digital_goods, digital_physical, gift_card, physical_goods, renew_subs, shareware, service.
    /// </summary>
    [JsonPropertyName("product_type")]
    public ProductType ProductType { get; set; }
}
