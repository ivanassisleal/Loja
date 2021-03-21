using Flunt.Validations;

namespace Loja.Dominio.Entidades
{
    public class Produto : EntidadeBase
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string Foto { get; set; }

        public override void Validar()
        {
            AddNotifications(new Contract<Produto>()
                .Requires().IsNotNullOrEmpty(Descricao, "Descricao")
                .Requires().IsGreaterThan(Valor, 0, "Valor")
                );
        }
    }
}
