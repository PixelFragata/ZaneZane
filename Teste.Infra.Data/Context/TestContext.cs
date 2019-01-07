using System;
using Microsoft.EntityFrameworkCore;
using Teste.Domain.Entities;

namespace Teste.Infra.Data.Context
{
    public class TestContext : DbContext
    {
        
        public DbSet<User> Users { get; set; }

        public TestContext(DbContextOptions options) : base(options)
        {

        }
    }
}
