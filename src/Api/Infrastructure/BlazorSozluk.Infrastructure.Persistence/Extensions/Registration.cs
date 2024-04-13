﻿using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Api.Domain.Models;
using BlazorSozluk.Infrastructure.Persistence.Context;
using BlazorSozluk.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorSozluk.Infrastructure.Persistence.Extensions;

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

        //api.webapi çalıştıgında fake data üreten blogumuz 
        //var seedData = new SeedData();
        //seedData.SeedAsync(configuration).GetAwaiter().GetResult();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
