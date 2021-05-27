using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using WebApi;

namespace Tests
{
    public class DependencyResolverHelper
    {
        private readonly IHost _host;

        public DependencyResolverHelper() => _host = Host.CreateDefaultBuilder(new string[] {
            "testing"
        })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseSetting("TestProp", "true");
            }).Build();

        public T GetService<T>()
        {
            using (var serviceScope = _host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var scopedService = services.GetRequiredService<T>();
                    return scopedService;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
        }
    }
}