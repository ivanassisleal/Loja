using Loja.Dados.Contexto;
using Loja.Dominio.Entidades;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loja.Dados.Testes
{
    public class BdMemoria
    {
        private LojaContexto _contexto;

        public BdMemoria()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var options = new DbContextOptionsBuilder<LojaContexto>()
                .UseSqlite(connection)
                .EnableSensitiveDataLogging()

                .Options;

            _contexto = new LojaContexto(options);

            CreateDadosFalsos();
        }

        public LojaContexto ObterContexto() => _contexto;

        private void CreateDadosFalsos()
        {
            if (_contexto.Database.EnsureCreated())
            {
                _contexto.Clientes.Add(new Cliente()
                {
                    Nome = "Fulano de Tal",
                    Email = "fulano@gmail.com",
                    Aldeia = "A1"
                });

                _contexto.Clientes.Add(new Cliente()
                {
                    Nome = "Sicrano de Tal",
                    Email = "sicrano@gmail.com",
                    Aldeia = "A2"
                });

                _contexto.Clientes.Add(new Cliente()
                {
                    Nome = "Beltrano de Tal",
                    Email = "beltrano@gmail.com",
                    Aldeia = "A2"
                });

                _contexto.Produtos.Add(new Produto()
                {
                    Descricao = "Produto 1",
                    Valor = 21
                });

                _contexto.Produtos.Add(new Produto()
                {
                    Descricao = "Produto 2",
                    Valor = 223
                });

                _contexto.SaveChanges();
            }
        }
    }
}
