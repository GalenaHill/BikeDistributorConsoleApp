namespace BikeDistributor.Core.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Web.Compilation;
    using System.Web.Hosting;
    using Contracts.utilities;

    public class AssembliesProvider : IAssembliesProvider
    {
        private static readonly IEnumerable<string> KnownPrefixes = new[]
        {
            "BikeDistributor"
        };

        private static readonly object SyncLock = new object();
        private static IEnumerable<Assembly> _assemblies;

        public static IAssembliesProvider Instance { get; } = new AssembliesProvider();

        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (_assemblies != null)
                {
                    return _assemblies;
                }

                var available = AvailableAssemblies();

                lock (SyncLock)
                {
                    if (_assemblies == null)
                    {
                        _assemblies = available;
                    }
                }

                return _assemblies;
            }
        }

        private static IEnumerable<Assembly> AvailableAssemblies()
        {
            return HostingEnvironment.IsHosted ?
                WebAssemblies() :
                ConsoleAssemblies();
        }

        private static IEnumerable<Assembly> ConsoleAssemblies()
        {
            var path = Path.GetDirectoryName(
                Assembly.GetExecutingAssembly().Location);

            if (path == null)
            {
                throw new InvalidOperationException("Unable to get directory location.");
            }

            var files = Directory.GetFiles(path, "*.dll")
                .Where(file => KnownPrefixes.Any(p => (Path.GetFileName(file) ?? string.Empty)
                    .StartsWith(p, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            var allAssemblies = files.Select(Assembly.LoadFile)
                .ToList();

            allAssemblies.Add(Assembly.GetEntryAssembly());

            var result = allAssemblies
                .Where(a => a != null)
                .Distinct()
                .ToList();

            return result;
        }

        private static IEnumerable<Assembly> WebAssemblies()
        {
            var list = BuildManager.GetReferencedAssemblies()
                .Cast<Assembly>()
                .Where(a => !a.GlobalAssemblyCache)
                .Where(a => !a.IsDynamic)
                .Where(a => !a.ReflectionOnly)
                .Where(a =>
                    KnownPrefixes.Any(kp => a.FullName.StartsWith(kp, StringComparison.Ordinal)))
                .ToList();

            return list;
        }
    }
}