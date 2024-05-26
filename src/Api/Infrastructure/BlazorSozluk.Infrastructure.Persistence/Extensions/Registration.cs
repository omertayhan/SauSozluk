using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Infrastructure.Persistence.Repositories;
using BlazorSozluk.Infrastructure.Persistence.Context;
using BlazorSozluk.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorSozluk.Api.Infrastructure.Persistence.Extensions;

public static class Registration
{
    public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        //db injections işlemi
        services.AddDbContext<BlazorSozlukContext>(conf =>
        {
            var connStr = configuration["BlazorSozlukDbConnectionString"].ToString();
            conf.UseSqlServer(connStr, opt =>
            {
                opt.EnableRetryOnFailure();
            });
        });

        //api.webapi çalıştıgında fake data üreten blok
        //var seedData = new SeedData();
        //seedData.SeedAsync(configuration).GetAwaiter().GetResult();
        services.AddScoped<DbContext, BlazorSozlukContext>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IEmailConfirmationRepository, EmailConfirmationRepository>();
        services.AddTransient<IEntryRepository, EntryRepository>();
        services.AddTransient<IEntryCommentRepository, EntryCommentRepository>();

        return services;
    }
}
