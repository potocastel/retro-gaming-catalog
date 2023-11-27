using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using RetroGamingCatalog.Api;
using RetroGamingCatalog.Dao;
using RetroGamingCatalog.Api.DTOs;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddDbContext<CatalogDb>();//opt => opt.UseInMemoryDatabase("catalog", x => { }));
builder.Services.AddTransient<SampleDataInitialization>();
builder.Services.AddQueries();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
    await scope.ServiceProvider.GetRequiredService<SampleDataInitialization>().Initialize();

app.BuildGamesApi();
app.BuildConsolesApi();
app.BuildManufacturers();

app.Run();

//public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Game[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}