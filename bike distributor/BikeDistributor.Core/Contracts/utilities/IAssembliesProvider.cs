namespace BikeDistributor.Core.Contracts.utilities
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// scans for and provides all referenced assemblies
    /// </summary>
    public interface IAssembliesProvider
    {
        IEnumerable<Assembly> Assemblies { get; }
    }
}