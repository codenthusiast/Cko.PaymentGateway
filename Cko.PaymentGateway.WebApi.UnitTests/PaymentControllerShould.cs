using AutoFixture;
using Cko.PaymentGateway.Core.Contracts;
using Cko.PaymentGateway.Core.Models;
using Cko.PaymentGateway.WebApi.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using Cko.PaymentGateway.Core.AppExceptions;

namespace Cko.PaymentGateway.WebApi.UnitTests
{
    public class PaymentControllerShould
    {
        private readonly Fixture _fixture;
        private readonly PaymentController _sut;
        private readonly IPaymentService _paymentService = Mock.Of<IPaymentService>();
        private readonly ILogger<PaymentController> _logger = Mock.Of<ILogger<PaymentController>>();
        public PaymentControllerShould()
        {
            _fixture = new Fixture();
            _sut = new PaymentController(_paymentService, _logger);
        }

        [Fact]
        public async Task Return_Created_When_PaymentIsApproved()
        {
            var paymentResponse = _fixture.Build<ProcessPaymentResponse>()
                                    .With(x => x.Status, "approved")
                                    .Create();
            var paymentRequest = _fixture.Create<CardPaymentRequest>();

            Mock.Get(_paymentService).Setup(x => x.ProcessCardPayment(It.IsAny<CardPaymentRequest>())).ReturnsAsync(paymentResponse);
            var response = await _sut.Process(paymentRequest);

            response.As<CreatedAtRouteResult>()
                    .Value
                    .Should()
                    .BeOfType<ProcessPaymentResponse>()
                    .And
                    .Match<ProcessPaymentResponse>(x => x.IsApproved == true && x.Status == "approved");

        }

        [Fact]
        public async Task Return_BadRequest_When_PaymentNotApproved()
        {
            var paymentResponse = _fixture.Build<ProcessPaymentResponse>()
                                    .With(x => x.Status, "declined")
                                    .With(x => x.IsApproved, false)
                                    .Create();
            var paymentRequest = _fixture.Create<CardPaymentRequest>();

            Mock.Get(_paymentService).Setup(x => x.ProcessCardPayment(It.IsAny<CardPaymentRequest>())).ReturnsAsync(paymentResponse);
            var response = await _sut.Process(paymentRequest);

            response.As<BadRequestObjectResult>()
                    .Value
                    .Should()
                    .BeOfType<ProcessPaymentResponse>()
                    .And
                    .Match<ProcessPaymentResponse>(x => x.IsApproved == false && x.Status == "declined");

        }

        [Fact]
        public async Task Return_Ok_When_PaymentIsFound()
        {
            var paymentResponse = _fixture.Build<ProcessPaymentResponse>()
                                    .With(x => x.Status, "declined")
                                    .With(x => x.IsApproved, false)
                                    .Create();

            Mock.Get(_paymentService).Setup(x => x.GetPaymentStatus(It.IsAny<Guid>())).ReturnsAsync(paymentResponse);
            var response = await _sut.Status(Guid.NewGuid());

            response.As<OkObjectResult>()
                    .Value
                    .Should()
                    .NotBeNull()
                    .And
                    .BeOfType<ProcessPaymentResponse>();
        }

        [Fact]
        public async Task Return_NotFound_When_PaymentNotFoundException_IsThrown()
        {

            Mock.Get(_paymentService).Setup(x => x.GetPaymentStatus(It.IsAny<Guid>())).ThrowsAsync(new PaymentNotFoundException());
            var response = await _sut.Status(Guid.NewGuid());

            response.As<NotFoundResult>()
                    .StatusCode
                    .Should()
                    .Be(404);
        }
    }
}
