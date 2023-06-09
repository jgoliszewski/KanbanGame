using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KanbanGame.Client;
using KanbanGame.Client.Services;
using Syncfusion.Blazor;

// License key here is legally published
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
    "Mgo+DSMBaFt+QHFqVkFrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQltjT35UckNmWHdYc3M=;Mgo+DSMBPh8sVXJ1S0d+X1ZPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXhTcEVkXXlbcXxdR2c=;ORg4AjUWIQA/Gnt2VFhhQlJDfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5VdkNjXntYdXVdT2hb;MjMzMDc2MUAzMjMxMmUzMTJlMzMzN0l5eTFQeUNCUTExVmliMDFGVUNRT2ZOejBmbkt4aEYvT1hFQ2tjeklQMEk9;MjMzMDc2MkAzMjMxMmUzMTJlMzMzN1VzdWs4bFZxaTQ2RmFTMDZreXU1ZE5NeG9PTG1INStMTVNaRG5qMldtUGM9;NRAiBiAaIQQuGjN/V0d+XU9HcVRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TcEVlWXhadnFUT2ldVg==;MjMzMDc2NEAzMjMxMmUzMTJlMzMzN2lXRzNVMW5ieDU1dlF3MlBONXZtRWNiSFVYMHdTc00zazh4bTdHdk5PZkU9;MjMzMDc2NUAzMjMxMmUzMTJlMzMzN2pkYzYrN3pVL1htV1pCdEVvUVpYY0g1OCt1UXRPaHlTNi9BZGNrbUZ0cjQ9;Mgo+DSMBMAY9C3t2VFhhQlJDfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5VdkNjXntYdXZVTmNb;MjMzMDc2N0AzMjMxMmUzMTJlMzMzN1VuazdYZXpaZDk4dFN0WkI2YnJGaUZ4cXdMditCUWZ5WUdNVWtpOVJHdVU9;MjMzMDc2OEAzMjMxMmUzMTJlMzMzN1AxTmp1SDBGampaZE56Y1lmZGg3K25lWU5jc2twZHBTTk85VlNrOHNmS1U9;MjMzMDc2OUAzMjMxMmUzMTJlMzMzN2lXRzNVMW5ieDU1dlF3MlBONXZtRWNiSFVYMHdTc00zazh4bTdHdk5PZkU9"
);

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
