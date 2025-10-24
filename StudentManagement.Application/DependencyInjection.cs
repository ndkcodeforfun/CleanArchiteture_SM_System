using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace StudentManagement.Application
{
    public static class DependencyInjection
    {
        // Đây chính là phương thức mở rộng bạn đang gọi ở Program.cs
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // 1. Đăng ký AutoMapper (ĐÃ SỬA LỖI)
            // Cú pháp mới cho AutoMapper v11+
            services.AddAutoMapper(cfg =>
            {
                // Quét toàn bộ Assembly để tìm các lớp kế thừa từ Profile
                // (như lớp MappingProfile bạn đã tạo)
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });

            // 2. Đăng ký MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // 3. Đăng ký FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
