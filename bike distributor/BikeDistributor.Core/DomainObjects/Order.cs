namespace BikeDistributor.Core.DomainObjects
{
    using System.Collections.Generic;

    public class Order
    {
        public IList<Line> Lines = new List<Line>();


        public Order(string company)
        {
            Company = company;
        }

        public string Company { get; private set; }

        public void AddLine(Line line)
        {
            Lines.Add(line);
        }

    }
}
