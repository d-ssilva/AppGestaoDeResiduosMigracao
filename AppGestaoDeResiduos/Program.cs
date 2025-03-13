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

var builder = WebApplication.CreateBuilder(args);

//  Configura��o do MongoDB
// Obt�m a string de conex�o do MongoDB do arquivo appsettings.json
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoConnection");
// Define o nome do banco de dados do MongoDB
var mongoDatabaseName = "GestaoResiduos";

// Registra o MongoDBContext como um servi�o singleton
builder.Services.AddSingleton<MongoDBContext>(sp =>
    new MongoDBContext(mongoConnectionString, mongoDatabaseName));
//  Configura��o do MongoDB

// Configura��o do MVC (Model-View-Controller) para a API
builder.Services.AddControllersWithViews();

//  Configura��o do Entity Framework Core para o Oracle
// Conecta o aplicativo ao banco de dados Oracle usando a string de conex�o do appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));
//  Configura��o do Entity Framework Core para o Oracle

// Registra o servi�o de teste como um servi�o com escopo (scoped)
builder.Services.AddScoped<TestService>();

// Configura��o do serializador JSON para evitar loops de refer�ncia
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64; // Ajuste conforme necess�rio
    });

//  Configura��o da autentica��o JWT
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
// Configura��o da autentica��o JWT

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

//  Adiciona dados de teste e exibe um token JWT de teste antes de iniciar a aplica��o
using (var scope = app.Services.CreateScope())
{
    // Obt�m o servi�o de teste e adiciona dados de teste ao banco de dados Oracle
    var testService = scope.ServiceProvider.GetRequiredService<TestService>();
    await testService.AddTestDataAsync();

    // Gera e exibe um token JWT de teste no console
    var tokenGenerator = new TokenGenerator(secretKey, issuer, audience);
    var testToken = tokenGenerator.GenerateTestToken();
    Console.WriteLine("Test JWT Token: " + testToken);
}
//  Adiciona dados de teste e exibe token JWT

app.Run();

//  Classe de servi�o para adicionar dados de teste ao banco de dados Oracle
public class TestService
{
    private readonly ApplicationDbContext _context;

    public TestService(ApplicationDbContext context)
    {
        _context = context;
    }

    // M�todo para adicionar um usu�rio de teste ao banco de dados

    public async Task AddTestDataAsync()
    {
        var testUser = new Usuario
        {
            Nome = "Danilo",
            Email = "danilo@exemplo.com",
            AgendouColeta = false,
            FoiNotificado = false,
            EnderecoId = 1
        };

        _context.Usuarios.Add(testUser);
        await _context.SaveChangesAsync();
    }
}

//  Classe para gerar tokens JWT de teste
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