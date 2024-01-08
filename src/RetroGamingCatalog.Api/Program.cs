using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
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
    opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<SampleDataInitialization>();
builder.Services.AddQueries();
builder.Services.AddCors(cors => cors.AddDefaultPolicy(pb => pb.AllowAnyOrigin()));
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
/*app.BuildGamesApi();
app.BuildConsolesApi();
app.BuildManufacturers();

app.UseCors(cpb => cpb.AllowAnyOrigin());

/*await using (var scope = app.Services.CreateAsyncScope())
{
    var user = new User
    {
        Id = Guid.NewGuid(),
        Email = "nicolas.castellano@test.com",
        RegistrationDate = DateTime.UtcNow,
        Username = "potocastel",
        UserType = User.UserTypeEnum.Admin,
        PasswordHash = string.Empty,
    };


    var passwordHasher = new PasswordHasher<User>();
    user.PasswordHash = passwordHasher.HashPassword(user, "Test123!");

    var db = scope.ServiceProvider.GetRequiredService<CatalogDb>();
    await db.AddAsync(user);
    await db.SaveChangesAsync();
}*/

app.Run();

//public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

[JsonSerializable(typeof(Game[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
