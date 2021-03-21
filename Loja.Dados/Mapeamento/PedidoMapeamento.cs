using Loja.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Loja.Dados.Mapeamento
{
    public class PedidoMapeamento : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido");

            builder.HasKey(m => m.Id);

            builder.HasIndex(m => m.Numero)
                .IsUnique();

            builder.Property(m => m.Valor)
                .IsRequired();

            builder.Property(m => m.ValorTotal)
               .IsRequired();

            builder.Property(m => m.Data)
              .IsRequired();

            builder
                .HasMany(m => m.Produtos)
                .WithOne(m => m.Pedido)
                .HasForeignKey(m => m.PedidoId);
        }
    }
}
