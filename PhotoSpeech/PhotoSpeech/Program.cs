using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using PhotoSpeech.DataAccess.Handlers;
using PhotoSpeech.DataAccess.Handlers.Interfaces;
using PhotoSpeech.DataAccess.Models;
using PhotoSpeech.Options;
using PhotoSpeech.Providers;
using PhotoSpeech.Services;
using PhotoSpeech.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMudServices();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserHandler, UserHandler>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IWordHandler, WordHandler>();
builder.Services.AddScoped<IPhotosService, PhotosService>();
builder.Services.AddScoped<IBingPhotoService, BingPhotoService>();
builder.Services.AddScoped<ITranslatorService, TranslatorService>();
builder.Services.AddSingleton<LoggedUserProvider>();
builder.Services.AddScoped<IBlobStorageService, BlobStorageService>();

builder.Services.Configure<AzureCognitiveOptions>(
    builder.Configuration.GetSection(AzureCognitiveOptions.Section));

builder.Services.Configure<AzureBingSearchOptions>(
    builder.Configuration.GetSection(AzureBingSearchOptions.Section));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
