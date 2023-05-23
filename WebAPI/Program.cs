using Microsoft.EntityFrameworkCore;
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

builder.Services.AddSingleton(typeof(IMapper<,>), typeof(Mapper<,>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// middleware
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
