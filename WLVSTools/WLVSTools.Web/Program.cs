var builder = WebApplication.CreateBuilder(args);

//Add Services to the Container
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Configure the HTTP Request Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    //The default HSTS value is 30 days
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();   

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}");

app.Run();
