using Microsoft.EntityFrameworkCore;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.OpenApi.Models;

using CompanyApp.Data;
using CompanyApp.Converter;
using CompanyApp.Mapper.Configuration;
using CompanyApp.Mapper.MapperService;
using CompanyApp.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecretKey"] ?? String.Empty))
            };
        });

builder.Services.AddAuthorization(options => 
{
    options.AddPolicy(IdentityConstants.PolicyName1, policy => {
        policy.RequireClaim(ClaimTypes.Role, IdentityConstants.ClaimNames1); 
    });

});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    options.SerializerSettings.Converters.Add(new NullableDateOnlyJsonConverter());
    options.SerializerSettings.Converters.Add(new NullableTimeOnlyJsonConverter());

});

builder.Services.AddDbContext<CompanyDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

// Create the Mapster configuration
var config = new TypeAdapterConfig();
MapsterConfiguration.ConfigureMappings(config);

// Register the TypeAdapterConfig as a singleton service
builder.Services.AddSingleton(config);

// Register the MapperService as a scoped service
builder.Services.AddScoped<AppMapper>();

//builder.Services.AddAutoMapper(typeof(AppMappingProfile));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add JWT token support
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();    // Add this line to enable authentication
app.UseAuthorization();

app.MapControllers();

app.Run();
