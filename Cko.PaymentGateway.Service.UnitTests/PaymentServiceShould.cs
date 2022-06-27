using AutoFixture;
using Cko.PaymentGateway.Core.AppExceptions;
using Cko.PaymentGateway.Core.Contracts;
using Cko.PaymentGateway.Core.Entities;
using Cko.PaymentGateway.Core.Models;
using Cko.PaymentGateway.Core.Repository;
using Cko.PaymentGateway.Service.DTOs;
using Cko.PaymentGateway.Service.Services;
using Cko.PaymentGateway.Service.ThirdParty;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Cko.PaymentGateway.Service.UnitTests
{
    public class PaymentServiceShould
    {
        private readonly Fixture _fixture;
        private readonly IBaseRepository<Transaction> _transactionRepository = Mock.Of<IBaseRepository<Transaction>>();
        private readonly IPaymentService _sut;
        private readonly IUnitOfWork _unitOfWork = Mock.Of<IUnitOfWork>();
        private readonly IAcquiringBankClient _bankClient = Mock.Of<IAcquiringBankClient>();
        public PaymentServiceShould()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            _sut = new PaymentService(_unitOfWork, _bankClient);
        }

        [Fact]
        public async Task Throw_PaymentNotFoundException_WhenPayment_DoesntExist()
        {
            Mock.Get(_transactionRepository).Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                                         .ReturnsAsync(default(Transaction));

            Mock.Get(_unitOfWork).Setup(x => x.TransactionRepository)
                      .Returns(_transactionRepository);

            Func<Task> act = async () => await _sut.GetPaymentStatus(Guid.NewGuid());
            await act.Should().ThrowAsync<PaymentNotFoundException>();
        }        
        
        [Fact]
        public async Task NotThrow_PaymentNotFoundException_WhenPayment_Exists()
        {
            var transaction = _fixture.Create<Transaction>();
            Mock.Get(_transactionRepository).Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                                         .ReturnsAsync(transaction);

            Mock.Get(_unitOfWork).Setup(x => x.TransactionRepository)
                      .Returns(_transactionRepository);

            Func<Task> act = async () => await _sut.GetPaymentStatus(Guid.NewGuid());
            await act.Should().NotThrowAsync<PaymentNotFoundException>();
        }   
        
        [Fact]
        public async Task Return_PaymentResponse_When_ProcessPayment_IsCalled()
        {
            var request = _fixture.Build<CardPaymentRequest>()
                                  .With(x => x.CardNumber, "5555555555554444")
                                  .Create();
            var responseFromBank = _fixture.Create<CardPaymentReponseDTO>();

            Mock.Get(_unitOfWork).Setup(x => x.TransactionRepository)
                      .Returns(_transactionRepository);
            Mock.Get(_bankClient).Setup(x => x.ProcessCardTransaction(It.IsAny<CardPaymenRequestDTO>()))
                .ReturnsAsync(responseFromBank);
            var response = await _sut.ProcessCardPayment(request);

            using var scope =  new AssertionScope();
            response.Should().NotBeNull();
            response.Should().Match<ProcessPaymentResponse>(x => x.CardNumber == "************4444");
        }

    }
}
