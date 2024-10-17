using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorDemo;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });



var host = builder.Build();
Aardworx.WebAssembly.JSRuntime.Instance = host.Services.GetService(typeof(IJSRuntime)) as WebAssemblyJSRuntime;
await host.RunAsync();