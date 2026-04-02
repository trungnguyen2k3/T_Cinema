
﻿using CinemaBE.Hubs;
using CinemaBE.Models;

﻿using CinemaBE.Models;

using CinemaBE.Services;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services cơ bản
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Đăng ký DbContext
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Đăng ký DI cho Service
builder.Services.AddScoped<IAccountService, AccountServiceImpl>();
builder.Services.AddScoped<IChatService, ChatServiceImpl>();

// 4. Đăng ký SignalR
builder.Services.AddSignalR();

// 5. Đăng ký CORS cho frontend local
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:3000",
                "http://127.0.0.1:5500",
                "http://localhost:5500",
                "https://127.0.0.1:5500",
                "https://localhost:5500"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// 6. Tắt validate model mặc định
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.Configure<CloudinarySettings>(
    builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddSingleton(sp =>
{
    var config = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();

    var account = new Account(
        config!.CloudName,
        config.ApiKey,
        config.ApiSecret
    );

    var cloudinary = new Cloudinary(account);
    cloudinary.Api.Secure = true; // luôn trả URL HTTPS

    return cloudinary;
});
var app = builder.Build();

// 7. Swagger chỉ bật ở môi trường Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 8. Middleware pipeline

// Tạm comment dòng này nếu muốn test SignalR local qua HTTP cho dễ
// app.UseHttpsRedirection();

// BẮT BUỘC phải có dòng này để CORS hoạt động
app.UseCors("AllowFrontend");

app.UseAuthorization();

// 9. Map controller API
app.MapControllers();


// 10. Map SignalR Hub cho chat realtime
app.MapHub<ChatHub>("/chatHub");

// 11. Run app
app.Run();

app.Run();
public partial class Program { }

