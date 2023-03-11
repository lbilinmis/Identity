using Identity.CoreWebUI.Context;
using Identity.CoreWebUI.Middlewares;
using Identity.CoreWebUI.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DbIdentityContext>();

#region 1.Durum
//builder.Services.AddIdentity<AppUser, AppRole>(opt => {
//    opt.Password.RequireDigit = false;
//    opt.Password.RequireLowercase = false;
//    opt.Password.RequireUppercase = false;
//    opt.Password.RequiredLength = 1;
//    opt.Password.RequireNonAlphanumeric = false;
//    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);// 10 dk boyunca kullan�c� giri� yapamaaz
//    opt.Lockout.MaxFailedAccessAttempts = 3; // 3 defa yanl�� giri� olursa bloklama olacakt�r
//}).AddEntityFrameworkStores<DbIdentityContext>();

#endregion

#region 2.Durum
builder.Services.AddIdentity<AppUser, AppRole>(opt =>
{
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequiredLength = 1;
    opt.Password.RequireNonAlphanumeric = false;

    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);// 10 dk boyunca kullan�c� giri� yapamaaz
    opt.Lockout.MaxFailedAccessAttempts = 3; // 3 defa yanl�� giri� olursa bloklama olacakt�r
}).AddErrorDescriber<AppCustomIdentityValidator>().AddPasswordValidator<AppCustomPasswordValidator>()
    .AddEntityFrameworkStores<DbIdentityContext>();

#endregion

builder.Services.ConfigureApplicationCookie(opt =>
{
    opt.LoginPath = new PathString("/Home/UserLogin");
    opt.Cookie.HttpOnly = true;
    opt.Cookie.Name = "IdentityCookie";
    opt.Cookie.SameSite = SameSiteMode.Strict; //Eri�im engellenir
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    opt.ExpireTimeSpan = TimeSpan.FromDays(10);
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("FemalePolicy", cnf => { cnf.RequireClaim("gender", "female"); });

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.CustomStaticFiles();

app.UseRouting();

//Giri� yapan kullan�c�n�n yetkili oldu�u sayfalar�n g�r�nt�leme
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    //endpoints.MapDefaultControllerRoute();


    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Panel}/{action=Index}/{id?}");

});


app.Run();
