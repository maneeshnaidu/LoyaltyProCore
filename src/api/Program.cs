using api.Models;
using api.Data;
using api.Interfaces;
using api.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using api.Services;
using CloudinaryDotNet;
using api.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    option.OperationFilter<SwaggerFileUploadOperationFilter>();
});

// Add DBContext
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 12;
})
.AddEntityFrameworkStores<ApplicationDBContext>()
.AddDefaultTokenProviders();

// Add Authentication Service & Schemes
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
        )
    };
});

// Add Cloudinary configuration
var cloudinaryAccount = new Account(
    builder.Configuration["Cloudinary:CloudName"],
    builder.Configuration["Cloudinary:ApiKey"],
    builder.Configuration["Cloudinary:ApiSecret"]
);
builder.Services.AddSingleton(new Cloudinary(cloudinaryAccount));

// Add Repositories
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IIntegrationRepository, IntegrationRepository>();
builder.Services.AddScoped<IOutletRepository, OutletRepository>();
builder.Services.AddScoped<IPointsRepository, PointsRepository>();
builder.Services.AddScoped<IRewardRepository, RewardRepository>();
builder.Services.AddScoped<ITransactionsRepository, TransactionRepository>();
builder.Services.AddScoped<IVendorRepository, VendorRepository>();

// Add Services
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUploadFileService, UploadFileService>();

builder.Services.AddControllers();

// Add CORS for vercel frontend
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowVercel",
//         policy => policy
//             .WithOrigins("http://localhost:3000/") // Replace with your actual Vercel domain
//             .AllowAnyMethod()
//             .AllowAnyHeader());
// });

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireSuperAdminRole", policy => policy.RequireRole("SuperAdmin"));
    options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireStaffRole", policy => policy.RequireRole("Staff"));
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapControllers();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    await SeedRolesAndAdmin(roleManager, userManager);
}

app.UseHttpsRedirection();

app.UseCors(x => x
     .AllowAnyMethod()
     .AllowAnyHeader()
     .AllowCredentials()
      .WithOrigins("http://localhost:3000")
      .SetIsOriginAllowed(origin => true));

// Add Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.Run();

// Seed Default Admin User
static async Task SeedRolesAndAdmin(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
{
    string adminEmail = "superadmin@example.com";
    string adminPassword = "Admin@123456";

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser { UserName = "superadmin", Email = adminEmail, FirstName = "Super", LastName = "Admin", UserCode = 1234 };
        await userManager.CreateAsync(adminUser, adminPassword);
        await userManager.AddToRoleAsync(adminUser, "SuperAdmin");
    }
}


