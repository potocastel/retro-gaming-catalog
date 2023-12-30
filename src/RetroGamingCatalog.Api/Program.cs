using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RetroGamingCatalog.Api;
using RetroGamingCatalog.Api.Endpoints;
using RetroGamingCatalog.Api.Queries;
using RetroGamingCatalog.Dao;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.AddJwtTokenAuthentication();

builder.Services.AddDbContext<CatalogDb>(
opt => 
opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
//opt.UseInMemoryDatabase("catalog", x => { })
);

builder.Services.AddTransient<SampleDataInitialization>();
builder.Services.AddQueries();
builder.Services.AddCors(cors =>
    cors.AddDefaultPolicy(pb => pb.AllowAnyOrigin()));
builder.Services.AddControllers();

var app = builder.Build();

    app.UseAuthentication();
    app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.BuildGamesApi();
app.BuildConsolesApi();
app.BuildManufacturers();

app.UseCors(cpb => cpb.AllowAnyOrigin());

app.Run();

//public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Game[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
