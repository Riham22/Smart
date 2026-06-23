using Microsoft.Extensions.DependencyInjection;

namespace SmartStore.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // تسجيل الـ MediatR ليفحص المشروع ويبحث عن أي Handler تلقائياً
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
