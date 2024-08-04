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
    public class ItemMapping : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Item");

            builder.HasKey(i => i.Id)
                .HasName("PK_Item");

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ItemId")
                .HasColumnOrder(1)
                .HasComment("Chave Primária do Item");

            builder.Property(i => i.Nome)
                .HasColumnName("Nome")
                .HasColumnOrder(2)
                .HasComment("Nome do Item");

            builder.Property(i => i.Quantidade)
                .HasColumnName("Quantidade")
                .HasColumnOrder(3)
                .HasComment("Quantidade do Item");

            builder.Property(i => i.Categoria)
                .HasColumnName("Categoria")
                .HasColumnOrder(4)
                .HasComment("Categoria do Item");

            builder.Property(i => i.Descricao)
                .HasColumnName("Descrição")
                .HasColumnOrder(5)
                .HasComment("Descrição do Item");

            builder.Property(i => i.EquipeId)
                .HasColumnName("EquipeId")
                .HasColumnOrder(6)
                .HasComment("Chave Estrangeira para Equipe");

            builder.Property(i => i.DataFornecimento)
                .HasColumnName("Fornecimento")
                .HasColumnOrder(7)
                .HasComment("Data de fornecimento do item");

            builder.Property(i => i.DataTermino)
                .HasColumnName("Termino")
                .HasColumnOrder(8)
                .HasComment("Data de termino do item");

            builder.HasOne(i => i.Equipe)
                .WithMany(s => s.Items)
                .HasForeignKey(i => i.EquipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
