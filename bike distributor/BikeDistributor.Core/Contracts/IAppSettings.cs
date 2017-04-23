namespace BikeDistributor.Core.Contracts
{
    /// <summary>
    /// retrieves a value from App / Web confif based
    /// on a key / type params
    /// </summary>
    public interface IAppSettings
    {
        T Get<T>(string key, T defaultValue);
    }
}