using CrossCutting.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.EFCore;
using Shared.Config;
using System;

namespace IBSLaw.Test
{
    public class SolutionContextCoreFactory : IDesignTimeDbContextFactory<ProductClientContext>
    {        
        public ProductClientContext CreateDbContext(string[] args)
        {
            IServiceCollection servicesCollection = new ServiceCollection();

            var configuration = DependencyInjectionModule.ConfigureJson();

            DependencyInjectionModule.ConfigureServices(servicesCollection, configuration);

            var services = servicesCollection.BuildServiceProvider();

            using IServiceScope serviceScope = services.CreateScope();

            IOptionsSnapshot<DbConnectionConfig> options = serviceScope.ServiceProvider.GetService<IOptionsSnapshot<DbConnectionConfig>>();

            var builder = new DbContextOptionsBuilder<ProductClientContext>();
            builder.UseSqlServer(options.Value.GetConnectionString());

            return new ProductClientContext(/*builder.Options,*/ options);
        }
    }
}
