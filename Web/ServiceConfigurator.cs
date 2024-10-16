using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Services;
using Data.Repositories;
using Data.Entities;

namespace Web
{
    public static class ServiceConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Configure SmtpSettings
            services.Configure<SMTPSetting>(services.BuildServiceProvider().GetService<IConfiguration>().GetSection("SmtpSettings"));
        }
    }
}
