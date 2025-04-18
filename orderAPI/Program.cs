using Microsoft.EntityFrameworkCore;
using orderAPI.Data; // This should match your namespace for AppDbContext
using orderAPI.Repositories;
using orderAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Register DbContext with MySQL (using Pomelo)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Repositories
builder.Services.AddScoped<IOutletRepository, OutletRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
//builder.Services.AddScoped<IQueueRepository, QueueRepository>();
//builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IBanRepository, BanRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

// Register Services
builder.Services.AddScoped<IOutletService, OutletService>();
builder.Services.AddScoped<IStaffService, StaffService>();
//builder.Services.AddScoped<IQueueService, QueueService>();
//builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IBanService, BanService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

//whatsapp
builder.Services.AddHttpClient<IWhatsAppService, WhatsAppService>();



var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
