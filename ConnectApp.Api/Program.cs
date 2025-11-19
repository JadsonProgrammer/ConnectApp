using ConnectApp.Application.Interfaces.Accounts;
using ConnectApp.Application.Interfaces.Auths;
using ConnectApp.Application.Interfaces.Users;
using ConnectApp.Application.Services.Accounts;
using ConnectApp.Application.Services.Auths;
using ConnectApp.Application.Services.Users;
using ConnectApp.Domain.Interfaces.Accounts;
using ConnectApp.Domain.Interfaces.Auths;
using ConnectApp.Domain.Interfaces.Auths.Tokens;
using ConnectApp.Domain.Interfaces.Users;
using ConnectApp.Infrastructure.Accounts;
using ConnectApp.Infrastructure.Auths;
using ConnectApp.Infrastructure.Auths.Token;
using ConnectApp.Infrastructure.Repositories.Auths;
using ConnectApp.Infrastructure.Repositories.Users;
using ConnectApp.Infrastructure.Sql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Carrega configurações do appsettings.json
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);



        // Lê as configurações JWT e registra diretamente no DI
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()
            ?? throw new InvalidOperationException("Configuração JWT não encontrada");

        builder.Services.AddSingleton(jwtSettings);

        // Gera a chave de segurança para validação do token



        //builder.Services.AddHttpContextAccessor();
        //builder.Services.AddScoped<IGetCredential, GetCredential>();
        //builder.Services.AddScoped<IJwtTokenService, TokenService>();

        var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
        var securityKey = new SymmetricSecurityKey(key);
        builder.Services.AddSingleton(securityKey);
        builder.Services.Configure<JwtSettings>(

        builder.Configuration.GetSection("JwtSettings"));

        //builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<IAuthService, AuthService>();

        // ---------------Account---------------
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IAccountRepository, AccountRepository>();

        // -----------------User-------------------
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection("JwtSettings"));

        // builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<IAuthService, AuthService>();



        // ---------------ConnectPay---------------
        //builder.Services.AddScoped<IReceitaRepository, ReceitaRepository>();
        //builder.Services.AddScoped<IDespesaRepository, DespesaRepository>();
        //builder.Services.AddScoped<ISubCategoriaRepository, SubCategoriaRepository>();
        //builder.Services.AddScoped<ITipoRepository, TipoRepository>();
        ////builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        //IServiceCollection serviceCollection = builder.Services.AddScoped<ICartaoCreditoRepository, CartaoCreditoRepository1>();
        //builder.Services.AddScoped<IContaBancariaRepository, ContaBancariaRepository>();
        ////builder.Services.AddScoped<IDespesaRepository, DespesaRepository>();
        //builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();
        //builder.Services.AddScoped<IFaturaCartaoRepository, FaturaCartaoRepository>();
        //builder.Services.AddScoped<IHistoricoValorInvestimentoRepository, HistoricoValorInvestimentoRepository>();
        //builder.Services.AddScoped<IInvestimentoRepository, InvestimentoRepository>();
        //builder.Services.AddScoped<ILancamentoCartaoRepository, LancamentoCartaoRepository>();
        //builder.Services.AddScoped<IMovimentacaoInvestimentoRepository, MovimentacaoInvestimentoRepository>();
        //builder.Services.AddScoped<IOrcamentoRepository, OrcamentoRepository>();
        //builder.Services.AddScoped<IParcelaEmprestimoRepository, ParcelaEmprestimoRepository>();
        //builder.Services.AddScoped<IReceitaService, ReceitaService>();
        //builder.Services.AddScoped<IDespesaService, DespesaService>();




        //-------------------Auth------------------
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IGetCredential, GetCredential>();


        //-----------------Connection--------------
        builder.Services.AddScoped<IConnectionProvider, SqlConnectionProvider>();
        builder.Services.AddHttpClient();
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IGetCredential, GetCredential>();




        // Configuração dos controladores
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        // Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
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
        });

        builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("MyPolicy", policy =>
                policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAngular",
                policy =>
                {
                    policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
        });




        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = builder.Environment.IsProduction();
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = securityKey,
                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RoleClaimType = ClaimTypes.Role, // garante que o [Authorize(Roles="...")] funcionez    
                NameClaimType = ClaimTypes.NameIdentifier // garante que User.Identity.NameId funcione
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            // Define que a política padrão exige um usuário autenticado.
            // Isso aplica [Authorize] globalmente.
            options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });

        {


            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("Admin", policy => policy.RequireRole("manager"))
                .AddPolicy("Employee", policy => policy.RequireRole("employee"));

            var app = builder.Build();
            app.UseCors("AllowAngular");

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}