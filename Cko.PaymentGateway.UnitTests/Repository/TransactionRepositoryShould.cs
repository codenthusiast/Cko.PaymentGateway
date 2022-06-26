using AutoFixture;
using Cko.PaymentGateway.Core.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cko.PaymentGateway.Data.UnitTests.Repository
{
    public class TransactionRepositoryShould : SqliteTestSetup
    {
        private readonly Fixture _fixture;
        public TransactionRepositoryShould() : base()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task Create_Transaction()
        {
            var transaction = _fixture.Build<Transaction>()
                                      .Without(x => x.Id)
                                      .Create();

            var repository = _unitOfWork.TransactionRepository;
            repository.Add(transaction);
            await _unitOfWork.SaveAsync();

            var getTransaction = await repository.GetByIdAsync(transaction.Id);

            using var assertionScope = new AssertionScope();
            getTransaction.Should().NotBeNull();
            getTransaction.Should().Match<Transaction>(x => transaction.Id == x.Id
                                                            && transaction.Amount == x.Amount
                                                            && transaction.CardNumber == x.CardNumber);
        }

        [Fact]
        public async Task Update_Transaction()
        {
            var transaction = _fixture.Build<Transaction>()
                                      .Without(x => x.Id)
                                      .Create();

            var repository = _unitOfWork.TransactionRepository;
            repository.Add(transaction);
            await _unitOfWork.SaveAsync();

            var newCardNumber = _fixture.Create<string>();
            transaction.CardNumber = newCardNumber;
            repository.Update(transaction.Id, transaction);
            await _unitOfWork.SaveAsync();
            var updatedTransaction = await repository.GetByIdAsync(transaction.Id);

            updatedTransaction.Should().Match<Transaction>(x => transaction.Id == x.Id
                                                            && transaction.Amount == x.Amount
                                                            && transaction.CardNumber == x.CardNumber
                                                            && transaction.CardNumber == newCardNumber);
        }

        [Fact]
        public async Task Delete_Transaction()
        {
            var transaction = _fixture.Create<Transaction>();
            var repository = _unitOfWork.TransactionRepository;
            repository.Add(transaction);

            await _unitOfWork.SaveAsync();
            repository.Remove(transaction);
            await _unitOfWork.SaveAsync();

            var deletedTransaction = await repository.GetByIdAsync(transaction.Id);
            deletedTransaction.Should().BeNull();
        }

        [Fact]
        public async Task Return_AllTransactions()
        {
            var transactions = _fixture.CreateMany<Transaction>(5);
            var repository = _unitOfWork.TransactionRepository;
            foreach (var transaction in transactions)
            {
                repository.Add(transaction);
            }
            await _unitOfWork.SaveAsync();

            var allTransactions = await repository.GetAllAsync().ToListAsync();

            using var assertionScope = new AssertionScope();
            allTransactions.Should().HaveCount(5);
            allTransactions.Should().BeEquivalentTo(transactions);
        }

        [Fact]
        public async Task Return_PagedTransactions()
        {
            var transactions = _fixture.CreateMany<Transaction>(5);
            var repository = _unitOfWork.TransactionRepository;
            foreach (var transaction in transactions)
            {
                repository.Add(transaction);
            }
            await _unitOfWork.SaveAsync();

            var allTransactions = await repository.GetAllAsync(2, 1).ToListAsync();
            allTransactions.Should().HaveCount(2);
        }
    }
}
