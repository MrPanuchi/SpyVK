using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using SpyVK.Data;
using SpyVK.Entities;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var userDbConnectionString = builder.Configuration.GetConnectionString("UserDbContext");
builder.Services.AddDbContext<ApplicationUserDbContext>(config =>
{
    config.UseSqlite(userDbConnectionString);
})
    .AddIdentity<ApplicationUser,ApplicationRole>(config =>
    {
        config.SignIn.RequireConfirmedAccount = true;
        config.Password.RequireDigit = false;
        config.Password.RequireLowercase = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
        config.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<ApplicationUserDbContext>();

builder.Services.AddAuthentication()
    .AddOAuth("VK", "VKontakte", config =>
    {
        config.ClientId = builder.Configuration["Authentication:VKontakte:App_ID"];
        config.ClientSecret = builder.Configuration["Authentication:VKontakte:Secret_key"];
        config.CallbackPath = builder.Configuration["Authentication:VKontakte:CallbackPath"];
        config.TokenEndpoint = builder.Configuration["Authentication:VKontakte:TokenEndpoint"];
        config.AuthorizationEndpoint = builder.Configuration["Authentication:VKontakte:AuthorizationEndpoint"];
        builder.Configuration.GetSection("Authentication:VKontakte:Scopes").Get<List<string>>().ForEach(x => config.Scope.Add(x));
        config.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "user_id");
        config.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
        config.SaveTokens = true;
        config.Events = new Microsoft.AspNetCore.Authentication.OAuth.OAuthEvents
        {
            OnCreatingTicket = context =>
            {
                context.RunClaimActions(context.TokenResponse.Response.RootElement);
                return Task.CompletedTask;
            }
        };
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

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
