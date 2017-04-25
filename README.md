**Solution refactoring memo**

As refacored, the solution has been enabled as follows:

1.  Order processing (dynamic calculation and dynamic reciept issuance) for any product item (not just bikes), with receipt / post transaction handling capability for any business object required to be receipted / handled in any manner desireable;

2.  Order calculation theory:  By nature of business, the calculation of a sales order is derivative of the following variables:

        a.  number of line items;
        b.  price and quantity per each line;
        c.  discount possibilities at the product (line item) level;
        e.  discount possibilities at the aggregate (sales order) level;
        f.  tax rate;
        
All refactoring now accommodates for the dynamic, varying nature of a-f above via the application of certain OOP principles (abstraction, encapsulation, certain polymorphic behavior) along with dependency injection over a layered architecture.

3. Discount theory:  Where the domain model composition is to remain intact ( **ISalesOrder** has **ILineItems** , **ILineItem** has **IProductInfo** (implemented as **SalesOrder** -&gt; **LineItem** (s) -&gt; **BikeProductInfo** ), order calculation vis-a-vis discounts is naturally affected in 2 areas:

a.  At the individual line level (e.g. product code, price, quantity, product purchase total specific discounts);

b.  At the order aggregate (sub-total, deal) level (e.g. preffered customer discounts, order volume discounts etc.);

Consequently, at order computation time, a collection of **IDiscountScanner** implementations is injected and charged with &quot;scanning&quot; and initializing each level with the appropriate discounts, based on extensible discount scanning logic.

As refactored, discount scanning logic of the **IDiscountScanner** implementation closely models business process reality by dynamically accommodating for a variety of discount issuance events which can occur at _both_ the management (configuration, policy) level as well as at the point of sale level.  This processes are now easily accomodated as discount scanner implementers can be added in order to encapsulate and provide varying discount issuance behaviour based on conifguration, sales activity, etc.

4.  Receipt issuance theory:  The creation of a receipt is typically subject to a variety of drivers (business policy, point of sale customer requirements, etc.).  At present, receipt generation and delivery has been handled in a polymorphic manner as named implementers of the **IReceiptManager** GetReciept() function are allowed to behave in a varying manner and injected dynamically based on the specific functionality desired by their caller.  This varying behavior can include format (string v html), form of submittal (e-mail v. sms) etc. and further covers receipt generation for any business object that is required to be receipted.   

5.  For further consideration:  Due to the test nature of the solution, the above functionality may not have been addressed optimally:  In a real-world scenario, a variety of business process requirements specific to industry / individual client must be obtained.  This solution is merely a demonstration of process analysis within the context of a programmatic (architectural) solution to a specific business problem with general design-time requirements.


Thank you for looking!

GalenaHill 
/ _A SaaS BI outfitter for the Man in the Arena_ / 
galenahill.com
