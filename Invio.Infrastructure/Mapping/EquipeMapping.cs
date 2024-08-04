using Invio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invio.Infrastructure.Mapping
{
    public class EquipeMapping : IEntityTypeConfiguration<Equipe>
    {
        public void Configure(EntityTypeBuilder<Equipe> builder)
        {
            builder.ToTable("Equipe");

            builder.HasKey(s => s.Id)
                .HasName("PK_Equipe");

            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("EquipeId")
                .HasColumnOrder(1)
                .HasComment("Chave Primária da Equipe");

            builder.Property(s => s.Nome)
                .HasColumnName("Nome")
                .HasColumnOrder(2)
                .HasComment("Nome da Equipe");

            builder.Property(s => s.Categoria)
                .HasColumnName("Categoria")
                .HasColumnOrder(3)
                .HasComment("Categoria da Equipe");
        }
    }
}
