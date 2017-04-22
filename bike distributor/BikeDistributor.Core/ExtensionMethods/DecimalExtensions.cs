namespace BikeDistributor.Core.ExtensionMethods
{
    using System;

    public static class DecimalExtensions
    {
        public static decimal Round(this decimal instance)
        {
            return Math.Round(instance, 0, MidpointRounding.AwayFromZero);
        }

        public static decimal RoundTo(this decimal instance, int decPlaces)
        {
            return Math.Round(instance, decPlaces, MidpointRounding.AwayFromZero);
        }

        public static decimal ConditionalRound(this decimal instance, decimal boundry)
        {
            return Math.Abs(instance) >= boundry ? Round(instance) : RoundTo(instance, 2);
        }
    }
}