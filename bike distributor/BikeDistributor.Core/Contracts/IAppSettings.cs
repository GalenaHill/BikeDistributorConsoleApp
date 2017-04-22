namespace BikeDistributor.Core.Contracts
{
    public interface IAppSettings
    {
        T Get<T>(string key, T defaultValue);
    }
}