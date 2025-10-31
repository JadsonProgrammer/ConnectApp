using ConnectApp.Application.Interfaces.Auths;
using ConnectApp.Application.Interfaces.Users;
using ConnectApp.Application.Services.Auths;
using ConnectApp.Application.Services.Users;
using ConnectApp.Domain.Interfaces.Auths.Tokens;
using ConnectApp.Domain.Interfaces.Users;
using ConnectApp.Infrastructure.Auth;
using ConnectApp.Infrastructure.Auth.Token;
using ConnectApp.Infrastructure.Repositories.Users;
using ConnectApp.Infrastructure.Sql;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Carrega configura��es do appsettings.json
        builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        // L� as configura��es JWT e registra diretamente no DI
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>()
            ?? throw new InvalidOperationException("Configura��o JWT n�o encontrada");

        builder.Services.AddSingleton(jwtSettings);

        // Gera a chave de seguran�a para valida��o do token
        var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
        var securityKey = new SymmetricSecurityKey(key);
        builder.Services.AddSingleton(securityKey);
        builder.Services.Configure<JwtSettings>(
        builder.Configuration.GetSection("JwtSettings"));

        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        // Registro de servi�os da aplica��o


        // -----------------User-------------------
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.Configure<JwtSettings>(
            builder.Configuration.GetSection("JwtSettings"));

        builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
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




        // Configura��o dos controladores
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
        // CORS
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




        // Autentica��o JWT
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
                RoleClaimType = ClaimTypes.Role, // garante que o [Authorize(Roles="...")] funcione
                NameClaimType = ClaimTypes.NameIdentifier // garante que User.Identity.NameId funcione
            };
        });



        {

            // Autoriza��o por roles
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


















/*
 * var builder = WebApplication.CreateBuilder(args);









// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
/*/