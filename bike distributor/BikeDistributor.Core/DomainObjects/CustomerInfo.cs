namespace BikeDistributor.Core.DomainObjects
{
    using Contracts.domainObjects;

    public class CustomerInfo : ICustomerInfo
    {
        public CustomerInfo()
        {
            
        }

        public CustomerInfo(
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