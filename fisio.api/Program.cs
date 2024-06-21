using System.Text;
using fisio.api;
using fisio.domain.Mappers;
using fisio.domain.Mappers.Interfaces;
using fisio.domain.Repositories;
using fisio.infra.Contexts;
using fisio.infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Autenticacao JWT
var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(x =>
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
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var serverVersion = new MySqlServerVersion(new Version(5, 7, 32));
var connectiontring = builder.Configuration.GetConnectionString("mysqlConnection");
builder.Services.AddDbContext<FisioMySQLContext>(options => options.UseMySql(connectiontring, serverVersion));
builder.Services.AddDbContext<FisioInMemoryContext>();

//dependencias dos repositorios
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

//dependencias do mapper
builder.Services.AddTransient<IMapperConfig, MapperConfig>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

//configuração apenas pra funcionar em localhost
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
