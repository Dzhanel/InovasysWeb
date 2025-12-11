using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InovaSys.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace InovaSys.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {

        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
