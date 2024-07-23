using Microsoft.Extensions.FileProviders;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using WLVSTools.Web.WebInfrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the Container
builder.Services.AddApplicationServices();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

var app = builder.Build();

//Configure the HTTP Request Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    //The default HSTS value is 30 days
    app.UseHsts();
}

app.UseHttpsRedirection();

setStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller}/{action}/{id?}");
app.MapControllerRoute(
    name: "customarea",
    pattern: "{area:exists}/{controller}/{action}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}");


app.Run();

void setStaticFiles()
{
    var isWindowsHosting = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    var processName = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().ProcessName);
    var isIISHosting = (processName.Contains("w3wp", StringComparison.OrdinalIgnoreCase) ||
        processName.Contains("iisexpress", StringComparison.OrdinalIgnoreCase));

    if (isWindowsHosting || isIISHosting)
    {
        // get the directory
        var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var assetDirectory = Path.Combine(assemblyDirectory, "Resources");

        // use it
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(assetDirectory),
            RequestPath = "/Resources"
        });
    }
    else
    {
        app.UseStaticFiles();
    }
}