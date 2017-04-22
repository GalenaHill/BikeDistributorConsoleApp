namespace BikeDistributor.Core.Contracts
{
    using System.Collections.Generic;
    using System.Reflection;

    public interface IAssembliesProvider
    {
        IEnumerable<Assembly> Assemblies { get; }
    }
}