
using Microsoft.EntityFrameworkCore;
using WebAPI.Model;
using WebAPI.Utility;

namespace WebAPI.Context
{
    public class BookingRoomsDBContext : DbContext
    {
        public BookingRoomsDBContext(DbContextOptions<BookingRoomsDBContext> options) : base(options) { }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> Universities { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().HasData(new Role
            {
                Guid = Guid.Parse("a0082ab9-4cde-4c07-ca74-08db60bf4a3f"),
                Name = nameof(RoleLevel.User),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            }, new Role {
                Guid = Guid.Parse("988f9a38-a740-4281-ca75-08db60bf4a3f"),
                Name = nameof(RoleLevel.Manager),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            }, new Role
            {
                Guid = Guid.Parse("f275ec7c-1322-4adc-ca76-08db60bf4a3f"),
                Name = nameof(RoleLevel.Admin),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            });

            builder.Entity<Employee>().HasIndex(e =>
            new
            {
                e.Nik,
                e.Email,
                e.PhoneNumber
            }).IsUnique();

            // buat relasi disini (universitas memiliki relasi one to many dengan educations. foreign key university id ada di educations)
            builder.Entity<Education>()
                .HasOne(u => u.University)
                .WithMany(e => e.Educations)
                .HasForeignKey(e => e.UniversityGuid);

            // relasi employee dengan account (1 to 1)
            builder.Entity<Account>()
                .HasOne(e => e.Employee)
                .WithOne(a => a.Account)
                .HasForeignKey<Account>(a => a.Guid);

            // relasi employee dengan education (1 to 1)
            builder.Entity<Education>()
                .HasOne(e => e.Employee)
                .WithOne(e => e.Education)
                .HasForeignKey<Education>(e => e.Guid);

            // relasi account dengan account roles (1 to many)
            builder.Entity<AccountRole>()
                .HasOne(ac => ac.Account)
                .WithMany(acr => acr.AccountRoles)
                .HasForeignKey(acr => acr.AccountGuid);

            // relasi roles dengan account roles (1 to many)
            builder.Entity<AccountRole>()
                .HasOne(r => r.Role)
                .WithMany(acr => acr.AccountRoles)
                .HasForeignKey(acr => acr.RoleGuid);
                // kalo mau ganti on cascade tulisnya disini cth => .OnDelete();

            // relasi employee dengan booking (1 to many)
            builder.Entity<Booking>()
                .HasOne(e => e.Employee)
                .WithMany(b => b.Bookings)
                .HasForeignKey(b => b.EmployeeGuid);

            // relasi booking dengan room (1 to many)
            builder.Entity<Booking>()
                .HasOne(r => r.Room)
                .WithMany(b => b.Bookings)
                .HasForeignKey(b => b.RoomGuid);


        }

        

    }
}
