using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SportClubWMS;
using SportClubWMS.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

if (builder.HostEnvironment.IsDevelopment())
{
    builder.Services.AddOidcAuthentication(options =>
    {
        // Configure your authentication provider options here.
        // For more information, see https://aka.ms/blazor-standalone-auth
        builder.Configuration.Bind("Local", options.ProviderOptions);
    });

    builder.Services.AddHttpClient<ICustomerDataService, CustomerDataService>
        (client => client.BaseAddress = new Uri("https://localhost:7117"));
    builder.Services.AddHttpClient<ISportGoodDataService, SportGoodDataService>
        (client => client.BaseAddress = new Uri("https://localhost:7117"));
}

await builder.Build().RunAsync();
