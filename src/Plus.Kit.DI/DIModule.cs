using Autofac;
using Microsoft.Extensions.Configuration;
using Plus.Kit.DI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Plus.Kit.DI
{
    public abstract class DIModule
    {
        protected internal ContainerBuilder ContainerBuilder { get; internal set; }

        protected internal IConfiguration Configuration { get; internal set; }

        /// <summary>
        /// PreInitialize
        /// </summary>
        public virtual void PreInitialize() { }

        /// <summary>
        /// Initialize
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// PostInitialize
        /// </summary>
        public virtual void PostInitialize() { }

        public static List<Type> FindDependedModuleTypesRecursivelyIncludingGivenModule(Type type)
        {
            var list = new List<Type>();
            AddModuleAndDependenciesRecursively(list, type);
            return list;
        }

        private static void AddModuleAndDependenciesRecursively(List<Type> modules, Type type)
        {
            if (!IsDiModule(type))
                throw new Exception($"此类型不是DI模块：{type.AssemblyQualifiedName}");

            if (modules.Contains(type)) return;

            modules.Add(type);

            var dependedModules = FindDependedModuleTypes(type);
            dependedModules.ForEach(dependedModule =>
            {
                AddModuleAndDependenciesRecursively(modules, dependedModule);
            });
        }

        private static List<Type> FindDependedModuleTypes(Type type)
        {
            if (!IsDiModule(type))
                throw new Exception($"此类型不是DI模块：{type.AssemblyQualifiedName}");

            var list = new List<Type>();

            if (type.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = type.GetTypeInfo().GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        list.Add(dependedModuleType);
                    }
                }
            }
            return list;
        }

        private static bool IsDiModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.IsClass && !typeInfo.IsAbstract && !typeInfo.IsGenericType && typeof(DIModule).IsAssignableFrom(type);
        }
    }
}