using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Loja.Dominio.Entidades
{
    public class Pedido : EntidadeBase
    {
        public Pedido()
        {
            Data = DateTime.Now;
            Produtos = new List<PedidoItem>();
        }

        public int Id { get; set;  }
        public int Numero { get; set; }
        public int ClienteId { get; set; }
        public DateTime Data { get; set; }
        public decimal Desconto { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorTotal { get; set; }
        public Cliente Cliente { get; set; }
        public List<PedidoItem> Produtos { get; set; }

        public override void Validar()
        {
            AddNotifications(new Contract<Pedido>()
                .Requires().IsGreaterThan(ValorTotal,0, "ValorTotal")
                .Requires().IsGreaterThan(Valor, 0, "Valor")
               );
        }
    }
}
