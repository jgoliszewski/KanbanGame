using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using KanbanGame.Client;
using KanbanGame.Client.Services;
using Syncfusion.Blazor;

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
    "MTU2MjM4NkAzMjMxMmUzMTJlMzMzNWo3RjFkSmwwSUU2cmN3UXJIRnNWUFlsZ2R3R2E1YkVnSEpVdFc3UHBHa2M9;Mgo+DSMBaFt+QHFqVkNrWE5CaV1CX2BZfFlzQmldfE4QCV5EYF5SRHVcQlxnTHlXdUFlXX0=;Mgo+DSMBMAY9C3t2VFhhQlJBfVtdW3xLflF1VWFTell6cFZWACFaRnZdQV1gSn5SckBkXXxad3NX;Mgo+DSMBPh8sVXJ1S0d+X1RPckBEQmFJfFBmR2lceFR0d0UmHVdTRHRcQl5gSX9Xc0JnWntXdnY=;MTU2MjM5MEAzMjMxMmUzMTJlMzMzNUhPQ2xhdXVuU0Q2WlZUdXJhaUoxdkxjaW1EaUZ5aUNJR1l6dld5Tlo2dkU9;NRAiBiAaIQQuGjN/V0d+XU9Hc1RGQmVWfFN0RnNbdVt3flFFcDwsT3RfQF5jSn1Td0FmXntddXdVRA==;ORg4AjUWIQA/Gnt2VFhhQlJBfVtdW3xLflF1VWFTell6cFZWACFaRnZdQV1gSn5SckBkXXxbdHNX;MTU2MjM5M0AzMjMxMmUzMTJlMzMzNWFibTR3TVJCakc3Z3NzSjIvM2dBMENZTUdDTFBkTllvM29iNG9jSG9GbmM9;MTU2MjM5NEAzMjMxMmUzMTJlMzMzNVF2UDJFVmV1MHdORFVzTEorTGN5OU9IZmlGRTZYZlo2TWNuM254UWZOd009;MTU2MjM5NUAzMjMxMmUzMTJlMzMzNUxBMUV3T2Q2czlPd0g4bGxMWG9xMFNiZkNrY0ZtVTVhTVBrb2ZlNWlVNWc9;MTU2MjM5NkAzMjMxMmUzMTJlMzMzNVc5bEZQZUxHTXFqOGU4VHJPdzk4U3RleUg2Y3RXRkN4enVWSjNWNEdmRVk9;MTU2MjM5N0AzMjMxMmUzMTJlMzMzNWo3RjFkSmwwSUU2cmN3UXJIRnNWUFlsZ2R3R2E1YkVnSEpVdFc3UHBHa2M9"
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
