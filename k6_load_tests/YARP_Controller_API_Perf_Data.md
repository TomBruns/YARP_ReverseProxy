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


running (1m15.0s), 0/1 VUs, 166878 complete and 0 interrupted iterations
default ✓ [======================================] 0/1 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 166878      ✗ 0
     data_received..................: 74 MB   983 kB/s
     data_sent......................: 6.0 MB  80 kB/s
     http_req_blocked...............: avg=519ns    min=0s med=0s       max=41.4ms  p(90)=0s      p(95)=0s
     http_req_connecting............: avg=0s       min=0s med=0s       max=0s      p(90)=0s      p(95)=0s
     http_req_duration..............: avg=331.7µs  min=0s med=503.7µs  max=6.6ms   p(90)=772.2µs p(95)=832.9µs
       { expected_response:true }...: avg=331.7µs  min=0s med=503.7µs  max=6.6ms   p(90)=772.2µs p(95)=832.9µs
     http_req_failed................: 0.00%   ✓ 0           ✗ 166878
     http_req_receiving.............: avg=56.77µs  min=0s med=0s       max=5.38ms  p(90)=283.2µs p(95)=350.1µs
     http_req_sending...............: avg=62.69µs  min=0s med=0s       max=2.34ms  p(90)=503.7µs p(95)=514.5µs
     http_req_tls_handshaking.......: avg=226ns    min=0s med=0s       max=37.8ms  p(90)=0s      p(95)=0s
     http_req_waiting...............: avg=212.23µs min=0s med=0s       max=6.02ms  p(90)=520.9µs p(95)=535.5µs
     http_reqs......................: 166878  2224.723351/s
     iteration_duration.............: avg=444.6µs  min=0s med=514.29µs max=43.98ms p(90)=875.1µs p(95)=909.4µs
     iterations.....................: 166878  2224.723351/s
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


running (1m15.0s), 00/10 VUs, 761427 complete and 0 interrupted iterations
default ✓ [======================================] 00/10 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 761427      ✗ 0
     data_received..................: 336 MB  4.5 MB/s
     data_sent......................: 27 MB   366 kB/s
     http_req_blocked...............: avg=186ns    min=0s med=0s       max=50.68ms p(90)=0s      p(95)=0s
     http_req_connecting............: avg=5ns      min=0s med=0s       max=646µs   p(90)=0s      p(95)=0s
     http_req_duration..............: avg=586.26µs min=0s med=600.19µs max=13.39ms p(90)=720.2µs p(95)=784.5µs
       { expected_response:true }...: avg=586.26µs min=0s med=600.19µs max=13.39ms p(90)=720.2µs p(95)=784.5µs
     http_req_failed................: 0.00%   ✓ 0           ✗ 761427
     http_req_receiving.............: avg=51.8µs   min=0s med=0s       max=8.49ms  p(90)=170.8µs p(95)=259.29µs
     http_req_sending...............: avg=16.15µs  min=0s med=0s       max=6.22ms  p(90)=0s      p(95)=0s
     http_req_tls_handshaking.......: avg=92ns     min=0s med=0s       max=38.09ms p(90)=0s      p(95)=0s
     http_req_waiting...............: avg=518.3µs  min=0s med=522.8µs  max=13.33ms p(90)=684µs   p(95)=728.6µs
     http_reqs......................: 761427  10152.25045/s
     iteration_duration.............: avg=618.66µs min=0s med=614.7µs  max=53.34ms p(90)=742.2µs p(95)=834.2µs
     iterations.....................: 761427  10152.25045/s
     vus............................: 1       min=1         max=10
     vus_max........................: 10      min=10        max=10

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


running (1m15.0s), 00/20 VUs, 1317128 complete and 0 interrupted iterations
default ✓ [======================================] 00/20 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 1317128      ✗ 0
     data_received..................: 579 MB  7.7 MB/s
     data_sent......................: 47 MB   632 kB/s
     http_req_blocked...............: avg=230ns    min=0s med=0s       max=42.76ms p(90)=0s      p(95)=0s
     http_req_connecting............: avg=9ns      min=0s med=0s       max=1.18ms  p(90)=0s      p(95)=0s
     http_req_duration..............: avg=686.79µs min=0s med=643.29µs max=16.93ms p(90)=997.6µs p(95)=1.13ms
       { expected_response:true }...: avg=686.79µs min=0s med=643.29µs max=16.93ms p(90)=997.6µs p(95)=1.13ms
     http_req_failed................: 0.00%   ✓ 0            ✗ 1317128
     http_req_receiving.............: avg=42.01µs  min=0s med=0s       max=8.81ms  p(90)=161.5µs p(95)=338.6µs
     http_req_sending...............: avg=25.68µs  min=0s med=0s       max=16.19ms p(90)=0s      p(95)=157.79µs
     http_req_tls_handshaking.......: avg=84ns     min=0s med=0s       max=39.91ms p(90)=0s      p(95)=0s
     http_req_waiting...............: avg=619.09µs min=0s med=577µs    max=14.33ms p(90)=909.8µs p(95)=1.05ms
     http_reqs......................: 1317128 17561.473005/s
     iteration_duration.............: avg=735.73µs min=0s med=687.8µs  max=55.59ms p(90)=1.05ms  p(95)=1.18ms
     iterations.....................: 1317128 17561.473005/s
     vus............................: 1       min=1          max=19
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


running (1m15.0s), 00/50 VUs, 1630902 complete and 0 interrupted iterations
default ✓ [======================================] 00/50 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 1630902      ✗ 0
     data_received..................: 707 MB  9.4 MB/s
     data_sent......................: 59 MB   783 kB/s
     http_req_blocked...............: avg=369ns   min=0s med=0s     max=70.08ms  p(90)=0s      p(95)=0s
     http_req_connecting............: avg=27ns    min=0s med=0s     max=2.83ms   p(90)=0s      p(95)=0s
     http_req_duration..............: avg=1.46ms  min=0s med=1.41ms max=64.17ms  p(90)=2.16ms  p(95)=2.45ms
       { expected_response:true }...: avg=1.46ms  min=0s med=1.41ms max=64.17ms  p(90)=2.16ms  p(95)=2.45ms
     http_req_failed................: 0.00%   ✓ 0            ✗ 1630902
     http_req_receiving.............: avg=70.37µs min=0s med=0s     max=27.98ms  p(90)=456.7µs p(95)=507.49µs
     http_req_sending...............: avg=39.76µs min=0s med=0s     max=37.09ms  p(90)=0s      p(95)=506µs
     http_req_tls_handshaking.......: avg=177ns   min=0s med=0s     max=58.15ms  p(90)=0s      p(95)=0s
     http_req_waiting...............: avg=1.35ms  min=0s med=1.31ms max=57.2ms   p(90)=2.02ms  p(95)=2.28ms
     http_reqs......................: 1630902 21745.276237/s
     iteration_duration.............: avg=1.51ms  min=0s med=1.45ms max=135.48ms p(90)=2.21ms  p(95)=2.52ms
     iterations.....................: 1630902 21745.276237/s
     vus............................: 1       min=1          max=50
     vus_max........................: 50      min=50         max=50