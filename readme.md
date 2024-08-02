# C# app with Twitter and Github integration
 To run on Windows, just run the provided exe. It can be found in the directory deploy/windows.
 
Open http://localhost:5000/swagger/ in your favourite browser. 


To build the solution, .net SDK 6.X is needed.


Databox dashboards: 

- Weekly tweets per day - Ptuj: https://app.databox.com/datawall/564de1786604cb80067274868751df5d062532a52

- Jskeet's Github User info: https://app.databox.com/datawall/5c3bd6d3a8bc5f3b3d89074bf8bb295f062532b08



Although, the data in the implemented integrations can change often, we don't need to react to it at the same moment 
because we are only interested in their daily summaries. That is why the scheduler is configured/hardcoded to execute once per day.
Executes once when the host is started and after each 24h. Each integration executes sequentially.

Improvements:
- Run the scheduled integrations in parallel instead of sequentially.
- Additional configurations can be implemented to expand the currently (hardcoded) limitations, as query data and schedule interval.
- Persist the received data from integrations in database/cache/memory/queue and send to databox in batches. We can avoid generating additional
network traffic for each request and work within the Databox (possible?) API rate limit. This could come handy if we have a lot of traffic in out APIs.
- Add a queue for the received API requests and execute them eventially.
- Improve APIs to return information about the imported data.
- If different scaling is needed for each integration, each integrations' host can be separated and scaled independently.
- Each integration can have different/independent scheduler.
- Add additional logging, retry logic.
- Add additional tests.
- Add API versioning, rate limiting, authentication? etc..
- Centrally manage dotnet version and packages (nugets) version.
- Improve API response handling
- Add additional unit tests for all of the services (currently only TwitterService has a few tests). 
	
Each integration in the solution is an independent project (classlib/dll), and can be used independently in external implementations.
Each integration can additionaly have its own github repo, build and release pipelines.
