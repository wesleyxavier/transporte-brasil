using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using src.core.services.veiculo;
using src.infra.data;
using src.infra.repositorys;
using src.infra.repositorys.veiculo;

namespace src.app.utils.Ioc {
    public static class IocService {
        public static IServiceCollection IocConfig (this IServiceCollection services) {
            services.DataBase ();
            services.Repository ();
            services.Service ();

            return services;
        }

        private static void Repository (this IServiceCollection services) {
            services.AddTransient<IVeiculoRepository, VeiculoRepository> ();
        }

        private static void Service (this IServiceCollection services) {
            services.AddTransient<IVeiculoService, VeiculoService> ();
        }

        private static void DataBase (this IServiceCollection services) {
            services.AddDbContext<AplicacaoContexto> (options => {
                var conexao = Environment.GetEnvironmentVariable ("DATABASE_TRANSPORTE_V1");
                if (!string.IsNullOrEmpty (conexao)) {
                    options.UseMySql (conexao);
                } else {
                    options.UseInMemoryDatabase("testedb");
                }
            });
        }
    }
}