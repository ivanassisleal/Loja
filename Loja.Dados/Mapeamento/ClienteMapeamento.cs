using Loja.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Loja.Dados.Mapeamento
{
    public class ClienteMapeamento : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");

            builder.HasKey(m => m.Id);

            builder.HasIndex(m => m.Email)
                .IsUnique();

            builder.Property(m => m.Nome)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(m => m.Aldeia)
                .IsRequired()
                .HasMaxLength(80);

            builder
                .HasMany(m => m.Pedidos)
                .WithOne(m => m.Cliente)
                .HasForeignKey(m=>m.ClienteId);


        }
    }
}
