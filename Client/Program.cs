using Client.Repositories;
using Client.Repositories.Data;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(GeneralRepository<,>));
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

// session
/*builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(10);
});*/

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        }; 
    });    

/*builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();*/

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
/*
app.UseSession();*/
/*app.UseAuthentication();*/

app.UseRouting();
app.UseSession();
app.Use(async (context, next) =>
{
    var JWToken = context.Session.GetString("JWToken");
    if (!string.IsNullOrEmpty(JWToken))
    {
        context.Request.Headers.Add("Authorization", "Bearer" + JWToken);
    }

    await next();
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=University}/{action=Index}/{id?}");

app.Run();
