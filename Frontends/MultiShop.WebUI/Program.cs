
using MultiShop.WebUI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Service registration
builder.Services.AddWebUIServices(builder.Configuration);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Error/Page/403";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Page/500");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/ErrorPage/Error{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

var supportedCultures = new[] { "en", "fr", "de", "it", "tr" };
var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[4]).AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
