using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;
using WebAPI.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// ambil connection string yg ada di appsetting.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// konfigurasi bookingroomsdbcontext
builder.Services.AddDbContext<BookingRoomsDBContext>(options => options.UseSqlServer(connectionString));

// tambahin repositori

/*builder.Services.AddScoped<IGenericRepository<University>, GenericRepository<University>>();*/
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();

builder.Services.AddScoped<ITokenService, TokenService>();

//email service
builder.Services.AddTransient<IEmailService, EmailService>(_ => new EmailService(
    smtpServer: builder.Configuration["Email:SmtpServer"],
    smtpPort: int.Parse(builder.Configuration["Email:SmtpPort"]),
    fromEmailAddress: builder.Configuration["Email:FromEmailAddress"]
));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options => {
           options.RequireHttpsMetadata = false;
           options.SaveToken = true;
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateAudience = false,
               ValidAudience = builder.Configuration["JWT:Audience"],
               ValidateIssuer = false,
               ValidIssuer = builder.Configuration["JWT:Issuer"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
               ValidateLifetime = true,
               ClockSkew = TimeSpan.Zero
           };
       });

builder.Services.AddSingleton(typeof(IMapper<,>), typeof(Mapper<,>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => {
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MCC78",
        Description = "REST API for booking rooms"
    });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// middleware
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
