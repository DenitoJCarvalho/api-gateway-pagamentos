

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Getnet.Entities.Dto;
using Getnet.Enums;

namespace Getnet.Controllers.Dtos.Common;

public class CreditDto
{

    /// <summary>
    /// Tipo de transação.
    /// </summary>
    [JsonPropertyName("transaction_type")]
    public TransactionType TransactionType { get; set; }

    /// <summary>
    /// Merchant Payment Gateway ID.
    /// </summary>
    [JsonPropertyName("gateway_id")]
    public string GatewayId { get; set; } = string.Empty;

    /// <summary>
    /// Número de parcelas para uma transação de crédito parcelado.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("number_installments")]
    public int NumberInstallments { get; set; }

    /// <summary>
    /// Texto exibido na fatura do cartão do comprador.
    /// </summary>
    [JsonPropertyName("soft_descriptor")]
    public string? SoftDescriptor { get; set; }

    /// <summary>
    /// Campo utilizado para sinalizar a transação com outro Merchant Category Code (Código da Categoria do Estabelecimento) diferente do cadastrado.
    /// </summary>
    [JsonPropertyName("dynamic_mcc")]
    public int? DynamicMcc { get; set; }

    /// <summary>
    /// Conjunto de dados do cartão.
    /// </summary>
    [Required(ErrorMessage = "Campo obrigatório.")]
    [JsonPropertyName("card")]
    public CardVerificationDto Card { get; set; } = new CardVerificationDto { };

    [JsonPropertyName("tokenization")]
    public TokenizationDto Tokenization { get; set; } = new TokenizationDto { };

    /// <summary>
    /// Tipo de COF (Credential On File)
    /// </summary>
    [JsonPropertyName("credentials_on_file_type")]
    public CredentialsOnFileType? CredentialsOnFileType { get; set; }

    /// <summary>
    /// Código de uma transação feita no passado com o mesmo cartão e que foi efetivada com sucesso, que será usada pelo COF (Credential On File).
    /// </summary>
    [JsonPropertyName("transaction_id")]
    public string TransactionId { get; set; } = string.Empty;

}
