using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewYearLanding.DAL.Mongo;
using NewYearLanding.DAL.Mongo.Abstractions;
using NewYearLanding.DAL.Mongo.Implementation;
using NewYearLanding.DAL.RadarioSQL;

namespace NewYearLanding {
    public class Startup {
        private IConfiguration Config { get; }

        public Startup(IConfiguration configuration) {
            Config = configuration;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddCors(options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                );
            });
            services.AddMvc();
            services.Configure<MongoDbConnectionSettings>(
                options => {
                    options.ConnectionString = Config["MongoDb:ConnectionString"];
                    options.Database = Config["MongoDb:Database"];
                });
            services.AddLogging(f => f.AddConsole());
            services.AddSingleton(Config);
            services.AddTransient<ICompaniesRepository, CompaniesRepository>();
            services.AddTransient<IModelFacade, ModelFacade>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
