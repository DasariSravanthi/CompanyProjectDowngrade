using Microsoft.EntityFrameworkCore;
using Mapster;

using CompanyApp.Data;
using CompanyApp.Converter;
using CompanyApp.Mapper.MapperService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
