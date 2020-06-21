using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.EFCore;
using Shared.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CrossCutting.DependencyInjection
{
    public class DependencyInjectionModule
    {
        public static IConfiguration ConfigureJson()
        {
            var sharedFolder = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\SharedFiles"));
            return new ConfigurationBuilder()
                                         .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                         .AddJsonFile(Path.Combine(sharedFolder, $"appSettings.json"), optional: false, reloadOnChange: true)
                                         .AddJsonFile($"appSettings.json", optional: true, reloadOnChange: true)
                                         .Build();
        }

        public static void ConfigureWeb(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ConfigureDbConnection(serviceCollection, configuration);
            ConfigureClassesDI(serviceCollection, configuration);
        }

        private static void ConfigureClassesDI(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddOptions();

            serviceCollection.Configure<DbConnectionConfig>(options => configuration.GetSection("DbConnection").Bind(options));

            Type[] typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "CrossCutting.Mapper");

            Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
            {
                return
                  assembly.GetTypes()
                          .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                          .ToArray();
            }

            serviceCollection.AddAutoMapper(typelist);
        }

        private static void ConfigureDbConnection(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
                .AddDbContext<ProductClientContext>();


        }
    }
}
