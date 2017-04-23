namespace BikeDistributor.Core.ExtensionMethods
{
    using System;
    using System.Diagnostics;
    using Contracts;

    public static class AppSettingsExtensions
    {
        [DebuggerStepThrough]
        public static T Get<T>(this IAppSettings instance, string key)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return instance.Get(key, default(T));
        }
    }
}