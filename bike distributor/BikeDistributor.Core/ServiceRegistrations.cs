namespace BikeDistributor.Core
{
    using System;
    using Autofac;
    using Autofac.Features.Scanning;
    using BikeDistributor.Core.Contracts;
    using BikeDistributor.Core.ExtensionMethods;
    using BikeDistributor.Core.Functions;
    using BikeDistributor.Core.Utilities;

    public class ServiceRegistrations : Autofac.Module
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

            builder.Register<MockDiscountRepository>().SingleInstance();

            builder.Register<DiscountProvider>();

            builder.Register<RecieptGenerator>();

            builder.Register<OrderManager>();
        }
    }
}
