using Cko.PaymentGateway.Service.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cko.BankSimulator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(ILogger<PaymentController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("process")]
        public ActionResult<CardPaymentReponseDTO> ProcessPayment(CardPaymenRequestDTO dto)
        {
            var status = GetRandomStatus();
            if (status == "approved")
            {
                var response = new CardPaymentReponseDTO
                {
                    PaymentReference = Guid.NewGuid(),
                    IsApproved = true,
                    Status = status
                };
                return CreatedAtRoute(null, response);
            }
            else
            {
                var response = new CardPaymentReponseDTO
                {
                    PaymentReference = Guid.NewGuid(),
                    IsApproved = false,
                    Status = status
                };
                return BadRequest(response);
            }
        }

        private static string GetRandomStatus()
        {
            //HACK: simulate random, biased status
            var paymentStatuses = new string[]
                                {
                                    "declined",
                                    "declined",
                                    "declined",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "approved",
                                    "fraud_detected",
                                    "fraud_detected",
                                    "fraud_detected",
                                    "fraud_detected",
                                };
            var statusIndex = Random.Shared.Next(paymentStatuses.Length);
            var paymentStatus = paymentStatuses[statusIndex];
            return paymentStatus;
        }
    }
}
