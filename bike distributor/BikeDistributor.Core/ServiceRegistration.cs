namespace BikeDistributor.Core
{
    using System;
    using Autofac;
    using ExtensionMethods;
    using Functions;
    using Utilities;
    using System.Collections.Generic;
    using System.Reflection;
    using Enums;
    using Contracts.functions;

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

            builder.Register<OrderManager>();

            #region receipt generators and factory
            //  plain string reciept builder
            builder
                .RegisterAssemblyTypes(Assembly
                    .GetAssembly(typeof(ServiceRegistration)))
                .Where(t => !t.IsAbstract && t.IsClass)
                .AssignableTo<IReceiptManager>()
                .AsSelf()
                .Named<IReceiptManager>(
                Enum.GetName(typeof(ReceiptFunctionality), 
                        ReceiptFunctionality.OrderReceiptInStringFormat))
                .AsImplementedInterfaces();

            //  mock other type receipt builder
            builder
                .RegisterAssemblyTypes(Assembly
                    .GetAssembly(typeof(ServiceRegistration)))
                .Where(t => !t.IsAbstract && t.IsClass)
                .AssignableTo<IReceiptManager>()
                .AsSelf()
                .Named<IReceiptManager>(
                Enum.GetName(
                        typeof(ReceiptFunctionality), 
                        ReceiptFunctionality.OrderReceiptInSomeOtherFormat))
                .AsImplementedInterfaces();

            // builder factory
            builder.Register<Func<string, IEnumerable<IReceiptManager>>>(
                c =>
                {
                    var ctx = c.Resolve<IComponentContext>();

                    return name => ctx.ResolveNamed<IEnumerable<IReceiptManager>>(name);
                });

            #endregion

            // discount scanners
            builder.RegisterAssemblyTypes(this.ThisAssembly)
                .Where(t => !t.IsAbstract && t.IsClass)
                .AssignableTo<IDiscountScanner>()
                .AsSelf()
                .AsImplementedInterfaces();
        }
    }
}
