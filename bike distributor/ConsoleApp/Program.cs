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
            string company = "Go Fast Company";
            var bike1 = new Bike("OG-Rider", "OG-451", 455.99M, 120);
            var line1 = new Line(bike1, 1000);
            var order = new Order(company);
            order.AddLine(line1);
            var bike2 = new Bike("Slow-Rider", "OG-451.2", 121.99M, 10);
            var line2 = new Line(bike2, 500);
            order.AddLine(line2);
            #endregion

            var container = _createContainer();

            var generator = container.Resolve<RecieptGenerator>();

            Console.WriteLine(
                generator.GenerateReciept(order, ReceiptType.Plain));

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
