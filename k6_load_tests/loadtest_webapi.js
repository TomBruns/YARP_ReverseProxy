// ======================
//	k6 load testing script
//		https://k6.io/
//
//	to run: k6 run loadtest_webapi.js
// ======================

// Imports
import http from "k6/http";
import { check, sleep } from "k6";
//import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";
//import { textSummary } from 'https://jslib.k6.io/k6-summary/0.0.1/index.js'

// Init Code
export const options = {
	stages: [
		{ duration: "10s", target: 25 },
		{ duration: "1m00s", target: 50 },
		{ duration: "5s", target: 0 },
	]
};

//export function handleSummary(data) {
//  return {
//    "summary.html": htmlReport(data, { title: "Controller Thru Reverse Proxy" }),
//    stdout: textSummary(data, { indent: ' ', enableColors: true }),
//  };
//}

// VU Code
export default function() {
	//let res = http.get("https://localhost:7047/api/v2/Weather/forecast");			// RAFT (Controller Style) Direct
	//let res = http.get("https://localhost:7174/api/v2/Weather/forecast");			// Express (Minimal API) Direct
	//let res = http.get("https://localhost:7296/raft/api/v2/Weather/forecast");	// RAFT (Controller Style) via YARP
	let res = http.get("https://localhost:7296/express/api/v2/Weather/forecast");	// Express (Minimal API) via YARP
	check(res, {
		"status was 200": (r) => r.status == 200
	});
}