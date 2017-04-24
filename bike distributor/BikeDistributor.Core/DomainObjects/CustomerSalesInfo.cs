namespace BikeDistributor.Core.DomainObjects
{
    using Contracts;

    public class CustomerSalesInfo : ICustomerSalesInfo
    {
        public CustomerSalesInfo()
        {
            
        }

        public CustomerSalesInfo(
            string customerId, 
            string customerName, 
            string discountCode)
        {
            this.CustomerId = customerId;
            this.CustomerName = customerName;
            this.DiscountCode = discountCode;
        }

        public string CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string DiscountCode { get; set; }
    }
}