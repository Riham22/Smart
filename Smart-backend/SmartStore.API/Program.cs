using Microsoft.EntityFrameworkCore;
using SmartStore.Infrastructure.Persistence;
using SmartStore.Application;
using SmartStore.Application.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// --- [قسم تسجيل الخدمات - كل شيء يوضع قبل builder.Build] ---

// 1. تسجيل الـ DbContext والـ Interface الخاص به
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<ApplicationDbContext>());

// 2. تسجيل خدمات مشروع الـ Application (MediatR)
builder.Services.AddApplicationServices();

// 3. تسجيل الـ CORS لتوفير الأمان لـ Next.js
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // بورت الـ Next.js الافتراضي
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// 4. تسجيل الـ Controllers والـ OpenAPI
builder.Services.AddControllers();
builder.Services.AddOpenApi();


// --- [بناء التطبيق الفعلي - ممنوع تسجيل خدمات بعد هذا السطر] ---
var app = builder.Build();


// --- [قسم الـ Middleware وخط أنابيب طلبات الـ HTTP] ---

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// تفعيل الـ CORS لتأمين الاتصال
app.UseCors("AllowFrontend");

// ربط الـ Endpoints الخاصة بالـ Controllers بالنظام
app.MapControllers();

// تشغيل السيرفر
app.Run();
