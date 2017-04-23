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

            var bike1 = new Bike(
                "OG-Rider", 
                "OG-451", 
                455.99M, 
                35, 
                "ENTER CODE HERE");

            var line1 = new LineItem(bike1, 1000);

            var bike2 = new Bike(
                "Slow-Rider", 
                "OG-451.2", 
                121.99M, 10, 
                "ENTER CODE HERE");

            var line2 = new LineItem(bike2, 500);

            var order = new Order(new CustomerSalesInfo()
            {
                CustomerId = "GoFastCustomerId",
                CustomerName = "GoFastCustomer",
                DiscountCode = "CCC"
            });

            order.AddLine(line1);

            order.AddLine(line2);

            #endregion

            var container = _createContainer();

            var orderManager = container.Resolve<OrderManager>();

            Console.WriteLine(
                orderManager.GetReceipt(
                    orderManager.CalcualteOrder(order), ReceiptType.Plain));
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
