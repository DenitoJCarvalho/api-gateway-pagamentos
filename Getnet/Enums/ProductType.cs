
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Getnet.Infrastructure.Configurations.Serialization;

namespace Getnet.Enums;

/// <summary>
/// Identificador do tipo de produto vendido.
/// </summary>
[JsonConverter(typeof(JsonEnumMemberConverter<ProductType>))]
public enum ProductType
{
    /// <summary>
    /// Pordutos comprados em loja física, geralmente com retirada imediata.
    /// </summary>
    [EnumMember(Value = "cash_carry")]
    CashCarry,

    /// <summary>
    /// Conteúdo digital individual, normalmente protegido por direitos autorais.
    /// </summary>
    [EnumMember(Value = "digital_content")]
    DigitalContent,


    /// <summary>
    /// Bens digitais que não são conteúdos artísticos, mas sim utilitários.
    /// </summary>
    [EnumMember(Value = "digital_goods")]
    DigitalGoods,

    /// <summary>
    /// Produtos com uma parte digital e uma física integradas.
    /// </summary>
    [EnumMember(Value = "digital_physical")]
    DigitalPhysical,

    /// <summary>
    /// Cartões presente ou vouchers digitais/físicos com valor armazenado.
    /// </summary>
    [EnumMember(Value = "gift_card")]
    GiftCard,

    /// <summary>
    /// Produtos físicos enviados ao cliente.
    /// </summary>
    [EnumMember(Value = "physical_goods")]
    PhysicalGoods,

    /// <summary>
    /// Renovação de uma  assinatura de serviço. 
    /// </summary>
    [EnumMember(Value = "renew_subs")]
    RenewSubs,

    /// <summary>
    /// Software que pode ser testado gratuitamente por um tempo e depois precisa ser comprado.
    /// </summary>
    [EnumMember(Value = "shareware")]
    Shareware,

    /// <summary>
    /// Prestação de serviços em geral, sem entrega de produtos físicos ou digitais.
    /// </summary>
    [EnumMember(Value = "service")]
    Service
}
