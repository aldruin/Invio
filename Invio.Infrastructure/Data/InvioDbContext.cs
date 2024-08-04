using Invio.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Infrastructure.Data
{
    public class InvioDbContext : IdentityDbContext <Usuario, IdentityRole<Guid>,Guid>
    {
        public InvioDbContext( DbContextOptions <InvioDbContext> options) : base(options) { }

        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InvioDbContext).Assembly);
        }
    }
}
