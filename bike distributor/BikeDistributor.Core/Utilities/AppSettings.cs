using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using BikeDistributor.Core.Contracts;

namespace BikeDistributor.Core.Utilities
{
    public class AppSettings : IAppSettings
    {
        private readonly NameValueCollection storage;

        public AppSettings()
        {
            this.storage = ConfigurationManager.AppSettings;
        }

        public static IAppSettings Instance { get; } = new AppSettings();

        public T Get<T>(string key, T defaultValue)
        {
            var value = this.storage[key];

            if (string.IsNullOrWhiteSpace(value))
            {
                return defaultValue;
            }

            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (!converter.CanConvertFrom(typeof(string)))
            {
                return defaultValue;
            }

            var convertedValue = (T)converter.ConvertFromString(value);

            return convertedValue;
        }
    }
}