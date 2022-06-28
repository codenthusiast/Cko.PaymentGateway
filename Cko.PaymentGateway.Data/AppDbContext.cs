using Cko.PaymentGateway.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cko.PaymentGateway.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<Merchant> Merchants { get; set; }
    }

}
