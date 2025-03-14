using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AppGestaoDeResiduos.Data;
using AppGestaoDeResiduos.Models;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using MongoDB.Driver;
using AppGestaoDeResiduos.Data.Repositories; // Adicione este namespace para os reposit�rios

var builder = WebApplication.CreateBuilder(args);

// ##################################################
// #               Configura��o do MongoDB          #
// ##################################################

// Obt�m a string de conex�o do MongoDB do arquivo appsettings.json
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoConnection");
// Define o nome do banco de dados do MongoDB
var mongoDatabaseName = "GestaoResiduos";

// Registra o MongoDBContext como um servi�o singleton
builder.Services.AddSingleton<MongoDBContext>(sp =>
    new MongoDBContext(mongoConnectionString, mongoDatabaseName));

// ##################################################
// #         Fim da Configura��o do MongoDB         #
// ##################################################

// ##################################################
// #           Registro dos Reposit�rios            #
// ##################################################

// Registra os reposit�rios como servi�os com escopo (scoped)
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<ColetaRepository>();
builder.Services.AddScoped<CaminhaoRepository>();
builder.Services.AddScoped<NotificacaoRepository>();

// ##################################################
// #         Fim do Registro dos Reposit�rios       #
// ##################################################

// Configura��o do MVC (Model-View-Controller) para a API
builder.Services.AddControllersWithViews();

// ##################################################
// #   Configura��o do Entity Framework Core (Oracle)#
// ##################################################

// Conecta o aplicativo ao banco de dados Oracle usando a string de conex�o do appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// ##################################################
// # Fim da Configura��o do Entity Framework Core   #
// ##################################################


// ##################################################
// #           Configura��o da Autentica��o JWT     #
// ##################################################

// Define a chave secreta, emissor e audi�ncia para o token JWT
var secretKey = "your_very_long_secret_key_32_chars_minimum"; // Deve ter pelo menos 32 caracteres
var issuer = "useradmin";
var audience = "adminaudience";

// Configura o esquema de autentica��o JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// ##################################################
// #      Fim da Configura��o da Autentica��o JWT   #
// ##################################################

var app = builder.Build();

// Configura��o do pipeline de requisi��es HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Adiciona middleware de autentica��o e autoriza��o
app.UseAuthentication();
app.UseAuthorization();

// Configura a rota padr�o para os controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ##################################################
// #   Adiciona Dados de Teste e Exibe Token JWT    #
// ##################################################

using (var scope = app.Services.CreateScope())
{
    // Gera e exibe um token JWT de teste no console
    var tokenGenerator = new TokenGenerator(secretKey, issuer, audience);
    var testToken = tokenGenerator.GenerateTestToken();
    Console.WriteLine("Test JWT Token: " + testToken);
}

// ##################################################
// # Fim da Adi��o de Dados de Teste e Exibi��o JWT #
// ##################################################

app.Run();

// ##################################################
// #   Classe para Gerar Tokens JWT de Teste        #
// ##################################################

public class TokenGenerator
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;

    public TokenGenerator(string secretKey, string issuer, string audience)
    {
        _secretKey = secretKey;
        _issuer = issuer;
        _audience = audience;
    }

    // M�todo para gerar um token JWT de teste
    public string GenerateTestToken()
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "Danilo"),
                new Claim(ClaimTypes.Role, "Admin")
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _issuer,
            Audience = _audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

// ##################################################
// # Fim da Classe para Gerar Tokens JWT de Teste   #
// ##################################################