using Autofac;
using ReactiveUI;
using Splat;
using Splat.Autofac;
using System.Reflection;

namespace GameStoreApp {
    public static class Bootstrapper {
        public static void Init() {
            var builder = new ContainerBuilder();

            builder.RegisterModule<AutofacRegisterAllMVVM>();
            builder.RegisterType<GameStoreContext>()
                   .AsSelf()
                   .SingleInstance();

            AutofacDependencyResolver resolver = new AutofacDependencyResolver(builder);
            Locator.SetLocator(resolver);
            Locator.CurrentMutable.InitializeSplat();
            Locator.CurrentMutable.InitializeReactiveUI();

            var container = builder.Build();
            resolver.SetLifetimeScope(container);
        }
    }

    public class AutofacRegisterAllMVVM : Autofac.Module {
        protected override void Load(ContainerBuilder builder) {
            // Register all ViewModels
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(t => t.Name.EndsWith("ViewModel"))
                   .AsImplementedInterfaces()
                   .AsSelf()
                   .SingleInstance();

            // Register all services
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .SingleInstance();


        }
    }
}
