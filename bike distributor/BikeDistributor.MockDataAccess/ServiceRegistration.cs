namespace BikeDistributor.MockDataAccess
{
    using System;
    using Autofac;
    using Core.ExtensionMethods;

    public class ServiceRegistrations : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            // single instance - 
            // only because of test mock data initialization present in this object
            builder.Register<MockDiscountRepository>().SingleInstance();
        }
    }
}
