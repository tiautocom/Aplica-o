using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APP.DOMAIN.Identity;
using APP.REPOSITORY;
using APP.REPOSITORY.Interface;
using APP.REPOSITORY.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace APP.WEBAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(
                x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            IdentityBuilder builder = services.AddIdentityCore<User>(options =>
             {
                 options.Password.RequireDigit = false;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireUppercase = false;
                 options.Password.RequiredLength = 4;
             });

            //Passando a  configuração para o objeto BuilderUserType
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);

            //Informando o  meu context (DataContext)
            builder.AddEntityFrameworkStores<DataContext>();

            //Responsavel pelas Validaçoes
            builder.AddRoleValidator<RoleValidator<Role>>();

            //Gerenciador dos papeis(perfis)
            builder.AddRoleManager<RoleManager<Role>>();

            //Sign
            builder.AddSignInManager<SignInManager<User>>();

            //Configurar o JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>{
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    //alem de validar 
                    //Configurar Token
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.ASCII
                    .GetBytes(Configuration.GetSection("AppSetting:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });


            //Configurar para a chamada do Controller(politica)
            //Rota exige autenticação do usuario
             services.AddControllers(options =>
             {
                 var policy = new AuthorizationPolicyBuilder()
                     .RequireAuthenticatedUser()
                     .Build();
                 options.Filters.Add(new AuthorizeFilter(policy));
             })
             .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);



            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddAutoMapper(typeof(Startup));

            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
