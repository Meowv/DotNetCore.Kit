using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Plus.Kit.DI
{
    public static class DiExtensions
    {
        public static IServiceProvider AddPlusDI<TStartupModule>(this IServiceCollection services)
            where TStartupModule : DIModule
        {
            var diStarter = AddDIStarter<TStartupModule>(services);
            return AutofacServiceProvider(services, diStarter);
        }

        private static DIStarter AddDIStarter<TStartupModule>(IServiceCollection services) where TStartupModule : DIModule
        {
            var starter = DIStarter.Create<TStartupModule>(services);
            starter.Initialize();
            return starter;
        }

        private static IServiceProvider AutofacServiceProvider(IServiceCollection services, DIStarter diStarter)
        {
            diStarter.ContainerBuilder.Populate(services);
            var applicationContainer = diStarter.ContainerBuilder.Build();
            return new AutofacServiceProvider(applicationContainer);
        }
    }
}