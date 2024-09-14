using WebApplication2.Services.AnimeService;
using WebApplication2.Services.ApiService;
using WebApplication2.Services.ApiService.ApiService;
using WebApplication2.Services.MangakaService;
using WebApplication2.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IApiService, ApiService>(); 
builder.Services.AddSingleton<IAnimeService, AnimeService>(); // It is used both for initializing objects, but also in UserModel.
builder.Services.AddTransient<IUserService, UserService>(); // Means that every client request will create a new instance and isolate state between different clients
builder.Services.AddTransient<IMangakaService, MangakaService>(); // Same like UserService

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
