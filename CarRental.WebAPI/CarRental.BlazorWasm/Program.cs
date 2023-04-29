using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CarRental.BlazorWasm;
using CarRental.BlazorWasm.Services;
using Microsoft.JSInterop;
using Syncfusion.Blazor;
using CarRental.BlazorWasm.Services.ItemService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<HttpClient>();
builder.Services.AddSingleton<ApiService>();
builder.Services.AddSingleton<AuthService>();
builder.Services.AddSingleton<CarService>();
builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();
