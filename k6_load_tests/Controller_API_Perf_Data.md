1/1/0

          /\      |‾‾| /‾‾/   /‾‾/
     /\  /  \     |  |/  /   /  /
    /  \/    \    |     (   /   ‾‾\
   /          \   |  |\  \ |  (‾)  |
  / __________ \  |__| \__\ \_____/ .io

  execution: local
     script: loadtest_webapi.js
     output: -

  scenarios: (100.00%) 1 scenario, 1 max VUs, 1m45s max duration (incl. graceful stop):
           * default: Up to 1 looping VUs for 1m15s over 3 stages (gracefulRampDown: 30s, gracefulStop: 30s)


running (1m15.0s), 0/1 VUs, 243083 complete and 0 interrupted iterations
default ✓ [======================================] 0/1 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 243083      ✗ 0
     data_received..................: 105 MB  1.4 MB/s
     data_sent......................: 8.8 MB  117 kB/s
     http_req_blocked...............: avg=278ns    min=0s med=0s      max=46.9ms  p(90)=0s      p(95)=0s
     http_req_connecting............: avg=0s       min=0s med=0s      max=0s      p(90)=0s      p(95)=0s
     http_req_duration..............: avg=261.71µs min=0s med=0s      max=8.53ms  p(90)=616.1µs p(95)=672.2µs
       { expected_response:true }...: avg=261.71µs min=0s med=0s      max=8.53ms  p(90)=616.1µs p(95)=672.2µs
     http_req_failed................: 0.00%   ✓ 0           ✗ 243083
     http_req_receiving.............: avg=34.52µs  min=0s med=0s      max=5.92ms  p(90)=127.2µs p(95)=200.8µs
     http_req_sending...............: avg=18.81µs  min=0s med=0s      max=2.01ms  p(90)=0s      p(95)=0s
     http_req_tls_handshaking.......: avg=178ns    min=0s med=0s      max=43.28ms p(90)=0s      p(95)=0s
     http_req_waiting...............: avg=208.37µs min=0s med=0s      max=7.26ms  p(90)=514.1µs p(95)=525.9µs
     http_reqs......................: 243083  3241.057489/s
     iteration_duration.............: avg=306.54µs min=0s med=255.6µs max=48.13ms p(90)=676.1µs p(95)=718.4µs
     iterations.....................: 243083  3241.057489/s
     vus............................: 1       min=1         max=1
     vus_max........................: 1       min=1         max=1

5/10/0


          /\      |‾‾| /‾‾/   /‾‾/
     /\  /  \     |  |/  /   /  /
    /  \/    \    |     (   /   ‾‾\
   /          \   |  |\  \ |  (‾)  |
  / __________ \  |__| \__\ \_____/ .io

  execution: local
     script: loadtest_webapi.js
     output: -

  scenarios: (100.00%) 1 scenario, 10 max VUs, 1m45s max duration (incl. graceful stop):
           * default: Up to 10 looping VUs for 1m15s over 3 stages (gracefulRampDown: 30s, gracefulStop: 30s)


running (1m15.0s), 00/10 VUs, 1221527 complete and 0 interrupted iterations
default ✓ [======================================] 00/10 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 1221527      ✗ 0
     data_received..................: 546 MB  7.3 MB/s
     data_sent......................: 44 MB   586 kB/s
     http_req_blocked...............: avg=306ns    min=0s med=0s      max=44.5ms  p(90)=0s       p(95)=0s
     http_req_connecting............: avg=4ns      min=0s med=0s      max=1.08ms  p(90)=0s       p(95)=0s
     http_req_duration..............: avg=337.13µs min=0s med=505.6µs max=14.64ms p(90)=634.1µs  p(95)=695.7µs
       { expected_response:true }...: avg=337.13µs min=0s med=505.6µs max=14.64ms p(90)=634.1µs  p(95)=695.7µs
     http_req_failed................: 0.00%   ✓ 0            ✗ 1221527
     http_req_receiving.............: avg=29.01µs  min=0s med=0s      max=14.64ms p(90)=0s       p(95)=200.7µs
     http_req_sending...............: avg=54.11µs  min=0s med=0s      max=10.51ms p(90)=503.6µs  p(95)=517.9µs
     http_req_tls_handshaking.......: avg=55ns     min=0s med=0s      max=38.65ms p(90)=0s       p(95)=0s
     http_req_waiting...............: avg=254µs    min=0s med=0s      max=14.64ms p(90)=584.09µs p(95)=652.4µs
     http_reqs......................: 1221527 16286.846534/s
     iteration_duration.............: avg=383.05µs min=0s med=515.5µs max=45.87ms p(90)=652.29µs p(95)=712.6µs
     iterations.....................: 1221527 16286.846534/s
     vus............................: 1       min=1          max=9
     vus_max........................: 10      min=10         max=10

10/20/0

          /\      |‾‾| /‾‾/   /‾‾/
     /\  /  \     |  |/  /   /  /
    /  \/    \    |     (   /   ‾‾\
   /          \   |  |\  \ |  (‾)  |
  / __________ \  |__| \__\ \_____/ .io

  execution: local
     script: loadtest_webapi.js
     output: -

  scenarios: (100.00%) 1 scenario, 20 max VUs, 1m45s max duration (incl. graceful stop):
           * default: Up to 20 looping VUs for 1m15s over 3 stages (gracefulRampDown: 30s, gracefulStop: 30s)


running (1m15.0s), 00/20 VUs, 2026378 complete and 0 interrupted iterations
default ✓ [======================================] 00/20 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 2026378      ✗ 0
     data_received..................: 897 MB  12 MB/s
     data_sent......................: 73 MB   973 kB/s
     http_req_blocked...............: avg=273ns    min=0s med=0s      max=51.17ms p(90)=0s       p(95)=0s
     http_req_connecting............: avg=5ns      min=0s med=0s      max=1.35ms  p(90)=0s       p(95)=0s
     http_req_duration..............: avg=423.98µs min=0s med=506.1µs max=13.79ms p(90)=696.5µs  p(95)=781.3µs
       { expected_response:true }...: avg=423.98µs min=0s med=506.1µs max=13.79ms p(90)=696.5µs  p(95)=781.3µs
     http_req_failed................: 0.00%   ✓ 0            ✗ 2026378
     http_req_receiving.............: avg=33.44µs  min=0s med=0s      max=13.28ms p(90)=0s       p(95)=414µs
     http_req_sending...............: avg=45.29µs  min=0s med=0s      max=10.52ms p(90)=0s       p(95)=506µs
     http_req_tls_handshaking.......: avg=57ns     min=0s med=0s      max=51.17ms p(90)=0s       p(95)=0s
     http_req_waiting...............: avg=345.25µs min=0s med=474.9µs max=12.79ms p(90)=646.29µs p(95)=720µs
     http_reqs......................: 2026378 27018.162916/s
     iteration_duration.............: avg=475.69µs min=0s med=508.9µs max=52.71ms p(90)=722.4µs  p(95)=816.6µs
     iterations.....................: 2026378 27018.162916/s
     vus............................: 1       min=1          max=20
     vus_max........................: 20      min=20         max=20

25/50/0

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


running (1m15.0s), 00/50 VUs, 2583174 complete and 0 interrupted iterations
default ✓ [======================================] 00/50 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 2583174      ✗ 0
     data_received..................: 1.1 GB  15 MB/s
     data_sent......................: 93 MB   1.2 MB/s
     http_req_blocked...............: avg=261ns    min=0s med=0s      max=66.09ms p(90)=0s     p(95)=0s
     http_req_connecting............: avg=17ns     min=0s med=0s      max=8.58ms  p(90)=0s     p(95)=0s
     http_req_duration..............: avg=917.73µs min=0s med=856.2µs max=58.13ms p(90)=1.4ms  p(95)=1.66ms
       { expected_response:true }...: avg=917.73µs min=0s med=856.2µs max=58.13ms p(90)=1.4ms  p(95)=1.66ms
     http_req_failed................: 0.00%   ✓ 0            ✗ 2583174
     http_req_receiving.............: avg=45.14µs  min=0s med=0s      max=51.69ms p(90)=0s     p(95)=505.49µs
     http_req_sending...............: avg=37.39µs  min=0s med=0s      max=52.48ms p(90)=0s     p(95)=505.29µs
     http_req_tls_handshaking.......: avg=105ns    min=0s med=0s      max=57.51ms p(90)=0s     p(95)=0s
     http_req_waiting...............: avg=835.19µs min=0s med=776.9µs max=58.13ms p(90)=1.3ms  p(95)=1.52ms
     http_reqs......................: 2583174 34442.009149/s
     iteration_duration.............: avg=954µs    min=0s med=888.7µs max=80.01ms p(90)=1.45ms p(95)=1.73ms
     iterations.....................: 2583174 34442.009149/s
     vus............................: 0       min=0          max=49
     vus_max........................: 50      min=50         max=50


