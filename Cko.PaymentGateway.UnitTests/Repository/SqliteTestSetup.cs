using Cko.PaymentGateway.Core.Repository;
using Cko.PaymentGateway.Data.Repository;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cko.PaymentGateway.Data.UnitTests.Repository
{
    public abstract class SqliteTestSetup : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly AppDbContext _dbContext;
        protected readonly IUnitOfWork _unitOfWork;
        protected SqliteTestSetup()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlite(_connection)
                    .Options;
            _dbContext = new AppDbContext(options);
            _dbContext.Database.EnsureCreated();
            _unitOfWork = new UnitOfWork(_dbContext);
        }

        public void Dispose() => _connection.Close();

    }
}
