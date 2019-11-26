using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Plus.Kit.DI
{
    public class DIStarter
    {
        public Type StartupModule { get; }

        public ContainerBuilder ContainerBuilder { get; }

        private IServiceCollection ServiceDescriptors { get; }

        public List<DIModule> Instances { get; }

        public DIStarter(Type startupModule, IServiceCollection serviceDescriptors)
        {
            StartupModule = startupModule;
            ServiceDescriptors = serviceDescriptors;
        }

        public static DIStarter Create<TStartupModule>(IServiceCollection services) where TStartupModule : DIModule
        {
            return new DIStarter(typeof(TStartupModule), services);
        }

        public virtual void Initialize()
        {
            var moduleTypes = FindAllModuleTypes();
            RegisterModules(moduleTypes);
            CreateModules(moduleTypes);
            StartModules();
        }

        private List<Type> FindAllModuleTypes()
        {
            return DIModule.FindDependedModuleTypesRecursivelyIncludingGivenModule(StartupModule);
        }

        private void RegisterModules(ICollection<Type> moduleTypes)
        {
            foreach (var moduleType in moduleTypes)
            {
                ServiceDescriptors.AddTransient(moduleType);
            }
        }

        private void CreateModules(ICollection<Type> moduleTypes)
        {
            var serviceProvider = ServiceDescriptors.BuildServiceProvider();
            foreach (var moduleType in moduleTypes)
            {
                if (!(serviceProvider.GetService(moduleType) is DIModule instance)) continue;

                instance.ContainerBuilder = ContainerBuilder;
                instance.Configuration = serviceProvider.GetService<IConfigurationRoot>();
                Instances.Add(instance);
            }
        }

        private void StartModules()
        {
            Instances.ForEach(module => module.PreInitialize());
            Instances.ForEach(module => module.Initialize());
            Instances.ForEach(module => module.PostInitialize());
        }
    }
}