namespace BikeDistributor.Core
{
    using System;
    using Autofac;
    using ExtensionMethods;
    using Functions;
    using Utilities;

    public class ServiceRegistration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.RegisterInstance(AssembliesProvider.Instance)
            .AsImplementedInterfaces()
            .SingleInstance();

            builder.Register<AppSettings>().SingleInstance();

            builder.Register<DiscountProvider>();

            builder.Register<OrderManager>();
        }
    }
}
