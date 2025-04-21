
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Services;
using Services.Abstraction;
using Shared.ErrorsModels;
using Store.Project.Api.Extensions;
using Store.Project.Api.Middlewares;

namespace Store.Project.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);


            var app = builder.Build();

            // Configure the HTTP request pipeline.


            await app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
