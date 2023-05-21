using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Contracts;
using WebAPI.Model;
using WebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// ambil connection string yg ada di appsetting.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// konfigurasi bookingroomsdbcontext
builder.Services.AddDbContext<BookingRoomsDBContext>(options => options.UseSqlServer(connectionString));

// tambahin repositori
builder.Services.AddScoped<IGenericRepository<University>, UniversityRepository>();
builder.Services.AddScoped<IGenericRepository<Room>, RoomRepository>();
builder.Services.AddScoped<IGenericRepository<Role>, RoleRepository>();
builder.Services.AddScoped<IGenericRepository<Employee>, EmployeeRepository>();
builder.Services.AddScoped<IGenericRepository<Account>, AccountRepository>();
builder.Services.AddScoped<IGenericRepository<Booking>, BookingRepository>();
builder.Services.AddScoped<IGenericRepository<Education>, EducationRepository>();
builder.Services.AddScoped<IGenericRepository<AccountRole>, AccountRoleRepository>();

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
