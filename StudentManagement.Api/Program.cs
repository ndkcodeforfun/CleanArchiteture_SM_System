using StudentManagement.Application;
using StudentManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// 1. Đăng ký các dịch vụ từ các lớp khác
builder.Services.AddApplicationServices(); // Đăng ký MediatR, AutoMapper, Validators
builder.Services.AddInfrastructureServices(builder.Configuration); // Đăng ký DbContext, Repositories

// 2. Đăng ký các dịch vụ của API
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // TODO: Seed data
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
