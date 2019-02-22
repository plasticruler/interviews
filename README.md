# Realmdigital Pre-Interview Refactoring #
## Initial impression
1. I think this code is supposed to be some kind of product catalogue service exposed via a web interface.
2. The data is sourced from an external service (perhaps a vendor service) which by the looks of it is insecure.
3. It's apparent that both services are not REST compliant. 
4. An example of the point in 3 is that the wrong verb is used to GET a product but as that data source is a vendor service it's something you'll just have to work with I guess. I'll just assume it's what it is but it's probably not a good idea not to foresee that this dependency would be questioned by people doing the interview.
5. There's a lot of mapping going about with closely related objects, I would use automap for that.
6. No strong typing of the results for these services.
7. This code is not written according to SOLID principles.

## Obvious bugs
1. There is no error handling so what happens when an invalid search parameter is used?
2. There is no authentication or rate-limiting.
3. The data service is too tightly coupled to the product catalogue service making either untestable. Rather adopt the Repository Pattern.
4. The product catalogue service will always return the ZAR currency, this should be configurable (either as part of authentication / jwt token or configuration file or passed in as a parameter in the search).
5. The vendor service will extremely poorly written. 

## Architecture
1. I would probably decouple the price records from the product as the current architecture does not scale well. Why load all of the prices and currencies when say most of your products are sold in ZAR anyway? In this way you could have different prices for different sales channels, and you can run promotional prices over seasons or have special prices for partners.
2. I would create a currency service and price the products in the source cost currency, and use the service to apply a markup and local currency.
3. I would convert the calls to asynchronous using Task based processing, seeing that you will be I/O bound for most of the time the service runs (assuming that the vendor service stays in place). You will get higher throughput and make less load on the thread pool.
5. Wrap the results into a message and data block, use server status codes to be more REST compliant (404 on invalid search etc)
6. I would also inject a repository into the controller, then create use Mock to create a fake repository and setup the controller to be unit tested.
