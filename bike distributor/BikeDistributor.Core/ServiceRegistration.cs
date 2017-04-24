namespace BikeDistributor.Core
{
    using System;
    using Autofac;
    using ExtensionMethods;
    using Functions;
    using Utilities;
    using System.Collections.Generic;
    using System.Reflection;
    using Contracts;
    using Enums;

    public class ServiceRegistration : Autofac.Module
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

            #region receipt generators and factory
            //  plain string reciept builder
            builder
                .RegisterAssemblyTypes(Assembly
                    .GetAssembly(typeof(ServiceRegistration)))
                .Where(t => !t.IsAbstract && t.IsClass)
                .AssignableTo<IReceiptGenerator>()
                .AsSelf()
                .Named<IReceiptGenerator>(
                Enum.GetName(typeof(ReceiptFunctionality), ReceiptFunctionality.OrderReceiptInStringFormat))
                .AsImplementedInterfaces();

            //  mock other type receipt builder
            builder
                .RegisterAssemblyTypes(Assembly
                    .GetAssembly(typeof(ServiceRegistration)))
                .Where(t => !t.IsAbstract && t.IsClass)
                .AssignableTo<IReceiptGenerator>()
                .AsSelf()
                .Named<IReceiptGenerator>(
                Enum.GetName(typeof(ReceiptFunctionality), ReceiptFunctionality.OrderReceiptInSomeOtherFormat))
                .AsImplementedInterfaces();

            // builder factory
            builder.Register<Func<string, IEnumerable<IReceiptGenerator>>>(
                c =>
                {
                    var ctx = c.Resolve<IComponentContext>();

                    return name => ctx.ResolveNamed<IEnumerable<IReceiptGenerator>>(name);
                });

            #endregion
        }
    }
}
