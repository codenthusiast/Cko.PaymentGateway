using Cko.PaymentGateway.Core.Contracts;
using Cko.PaymentGateway.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cko.PaymentGateway.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;

        public PaymentController(IPaymentService paymentService, ILogger<PaymentController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }


        [HttpPost]
        [Produces(typeof(ProcessPaymentResponse))]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("process")]
        public async Task<IActionResult> Process(CardPaymentRequest request)
        {
            _logger.LogInformation("Initiating payment request ref: {paymentRef}", request.ExternalReference);
            var response = await _paymentService.ProcessCardPayment(request);
            if (response.IsApproved)
            {
                _logger.LogInformation("Payment approved ref: {paymentRef}", request.ExternalReference);
                return CreatedAtRoute(null, response);
            }
            else
            {
                _logger.LogWarning("Payment unsuccessful: {paymentRef} : status {status}", request.ExternalReference, response.Status);
                return BadRequest(response);
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("status")]
        public async Task<IActionResult> Status(Guid id)
        {
            var response = await _paymentService.GetPaymentStatus(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
    }
}
