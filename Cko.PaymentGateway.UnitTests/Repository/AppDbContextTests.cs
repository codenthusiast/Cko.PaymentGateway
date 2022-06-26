using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Cko.PaymentGateway.Data.UnitTests.Repository
{
    public class AppDbContextTests : SqliteTestSetup
    {
        [Fact]
        public async Task DatabaseIsAvailableAndCanBeConnectedTo()
        {
            var canConnect = await _dbContext.Database.CanConnectAsync();
            canConnect.Should().BeTrue();
        }
    }
}
