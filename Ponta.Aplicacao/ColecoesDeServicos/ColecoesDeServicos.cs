using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Ponta.Aplicacao.Modelos;
using Ponta.Dominio.Entidades;
using Ponta.Dominio.Enumeradores;
using Ponta.Dominio.IRepositorios;
using Ponta.Dominio.IServicos;
using Ponta.Infra.RepositórioBase;
using Ponta.Infra.Repositorios;
using Ponta.Servico.Servicos;

namespace Ponta.Aplicacao.ServicesCollections
{
    public static class ColecoesDeServicos
    {
        public static IServiceCollection AdicionarServicos(this IServiceCollection services)
        {
            services.AddScoped<IRepositorioBase<Tarefas>, RepositorioTarefa>();
            services.AddScoped<IServicoTarefa, ServicoTarefa>();

            services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            services.AddScoped<IServicoLogin, ServicoLogin>();


            return services;
        }

        public static IServiceCollection AdicionarModelos(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(config =>
            {
                config.CreateMap<ModeloEntradaUsuario, Usuario>();

                config.CreateMap<ModeloEntradaTarefa, Tarefas>();
                config.CreateMap<ModeloAtualizacaoTarefa, Tarefas>();
                config.CreateMap<Tarefas,ModeloSaidaTarefa>();
                


            }).CreateMapper());
            
            return services;

		}

    }
}
