using Autofac;
using Plus.Kit.DI;

namespace Plus.Kit
{
    public class StartupDIModule : DIModule
    {
        public override void PreInitialize()
        {
            ContainerBuilder.RegisterType<WeatherServices>().As<IWeatherServices>();
        }

        public override void Initialize()
        {
            
        }

        public override void PostInitialize()
        {
            
        }
    }
}