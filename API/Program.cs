using DofusRetroAPI.Database;
using DofusRetroAPI.Services.ItemService;
using DofusRetroAPI.Services.MonsterService;
using DofusRetroAPI.Services.RecipeService;
using DofusRetroAPI.Services.SetService;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Db Context SqlServer (Windows)
builder.Services.AddDbContext<DofusRetroDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(
        "DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add logging middleware
builder.Logging.AddConsole();

// DI Services
builder.Services.AddScoped<IMonsterService, MonsterService>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<ISetService, SetService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

WebApplication app = builder.Build();

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