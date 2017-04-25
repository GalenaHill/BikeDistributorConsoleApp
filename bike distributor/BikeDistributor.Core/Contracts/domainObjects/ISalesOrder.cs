namespace BikeDistributor.Core.Contracts.domainObjects
{
    using System.Collections.Generic;
    using utilities;

    /// <summary>
    /// abstracts / encapsulates data and functionality of
    /// a sales order
    /// </summary>
    public interface ISalesOrder : IHasId<string>, IHasDiscountItem
    {
        // data
        ICustomerInfo CustomerInfo { get; set; }

        ICollection<ILineItem> LineItems { get; set; }

        decimal DiscountAmount { get; set; }

        decimal DiscountCoefficient { get; set; }

        decimal Subtotal { get; set; }

        decimal SubTotalNetDiscount { get; set; }

        decimal TaxAmount { get; set; }

        decimal TaxCoefficient { get; set; }

        decimal Total { get; set; }
        // fx
        void AddLine(ILineItem lineItem);


    }
}