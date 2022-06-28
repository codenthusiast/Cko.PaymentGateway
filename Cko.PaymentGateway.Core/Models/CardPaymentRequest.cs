using System.ComponentModel.DataAnnotations;

namespace Cko.PaymentGateway.Core.Models
{
    public class CardPaymentRequest : PaymentRequest
    {
        [Required]
        [CreditCard]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "Invalid card number")]
        public string CardNumber { get; set; }
        [Required]
        [RegularExpression("0[1-9]|1[0-2]", ErrorMessage = "Invalid expiry month")]        
        public string ExpiryMonth { get; set; }
        [Required]
        [RegularExpression("^(0?[1-9]|[1-9][0-9])$", ErrorMessage = "Invalid expiry year")]
        public string ExpiryYear { get; set; }
        [Required]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "Invalid expiry Cvv")]
        public string Cvv { get; set; }
    }
}
