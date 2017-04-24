**Summary**

The refactored solution enables the following:

1.  Order processing (dynamic calculation and dynamic reciept issuance) for any inventory item (not just bikes), with receipt generation capability for any business object;

2.  Order calculation theory:  By nature of business, the calculation of a sales order is derivative of the following:

        a.  varying number of line items;
        b.  varying price and quantity per line;
        c.  varying discount possibilities at the product (inventory item) level;
        d.  varying inventory items and item attributes per line;
        e.  varying discount possibilities at the aggregate order (deal) level;
        f.  configurable tax rate;
        g.  manual overrides prior to order execution;
        
All refactoring now accommodates for the dynamic, varying nature of a-g above via the application of certain OOP principles (abstraction, encapsulation, certain polymorphic behavior) along with dependency injection over a layered architecture.

3. Discount theory:  Where the domain model composition is to remain intact ( **IOrder** has **ILineItems** , **ILineItem** has **IInventoryItem** (implemented as **Order** -&gt; **LineItem** (s) -&gt; **Bike** ), order calculation vis-a-vis discounts is naturally affected in 2 places:

a.  At the individual line level (where the inventory item part of each line has been initialized as a result of a frontline (sales) activity event);

b.  At the order aggregate (sub-total, deal) level (where the order's pre tax totals are now apparent as programmatic flow of control ensures that all line item objects present in the order have been initialized);

Consequently, at order computation time, an **IDiscountProvider** implementation is charged with &quot;scanning&quot; and retrieving the appropriate discount at both levels described above.

As refactored, discount issuance logic of the **IDiscountProvider** implementation closely models business process reality by dynamically accommodating for a variety of discount issuance events which can occur at _both_ the management (configuration, policy) level as well as at the sales (point of sale, wholesale) level without the need to recompile / redeploy.


Thanks for looking!

GalenaHill 
/ _A SaaS BI outfitter for the Man in the Arena_ / 
galenahill.com
