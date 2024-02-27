global using Microsoft.EntityFrameworkCore;
using DEWAN.Models;
using Microsoft.AspNetCore.Http.Features;
using System.Collections.Generic;
namespace DEWAN.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public DbSet<receipt> receipts { get; set; }
        public DbSet<item> items { get; set; }

        
    }

}
