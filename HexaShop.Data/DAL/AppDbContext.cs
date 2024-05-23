using HexaShop.Core.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexaShop.Data.DAL
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions option) : base(option)
        {
            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> Users { get; set; }

    }
}
