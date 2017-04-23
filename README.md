Summary

As shipped, the refactored solution is now enabled to addresses the following:

Order processing for not just bikes, but also any other inventory item;

Dynamic order calculation - The most dynamic process in the entire solution is the order calculation. It is subject to a variety of main drivers:

a.  varying number of lines;
b.  varying price and quantity per line;
c.  varying discount possibility at the individual line level;
d.  varying inventory item details per line (affects discounts);
e.  varying discount possibility at the aggregate order level;
f.  configurable tax rate;
All refactoring now accommodates for the dynamic, varying nature of a-f above via the application of OOP fundamentals (abstraction, encapsulation, separation of concerns etc.).

Discount theory: Where the domain model composition is to remain intact ( IOrder has ILineItems , ILineItem has IInventoryItem (implemented as Order -> LineItem (s) -> Bike ), the issuance of discounts at order calculation time can only occur in 2 logical places:
a. At the individual line level;

b. At the order aggregate (sub-total) level;

Consequently, at order computation time, an IDiscountProvider implementation is charged with "scanning" and retrieving the appropriate discount at both levels described above.

As refactored, discount issuance logic of the IDiscountProvider implementation accommodates for a variety of discount settings which can be configured (set) at both the management (business) level and at the sales level without the need to provide additional logic (code).

Thanks for looking!