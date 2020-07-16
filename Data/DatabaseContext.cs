using Microsoft.EntityFrameworkCore;
using SalesMotorcycle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesMotorcycle.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Data_Motor> Data_Motors { get; set; }

        public DbSet<UseRegister> User_Register { get; set; }

        public DbSet<Order> Order { get; set; }
    }
}
