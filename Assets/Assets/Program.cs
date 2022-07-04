using Assets.Models;
using Assets.Repositories;
using Assets.Contracts;
using AutoMapper;
using Assets.Mapping_profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = Environment.GetEnvironmentVariable("AzureTableStorage");
string AssetsTableName = Environment.GetEnvironmentVariable("AssetsTableName");
string AssetsCategoryTableName = Environment.GetEnvironmentVariable("AssetsCategoryTableName");
string AssetsTypeTableName = Environment.GetEnvironmentVariable("AssetsTypeTableName");

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AssetProfile ());
    mc.AddProfile(new AssetCategoryProfile ());
    mc.AddProfile(new AssetTypeProfile ());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IRepository<Asset>>(x => new AssetRepository(connectionString, AssetsTableName, mapper));
builder.Services.AddScoped<IRepository<AssetCategory>>(x => new AssetCategoryRepository(connectionString, AssetsCategoryTableName, mapper));
builder.Services.AddScoped<IRepository<AssetType>>(x => new AssetTypeRepository(connectionString, AssetsTypeTableName, mapper));
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("corsapp");



app.MapControllers();

app.Run();
