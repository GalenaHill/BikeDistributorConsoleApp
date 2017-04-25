namespace BikeDistributor.Core.Contracts.utilities
{
    using System.Collections.Generic;
    using domainObjects;

    public interface IHasDiscountItem
    {
        ICollection<IDiscountItem> DiscountItems { get; set; }

        void AddDiscount(IDiscountItem discountItem);

        decimal ManualDiscount { get; set; }

        decimal GetTotalDiscount();
    }
}