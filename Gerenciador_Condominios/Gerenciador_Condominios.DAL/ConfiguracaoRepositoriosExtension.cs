using Gerenciador_Condominios.DAL.Interfaces;
using Gerenciador_Condominios.DAL.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Condominios.DAL
{
    public static class ConfiguracaoRepositoriosExtension
    {
        public static void ConfigurarRepositorios(this IServiceCollection services)
        {
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddTransient<IFuncaoRepositorio, FuncaoRepositorio>();
            services.AddTransient<IVeiculoRepositorio, VeiculoRepositorio>();
            services.AddTransient<IEventoRepositorio, EventoRepositorio>();
            services.AddTransient<IServicoRepositorio, ServicoRepositorio>();
            services.AddTransient<IServicoPredioRepositorio, ServicoPredioRepositorio>();
            services.AddTransient<IHistoricoRecursosRepositorio, HistoricoRecursosRepositorio>();
            services.AddTransient<IApartamentoRepositorio, ApartamentoRepositorio>();
            //services.AddTransient<IMesRepositorio, MesRepositorio>();
            //services.AddTransient<IAluguelRepositorio, AluguelRepositorio>();
            //services.AddTransient<IPagamentoRepositorio, PagamentoRepositorio>();

        }
    }
}
