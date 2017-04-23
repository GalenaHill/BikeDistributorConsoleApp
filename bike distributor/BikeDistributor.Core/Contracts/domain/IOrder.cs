namespace BikeDistributor.Core.Contracts.domain
{
    using System.Collections.Generic;

    /// <summary>
    /// abstracts / encapsulates data and functionality of
    /// a sales order
    /// </summary>
    public interface IOrder : IHasId<string>
    {
        // data
        ICustomerSalesInfo CustomerSalesInfo { get; set; }

        ICollection<ILineItem> LineItems { get; set; }

        decimal DiscountAmount { get; set; }

        decimal ManualDiscountCoefficient { get; set; }

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