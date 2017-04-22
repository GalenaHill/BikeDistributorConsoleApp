# BikeDistributorConsoleApp
Sample project 

<p>&nbsp;</p>
<p>&nbsp;</p>
<p><strong>Main Changes</strong></p>
<p><strong>Abstraction, Encapsulation, Separation of Concerns &ndash; introduced from scratch as they were not present in the initial solution<br /> </strong><br /> The main functions (retrieval of discount tiers, the discount calculation and receipt generation) have all been abstracted, encapsulated and injected where needed via Autofac and a &ldquo;Utility Support System&rdquo; consisting of a variety of objects, enumerations and extension methods.&nbsp; Certain properties and types on the Bike, Line and Order objects have also been refactored in order to properly serve the changes above.</p>
<p><strong>Business rules on issuance of discounts that more closely resemble the real world</strong></p>
<ol>
<li>Introduction of discount coefficient tiers based on inventory age. Why:</li>
<li>Many flooring lines require you to pay of larger and larger portions based on the age of certain items. This hurts cash, and nothing that hurts cash is good.</li>
<li>Inventory may depreciate with time and the introduction of newer models. This causes retail value to suffer thus leading to lower sales volume and lower gross margins. Once again, old units need to move.</li>
<li>Discount coefficient tiers based on the size of the order where the most appropriate measure of such size is the orders gross pre-tax amount (i.e. &ldquo;Volume Discounts&rdquo;). Why:&nbsp; Volume based discounts are standard practice.</li>
</ol>
<p>Basically, via 1 and 2 above, you are ensuring that (a) older units move faster and (b) your customers are incentivized to place large(r) orders.</p>
<p>For example:</p>
<p>Aging based discount tiers</p>
<ol>
<li>For all units over 20 days in inventory, discount = 5%;</li>
<li>For all units over 50 days in inventory, discount = 10%;</li>
<li>For all units over 90 days in inventory, discount = 20%;</li>
</ol>
<p>Volume based discount tiers</p>
<ol>
<li>For all orders over $ 1000, discount = 5%;</li>
<li>For all orders over $ 5000, discount = 10%;</li>
<li>For all orders over $ 10,000, discount = 20%;</li>
</ol>
<p>The retailer can introduce as many tiers as needed.</p>
<p>The order calculating and receipt generating system will now dynamically accommodate any sort of bike / pricing combinations.&nbsp; The discount tiers are made available via a IMockDiscountRepository object.&nbsp; Why &ndash; such tiered settings are &ldquo;internal&rdquo; in nature and can be / should be managed without having to introduce any new order processing logic / code (i.e. they can be entered via a &ldquo;form&rdquo; and persisted in an internal data store from where they can be retrieved and used by the order processing system). &nbsp;</p>
<p><strong>Unit testing &ndash; excluded at this time.</strong></p>
