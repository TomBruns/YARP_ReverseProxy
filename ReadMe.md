#YARP ReverseProxy Solution

This solution was created as a technology demonstrator for a number of webAPI concepts:
- Compare the webAPI implementation styles for:
	- legacy **Controller** style
	- more recent **Minimal AP** style
- Support standard Swagger features in both implementations:
	- Path based API versioning
	- (Response) Examples
- Support standard webAPI features in both implementations:
	- Health Checks		*(Note: this only work via direct access)*
		- https://localhost:7047/health  (raft)
		- https://localhost:7174/health  (express)
- Implement a front-end **Reverse Proxy** using YARP that can route requests to the correct backend implementation based on a part of the URL path.
	- Block downstream health check endpoints from external callers and return a 404
- Run performance tests (using k6) to compare the relative performance of:
	- Controller vs Mimimal API style webAPIs
	- latency impact of adding a YARP based reverse proxy in front of the webAPI implementations

![Overall Solution Architecture diagram](https://github.com/TomBruns/YARP_ReverseProxy/blob/master/images/architecture.png?raw=true)

##Reverse Proxy

The YARP reverse proxy is configured (*in **appsettings.json** *) to:

1. remove the **raft** or **express** path prefix before forwarding to the correct downstream cluster
	https://localhost:7296/raft/[remaining path]
	https://localhost:7296/express/[remaining path]  

2. add the X-Forwarded-* HTTP headers  (lines 14-17 below)   

	*Note: The **/debug/headers** webapi endpoint is useful to see what http headers are sent to the service* 

`https://localhost:7296/raft/api/v2/debug/headers`
```
[Accept]: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7
[Host]: 127.0.0.1:5285
[User-Agent]: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36 Edg/120.0.0.0
[Accept-Encoding]: gzip, deflate, br
[Accept-Language]: en-US,en;q=0.9
[traceparent]: 00-61d36cbafdfe50538baa4e5ba2cff2f6-2a3bd36a9912faaa-00
[sec-ch-ua]: "Not_A Brand";v="8", "Chromium";v="120", "Microsoft Edge";v="120"
[sec-ch-ua-mobile]: ?0
[sec-ch-ua-platform]: "Windows"
[sec-fetch-site]: none
[sec-fetch-mode]: navigate
[sec-fetch-user]: ?1
[sec-fetch-dest]: document
[X-Forwarded-For]: ::1
[X-Forwarded-Prefix]: /raft
[X-Forwarded-Host]: localhost:7296
[X-Forwarded-Proto]: https
```

`https://localhost:7296/express/api/v2/debug/headers`

```
[Accept]: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7
[Host]: 127.0.0.1:5233
[User-Agent]: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36 Edg/120.0.0.0
[Accept-Encoding]: gzip, deflate, br
[Accept-Language]: en-US,en;q=0.9
[traceparent]: 00-835aa7bab9105ff5008cc21565611126-a8c320bd3f17a727-00
[sec-ch-ua]: "Not_A Brand";v="8", "Chromium";v="120", "Microsoft Edge";v="120"
[sec-ch-ua-mobile]: ?0
[sec-ch-ua-platform]: "Windows"
[sec-fetch-site]: none
[sec-fetch-mode]: navigate
[sec-fetch-user]: ?1
[sec-fetch-dest]: document
[X-Forwarded-For]: ::1
[X-Forwarded-Prefix]: /express
[X-Forwarded-Host]: localhost:7296
[X-Forwarded-Proto]: http
```

##Swagger websites behind a Reverse Proxy

Typically you have two (2) choices about how to support Swagger websites behind a Reverse Proxy:
1. Host the OpenAPI files/Swagger Websites on the Reverse Proxy Itself making the appropriate changes to those OpenAPI files.
2. Expose the OpenAPI/Swagger Websites from the underlying services

If you choose (as this project did) option #2, you need to dynamically adjust the server information in the OpenAPI files so they will use the use the correct URL to come thru the Reverse Proxy.  

A new swagger extension `SwaggerReverseProxyExtensions` was created to use the **X-Forwarded-** info in the HTTP header (if present) to dynamically make the necessary changes in the OpenAPI file.

#Performance Testing Summary

##Controller vs Minimal API

![Controller vs Minimal API avg time graph](https://github.com/TomBruns/YARP_ReverseProxy/blob/master/images/ctlr_vs_min_avg_response.png?raw=true)

![Controller vs Minimal API p95 time graph](https://github.com/TomBruns/YARP_ReverseProxy/blob/master/images/ctlr_vs_min_p95_response.png?raw=true)

Notes:
- With **minimal webAPI logic**, Minimal APIs are roughtly 10% faster then Controller Style webAPIs
- This performance margin may be less significant as execution time inside the endpoint increases

![YARP Contribution graph](https://github.com/TomBruns/YARP_ReverseProxy/blob/master/images/yarp_contrib_to_response_time.png?raw=true)

Notes:
- YARP **adds less then 1 mSec** to overall latency

#Bonus Learnings

During the implementation, a number of bonus learning opportunities occured:
- How to define MinimalApi **endpoint groups** in separate class files (move out of program.cs)
- How to replicate all of the Swagger configuration typical on **Controller style** webAPIs when using **Minimal API** style webAPIs
- How to support **multiple API versions** when using **Minimal API** style webAPIs
- Use **FluentValidation** to validate input parms
- Use **ProblemDetails** to return unsuccessful responses
- Use IDocumentFilter to add the assy build timestamp into the info section of the OpenAPI file
- Use IDocumentFilter to add descriptions to OpenAPI tags (this is not currently supported for Minimal APIs)
- Uses custom attribute to control order that tag groups are displayed in Swagger (by default they are sorted alphabetically)
- Uses PreSerializeFilter and **X-Forwared-* headers**  to enable a Swagger website to work correctly when behind a reverse proxy



#Projects

This solution consists of the following Projects:

| csproj name | Description                    |
| ------------- | ------------------------------ |
| `Worldpay.US.ReverseProxy`   | Implements a reverse proxy using YARP     |
| `Worldpay.US.RAFT`      | Implements legacy **Comtroller** style webAPIs       |
| `Worldpay.US.Express`   | Implements newer **Minimal API** style webAPIs     |
| `Worldpay.US.Swagger.Extensions`   | A set of reuseable Swagger Extentions   |

#Swagger Extensions

A number of custom, reuseable Swagger Extensions were created:

| Name | Description                    |
| ------------- | ------------------------------ |
| `SwaggerBuildTimestampDocFilter` | Uses custom attribute added at webAPI assy compile time to add compile timstamp to OpenAPI file |
| `SwaggerControllerDisplayOrder` <br> `SwaggerControllerDisplayOrderAttribute ` | Controls display order of OpenAPI tags (controller style webAPIs) |
| `SwaggerDefaultValues` | Displays default values in Swagger Websites |
| `SwaggerReverseProxyExtensions` | If X-Forwarded-* http attributes are available, creates the necessary info in the OpenAPI Servers object  |
| `SwaggerTagDescriptionsDocFilter` | Allows you to add Descriptions to OpenAPI tags (not currently supported for Minimal APIs)|
| `SwaggerTagDisplayOrder`<br> `SwaggerTagDisplayOrderAttribute` | Controls display order of OpenAPI tags (Minimal API style webAPIs)|
| `SwaggerXmlComments` |Adds the XML docuemantion files for all assys loaded in the app domain with a assy name prefix |

#URLs

Here are some of the useful URLs available when running the projects:

| csproj name | Base Endpoint                    |  URLs | 
| ------------- | ------------------------------ | 
| `Worldpay.US.ReverseProxy` | https://localhost:7296 <br> http://localhost:5268     |  RAFT <br> - https://localhost:7296/raft/swagger/index.html <br>  - https://localhost:7296/raft/api/v2/Payments/authorize <br> Express <br> - https://localhost:7296/express/swagger/index.html  <br>- https://localhost:7296/express/api/v2/payments/authorize <br> - https://localhost:7296/raft/api/v2/debug/headers <br> - https://localhost:7296/express/api/v2/debug/headers |
| `Worldpay.US.RAFT`      | https://localhost:7047 <br> http://localhost:5285     |  - https://localhost:7047/swagger<br>  - https://localhost:7047/health<br> - https://localhost:7047/api/v1/Weather/forecast?numberOfDays=5 <br>- https://localhost:7047/api/v2/Payments/authorize  |
| `Worldpay.US.Express`   | https://localhost:7174 <br> http://localhost:5233     |  - https://localhost:7174/swagger<br>  - https://localhost:7174/health<br>- https://localhost:7174/api/v1/Weather/forecast?numberOfDays=5 <br>- https://localhost:7174/api/v2/Payments/authorize   |


#Running the three (3) projects

*Hint: Consider using **Windows Terminal**  to organize the command line sessions*

![Windows Terminal Screenshot](https://github.com/TomBruns/YARP_ReverseProxy/blob/master/images/windows%20terminal.png?raw=true)

You can run each project after building using the **exe** in the output folder

###Worldpay.US.Express
```
C:\Users\e5593810\source\repos\FISGitHub\YARP_ReverseProxy\Worldpay.US.RAFT\bin\release\net8.0>Worldpay.US.RAFT.exe
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://127.0.0.1:7047
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://127.0.0.1:5285
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\e5593810\source\repos\FISGitHub\YARP_ReverseProxy\Worldpay.US.RAFT\bin\release\net8.0
```

###Worldpay.US.RAFT
```
C:\Users\e5593810\source\repos\FISGitHub\YARP_ReverseProxy\Worldpay.US.Express\bin\Release\net8.0>Worldpay.US.Express.exe
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: https://127.0.0.1:7174
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://127.0.0.1:5233
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: C:\Users\e5593810\source\repos\FISGitHub\YARP_ReverseProxy\Worldpay.US.Express\bin\Release\net8.0
```

###Worldpay.US.ReverseProxy
```
C:\Users\e5593810\source\repos\FISGitHub\YARP_ReverseProxy\Worldpay.US.ReverseProxy\bin\Release\net8.0>Worldpay.US.ReverseProxy.exe
```
*Note: The logging was cut down for the performance tests*

#Running performance tests

1. Configure all appsettings.josn file to minimize console logging
2. Start release builds of the three (3) projects
3. Change your working directory to the ```k6_load_tests``` directory in the project
4. Edit the k6 script file ```loadtest_webapi.js``` and uncomment the endpoint you want to call

	```javascript
	export default function() {
		//let res = http.get("https://localhost:7047/api/v2/Weather/forecast");			// RAFT (Controller Style) Direct
		//let res = http.get("https://localhost:7174/api/v2/Weather/forecast");			// Express (Minimal API) Direct
		//let res = http.get("https://localhost:7296/raft/api/v2/Weather/forecast");	// RAFT (Controller Style) via YARP
		let res = http.get("https://localhost:7296/express/api/v2/Weather/forecast");	// Express (Minimal API) via YARP
		check(res, {
			"status was 200": (r) => r.status == 200
		});
```

5. Execute the performance test from the commannd line

```shell
C:\Users\e5593810\source\repos\FISGitHub\YARP_ReverseProxy\k6_load_tests>k6 run loadtest_webapi.js

/\      |‾‾| /‾‾/   /‾‾/
/\  /  \     |  |/  /   /  /
/  \/    \    |     (   /   ‾‾\
/          \   |  |\  \ |  (‾)  |
/ __________ \  |__| \__\ \_____/ .io

execution: local
script: loadtest_webapi.js
output: -

scenarios: (100.00%) 1 scenario, 50 max VUs, 1m45s max duration (incl. graceful stop):
* default: Up to 50 looping VUs for 1m15s over 3 stages (gracefulRampDown: 30s, gracefulStop: 30s)


running (1m15.0s), 00/50 VUs, 1752010 complete and 0 interrupted iterations
default ✓ [======================================] 00/50 VUs  1m15s

✓ status was 200

checks.........................: 100.00% ✓ 1752010      ✗ 0
data_received..................: 759 MB  10 MB/s
data_sent......................: 63 MB   841 kB/s
http_req_blocked...............: avg=330ns   min=0s med=0s     max=70.81ms  p(90)=0s      p(95)=0s
http_req_connecting............: avg=23ns    min=0s med=0s     max=2.88ms   p(90)=0s      p(95)=0s
http_req_duration..............: avg=1.36ms  min=0s med=1.29ms max=241.82ms p(90)=2.01ms  p(95)=2.29ms
{ expected_response:true }...: avg=1.36ms  min=0s med=1.29ms max=241.82ms p(90)=2.01ms  p(95)=2.29ms
http_req_failed................: 0.00%   ✓ 0            ✗ 1752010
http_req_receiving.............: avg=65.81µs min=0s med=0s     max=18.21ms  p(90)=354.6µs p(95)=506.7µs
http_req_sending...............: avg=37.78µs min=0s med=0s     max=30.43ms  p(90)=0s      p(95)=505.49µs
http_req_tls_handshaking.......: avg=148ns   min=0s med=0s     max=60.8ms   p(90)=0s      p(95)=0s
http_req_waiting...............: avg=1.25ms  min=0s med=1.2ms  max=227.21ms p(90)=1.88ms  p(95)=2.12ms
http_reqs......................: 1752010 23359.687506/s
iteration_duration.............: avg=1.4ms   min=0s med=1.32ms max=313.85ms p(90)=2.06ms  p(95)=2.35ms
iterations.....................: 1752010 23359.687506/s
vus............................: 1       min=1          max=49
vus_max........................: 50      min=50         max=50
```