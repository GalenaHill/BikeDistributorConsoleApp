**Summary**

The refactored solution is now enabled to address the following:

1.  Order processing (dynamic calculation and dynamic reciept issuance) for any inventory item (not just bikes), with receipt generation capability for any business object of any supported type / format;

2.  Order calculation theory:  By nature of business, the calculation of a sales order is derivative of a variety of main drivers:

        a.  varying number of line items;
        b.  varying price and quantity per line;
        c.  varying discount possibility at the individual line level;
        d.  varying inventory item details per line (affects discounts);
        e.  varying discount possibility at the aggregate order level;
        f.  configurable tax rate;

All refactoring now accommodates for the dynamic, varying nature of a-f above via the application of OOP principles (abstraction, encapsulation, certain polymorphic behavior), dependency injection and a layered architecture.

3. Discount theory:  Where the domain model composition is to remain intact ( **IOrder** has **ILineItems** , **ILineItem** has **IInventoryItem** (implemented as **Order** -&gt; **LineItem** (s) -&gt; **Bike** ), the issuance of discounts at order calculation time can only occur in 2 logical places:

a.  At the individual line level;

b.  At the order aggregate (sub-total) level;

Consequently, at order computation time, an **IDiscountProvider** implementation is charged with &quot;scanning&quot; and retrieving the appropriate discount at both levels described above.

As refactored, discount issuance logic of the **IDiscountProvider** implementation closely models business process reality by dynamically accommodating for a variety of discount issuance events which can occur at _both_ the management (configuration, policy) level as well as at the sales (point of sale, wholesale) level without the need to recompile / redeploy.

Thank you for loking!