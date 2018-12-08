using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewYearLanding.DAL.Mongo;
using NewYearLanding.DAL.Mongo.Abstractions;
using NewYearLanding.DAL.Mongo.Implementation;
using NewYearLanding.DAL.RadarioSQL;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;


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
            services.AddMvc(config => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            services.Configure<MongoDbConnectionSettings>(
                options => {
                    options.ConnectionString = Config["MongoDb:ConnectionString"];
                    options.Database = Config["MongoDb:Database"];
                });
            services.AddLogging(f => f.AddConsole());
            services.AddSingleton(Config);
            services.AddTransient<ICompaniesRepository, CompaniesRepository>();
            services.AddTransient<IModelFacade, ModelFacade>();

            services
                .AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasicAuthentication(ConfigureBasicAuthOptions);
        }

        private void ConfigureBasicAuthOptions(BasicAuthenticationOptions options) {
            options.Realm = "Radario";
            options.Events = new BasicAuthenticationEvents {
                OnValidatePrincipal = context => {
                    if ((context.UserName == "radario") && (context.Password == "qwertyp0p")) {
                        var claims = new List<Claim> {new Claim(ClaimTypes.Name, context.UserName, context.Options.ClaimsIssuer)};

                        var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, BasicAuthenticationDefaults.AuthenticationScheme));
                        context.Principal = principal;
                    }
                    else {
                        context.AuthenticationFailMessage = "Authentication failed.";
                    }

                    return Task.CompletedTask;
                }
            };
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
