using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restuarant.Models;
using Restuarant.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMvc(x => x.EnableEndpointRouting = false);

// Get the connection string from environment variables
string connectionString = Environment.GetEnvironmentVariable("SQLCONNECTIONSTRING");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Register repositories
builder.Services.AddScoped<IRepository<MasterMenu>, MasterMenuRepository>();
builder.Services.AddScoped<IRepository<MasterOffer>, MasterOfferRepository>();
builder.Services.AddScoped<IRepository<TransactionBookTable>, TransactionBookTableRepository>();
builder.Services.AddScoped<IRepository<TransactionContactUs>, TransactionContactUsRepository>();
builder.Services.AddScoped<IRepository<TransactionNewsletter>, TransactionNewsletterRepository>();
builder.Services.AddScoped<IRepository<SystemSetting>, SystemSettingRepository>();
builder.Services.AddScoped<IRepository<MasterContactUsInformation>, MasterContactUsInformationRepository>();
builder.Services.AddScoped<IRepository<MasterServices>, MasterServicesRepository>();
builder.Services.AddScoped<IRepository<MasterPartner>, MasterPartnerRepository>();
builder.Services.AddScoped<IRepository<MasterSocialMedia>, MasterSocialMediaRepository>();
builder.Services.AddScoped<IRepository<MasterWorkingHours>, MasterWorkingHoursRepository>();
builder.Services.AddScoped<IRepository<MasterCategoryMenu>, MasterCategoryMenuRepository>();
builder.Services.AddScoped<IRepository<MasterItemMenu>, MasterItemMenuRepository>();
builder.Services.AddScoped<IRepository<MasterSlider>, MasterSliderRepository>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Admin/Account/Login";
});

var app = builder.Build();

app.UseRouting();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseMvcWithDefaultRoute();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Account}/{action=Login}/{Id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{Id?}"
    );
});

app.Run();
