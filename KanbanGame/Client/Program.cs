using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KanbanGame.Client;
using KanbanGame.Client.Services;
using Syncfusion.Blazor;

using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
Console.WriteLine(config["sflicense"]);
Console.WriteLine("test");

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(config["sflicense"]);

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(
    sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
);
builder.Services.AddScoped<IKanbanTaskService, KanbanTaskService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IFeatureService, FeatureService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddSyncfusionBlazor();

await builder.Build().RunAsync();
