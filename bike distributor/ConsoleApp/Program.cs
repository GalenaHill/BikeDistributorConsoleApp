namespace ConsoleApp
{
    using System;
    using System.Linq;
    using Autofac;
    using BikeDistributor.Core.DomainObjects;
    using BikeDistributor.Core.Enums;
    using BikeDistributor.Core.Functions;
    using BikeDistributor.Core.Utilities;

    class Program
    {
        static void Main(string[] args)
        {
            #region initialize mock order

            var bike1 = new BikeProductInfo(
                "OG-Rider",
                "OG-451",
                455.99M,
                "ABC-123");

            var line1 = new LineItem(bike1, 1000);

            var bike2 = new BikeProductInfo(
                "Slow-Rider",
                "OG-451.2",
                121.99M,
                "FGH-789");

            var line2 = new LineItem(bike2, 1
                );

            var order = new SalesOrder(
                new CustomerInfo()
                {
                    CustomerId = "CustomerId-123",
                    CustomerName = "GoFastCustomer",
                    DiscountCode = "CCC"
                })
            {
                ManualDiscount = .0M
            };

            order.AddLine(line1);

            order.AddLine(line2);

            #endregion

            var container = _createContainer();

            var orderManager = container.Resolve<OrderManager>();

            Console.WriteLine(
                orderManager.HandleOrderPostSale(
                    orderManager.CalcualteOrder(order), 
                    OrderPostSaleFunctionality.OrderReceiptInStringFormat));
        }

        private static IContainer _createContainer()
        {
            var assemblies = AssembliesProvider
                           .Instance
                           .Assemblies
                           .ToArray();

            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(assemblies);

            return builder.Build();
        }
    }
}
