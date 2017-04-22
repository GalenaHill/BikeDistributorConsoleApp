namespace BikeDistributor.Core.ExtensionMethods
{
    using System;
    using Autofac;
    using Autofac.Builder;

    public static class ContainerBuilderExtensions
    {
        public static IRegistrationBuilder<TService, ConcreteReflectionActivatorData, SingleRegistrationStyle>
            Register<TService>(this ContainerBuilder instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return instance.RegisterType<TService>()
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}