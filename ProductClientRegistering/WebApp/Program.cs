using CrossCutting.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace WebApp
{
    public class Program
    {
        private static IConfiguration configuration;
        public static void Main(string[] args)
        {
            CreateHostBuilder(args);

            configuration = DependencyInjectionModule.ConfigureJson();
            IHostBuilder builder = CreateHostBuilder(args);

            IHost host = builder.Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
              .ConfigureServices((hostContext, services) =>
              {
                  hostContext.Configuration = configuration;
              })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel((context, options) =>
                    {
                        options.AddServerHeader = false;
                        options.Limits.MaxRequestBodySize = 1073741274;
                        options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(120);
                        options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(120);
                    });
                    webBuilder.UseIIS();
                    webBuilder.UseIISIntegration();
                });
    }
}
