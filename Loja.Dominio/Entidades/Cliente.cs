using Flunt.Validations;
using System;
using System.Collections.Generic;

namespace Loja.Dominio.Entidades
{
    public class Cliente : EntidadeBase
    {
        public Cliente()
        {
            CriadoEm = DateTime.Now;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Aldeia { get; set; }
        public DateTime CriadoEm { get; set; }

        public List<Pedido> Pedidos { get; set; }

        public override void Validar()
        {
            AddNotifications(new Contract<Cliente>()
               .Requires().IsNotNullOrEmpty(Nome, "Nome")
               .Requires().IsNotNullOrEmpty(Email, "Email").IsEmail(Email, "Email")
               .Requires().IsNotNullOrEmpty(Aldeia, "Aldeia")
               );
        }
    }
}
