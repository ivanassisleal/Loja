using Loja.Dados;
using Loja.Dados.Repositorios;
using Loja.Dominio.Interfaces;
using Loja.Dominio.Interfaces.Repositorios;
using Loja.Dominio.Interfaces.Servicos;
using Loja.Dominio.Servicos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loja.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Serviços de Domínio
            services.AddScoped<IClienteServico, ClienteServico>();
            services.AddScoped<IProdutoServico, ProdutoServico>();
            services.AddScoped<IPedidoServico, PedidoServico>();

            // Repositórios
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
            services.AddScoped<IPedidoRepositorio, PedidoRepositorio>();
        }
    }
}
