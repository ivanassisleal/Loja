using Loja.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Loja.Dados.Mapeamento
{
    public class ProdutoMapeamento : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.Descricao)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(m => m.Valor)
                .IsRequired();

            builder.Property(m => m.Foto)
                .HasMaxLength(80);

        }
    }
}
