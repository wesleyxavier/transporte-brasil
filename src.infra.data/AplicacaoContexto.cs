using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using src.core.entities;

namespace src.infra.data {
    public class AplicacaoContexto : DbContext {
        public AplicacaoContexto (DbContextOptions<AplicacaoContexto> options):
            base (options) {
                Database.EnsureCreated ();
            }

        protected override void OnModelCreating (ModelBuilder builder) {
            builder.Entity<Veiculo> ().ToTable ("Veiculo").HasKey (c => c.Id);

            base.OnModelCreating (builder);
        }

        public DbSet<Veiculo> Veiculo { get; set; }
    }
}