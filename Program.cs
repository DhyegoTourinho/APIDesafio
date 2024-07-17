using APIDesafio;
using APIDesafio.Dados;
using APIDesafio.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<TokenService>();
builder.Services.AddControllers();

//ESSE CARA PODE DAR ERRADO, ADICIONEI PRA TESTE
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddAuthentication(opcs =>
{
    opcs.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opcs.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opcs =>
{
    opcs.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
        ValidateIssuer = false,
        ValidateAudience = true,
        ValidAudience = "clienteapi"
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "DesafioAPI", Description = "Uma api para testar e melhorar meus conhecimento em C# e ASP.NET", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Insira o token de acesso!",
        Name = "Autorização",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        new string[]{}
    }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
