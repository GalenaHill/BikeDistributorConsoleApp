Sample Consulting Memo

Re:  bike distributor project refactoring

https://github.com/GalenaHill/BikeDistributorConsoleApp.git

Overview

The two main information management processes addressed in the solution are as follows:

1.  Order calculation of an incoming order;
2.  Receipt content generation based on a completed order.

The programmatic logic for handling 1 & 2 above should be (can be) abstracted extensibly.  This has been addressed via the IOrderManager type the functionality of which can be abstracted even further. 

Order calculation

The most dynamic process in the entire solution is the order calculation.  It is subject to a variety of main drivers: 
	a.  varying number of lines;
	b.  varying price and quantity per line; 
	c.  varying discount possibility at the individual line level;
	d.  varying bike details per line; 
	e.  varying discount possibility at the aggregate order level; 
	f.  configurable tax rate; 

Consequently, domain logic processing should accommodate the dynamic, varying nature of a-f above.  This is currently addressed via the IDiscountProvider type which is injected in the implementation of IOrderManager and the retrieval of the tax rate configuration via an IAppSettings object.    

Where the domain model composition is to remain intact (order has lines, line has bike), the issuance of discounts at order calculation time can only occur in 2 logical places:

1.  At the individual line level, driven by variables such as:
	a.  inventory aging;
	b.  quantity of bikes purchased;
	c.  quantity of bikes in inventory;
	d.  bike purchase price;

2.  At the order aggregate (sub-total) level driven by variables such as:
	a.  Gross sale volume discount;
	b.  Preferred customer discount;
	c.  Other – discretionary discount.

Consequently, at order computation time, the IDiscountProvider implementation must examine each order at both levels described above.  At the moment, discount issuance logic is limited to accommodate 1.a and 2.a above.  In a real-world implementation, the IDiscountProvider implementations can be refactored to accommodate all of the above and more.  Currently, the DiscountProvider object obtains discount tier settings from the injected implementation of the IMockDiscountRepository and dynamically issues discounts based on persistent (internal, management) discount tier settings.

Additional

The remaining functionality (e.g. unit testing) is not currently addressed at this time and will also be implemented in a real-world scenario.


