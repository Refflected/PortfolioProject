using API.Bootstrap;
using DataAccessLayer.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.ConfigurePortfolioContext(builder.Configuration);

// Repositories
builder.Services.ConfigureRepositories();

// Authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:5001";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
        };
    });

// Add IHttpClientFactory for token request endpoint
builder.Services.AddHttpClient();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Portfolio API",
        Version = "v1"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Please insert JWT with Bearer into field",
        In = ParameterLocation.Header,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = JwtConstants.HeaderType,
        Type = SecuritySchemeType.Http,
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            BearerFormat = JwtConstants.HeaderType,
            Name = "JWT Bearer Authentication",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = JwtBearerDefaults.AuthenticationScheme,
            
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = JwtBearerDefaults.AuthenticationScheme,
            },
            
        },
        new string[] { }
    }});
    options.OperationFilter<SwaggerOperationFilter>();
});

var app = builder.Build();

if (args.Contains("/seed"))
{
    PortfolioTestSeeding.SeedDatabase(app.Services); 
    return;
}

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
