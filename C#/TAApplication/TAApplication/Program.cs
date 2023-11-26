using TAApplication.Areas.Data;
using TAApplication.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using TAApplication.Areas.Identity.Services;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

//try3
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<TAUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() //Allows roles to be used
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();


// Create a policy for user u0000000 so they can access ApplicationDetails later
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CorrectUnid", policy =>
       policy.RequireAssertion(context =>
           context.User.HasClaim(c =>
               c.Value == "u0000000@utah.edu")));
});

builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

//Adds external login through google authentication

builder.Services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection =
                     builder.Configuration.GetSection("Authentication:Google");

                    options.ClientId = googleAuthNSection["ClientId"];
                    options.ClientSecret = googleAuthNSection["ClientSecret"];
                });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//Seeding for User/ Roles
using (var scope = app.Services.CreateScope())
{
    
    var DB = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DB.Database.EnsureCreated();
    var um = scope.ServiceProvider.GetRequiredService<UserManager<TAUser>>();
    var rm = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await DB.InitializeUsers(um, rm);
    //I can't find a way to update the applicants inside of ApplicantionDbContext, so I have to do it here.
    
    var applicants=await DB.InitializeApplication(um);
    if (applicants != null)
    {
        foreach(var application in applicants)
        {
            DB.Applicants.Add(application);
        }
        await DB.SaveChangesAsync();
    }
    var courses = await DB.InitializeCourse();
    if (courses != null)
    {
        foreach (var course in courses)
        {
            DB.Course.Add(course);
        }
        await DB.SaveChangesAsync();
    }
    var slots = await DB.InitializeSchedule(um);
    if (slots != null)
    {
        foreach (var slot in slots)
        {
            DB.Slot.Add(slot);
        }
        await DB.SaveChangesAsync();
    }
    var enrollments =await DB.InitializeEnrollment("temp.csv");
    if (enrollments != null)
    {
        foreach (var item in enrollments)
        {
            DB.Enrollments.Add(item);
        }
        await DB.SaveChangesAsync();
    }

}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
