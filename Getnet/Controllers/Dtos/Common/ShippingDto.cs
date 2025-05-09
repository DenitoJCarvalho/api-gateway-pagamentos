
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Getnet.Controllers.Dtos.Common;
    public class ShippingDto
    {
        /// <summary>
        /// Primeiro nome do comprador.
        /// </summary>
        [Length(1,40, ErrorMessage = "Campo deve ter entre 1 a 40 caracteres.")]
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Nome completo do comprador.
        /// </summary>
        [Length(1,100, ErrorMessage = "Campo deve ter entre 1 a 40 caracteres.")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Email do comprador.
        /// </summary>
        [EmailAddress(ErrorMessage = "Por favor informe um e-mail válido.")]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Telefone do comprador. (sem mascará)
        /// </summary>
        [Length(10, 15, ErrorMessage = "Telefone deve ter entre 10 e 15 caracteres.")]
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
        public List<AddressDto> Address { get; set; } = new List<AddressDto> { }; 
    }
