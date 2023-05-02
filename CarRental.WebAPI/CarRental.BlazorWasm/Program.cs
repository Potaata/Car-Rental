using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CarRental.BlazorWasm;
using CarRental.BlazorWasm.Services;
using Microsoft.JSInterop;
using Syncfusion.Blazor;
using Microsoft.AspNetCore.Components;
using CarRental.BlazorWasm.Services.ItemService;
using CarRental.BlazorWasm.Pages;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSingleton<ApiService>();
builder.Services.AddSingleton<SessionService>();
builder.Services.AddSingleton<LoginService>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<RentService>();
builder.Services.AddSingleton<PendingHistoryService>();
builder.Services.AddSingleton<CarService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<DiscountService>();
builder.Services.AddSingleton<DamageService>();


builder.Services.AddSyncfusionBlazor();


await builder.Build().RunAsync();
