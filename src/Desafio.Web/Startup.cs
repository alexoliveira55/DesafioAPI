using Desafio.Dados.Contexto;
using Desafio.Dados.Repositorio;
using Desafio.Negocio.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Text;

namespace Desafio.Web
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllersWithViews();

            string NomeArquivoXmlDocumentacao = "Desafio.Web.xml";
            services.AddSwaggerGen(c =>
            {

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "Authorization", 
                    In = ParameterLocation.Header,
                    Description = "Identificador de sessão",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",

                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                         new OpenApiSecurityScheme
                         {
                             Reference = new OpenApiReference
                             {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                             }
                         },
                         Array.Empty<string>()
                     }
                 });
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "DesafioApi",
                        Version = "v1",
                        Description = "API REST criada com o ASP.NET Core 3.1 para teste de conhecimentos.<br /><br />"
                                      + "<b>ACESSO E UTILIZAÇÃO</b><br />"
                                      + "Antes de utilizar qualquer uma das operações dessa API, o usuário deve obter o Token de autorização através da operação /Home/autenticar.<br />"
                                      + "Após o Token gerado clique em  Authorize e Informe o Token. Ex.: Bearer [Seu Token].<br />"
                                      + "<b>O Token Expira após 2 horas.</b><br /><br />"
                                      + "<b>Utilize os dados abaixo para autenticar e obter o token:</b><br />"
                                      + "<b>nome:</b> globaltec<br />"
                                      + "<b>senha:</b> globaltec",


                        Contact = new OpenApiContact
                        {
                            Name = "Alex de Oliveira dos Santos (Clique para acessar o código fonte no GitHub)",
                            Url = new Uri("https://github.com/alexoliveira55/DesafioAPI")
                        }
                         
                    });
                string caminhoArquivo = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, NomeArquivoXmlDocumentacao);
                c.IncludeXmlComments(caminhoArquivo);
            });


            var chave = Encoding.ASCII.GetBytes(Util.UtilitarioToken.ChavePrivada);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(chave),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<ContextoDesafio>();
            services.AddScoped<IUsuarioAutenticacaoRepositorio, UsuarioAutenticacaoRepositorio>();
            services.AddScoped<IPessoaRepositorio, PessoaRepositorio>();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Ativando middlewares para uso do Swagger
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                //c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "DesafioApi");
            });


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
