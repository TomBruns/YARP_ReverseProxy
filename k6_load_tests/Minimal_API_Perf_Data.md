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


running (1m15.0s), 0/1 VUs, 265804 complete and 0 interrupted iterations
default ✓ [======================================] 0/1 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 265804     ✗ 0
     data_received..................: 115 MB  1.5 MB/s
     data_sent......................: 9.6 MB  128 kB/s
     http_req_blocked...............: avg=396ns    min=0s med=0s max=72.43ms p(90)=0s      p(95)=0s
     http_req_connecting............: avg=0s       min=0s med=0s max=0s      p(90)=0s      p(95)=0s
     http_req_duration..............: avg=232.76µs min=0s med=0s max=11.96ms p(90)=588.6µs p(95)=650.29µs
       { expected_response:true }...: avg=232.76µs min=0s med=0s max=11.96ms p(90)=588.6µs p(95)=650.29µs
     http_req_failed................: 0.00%   ✓ 0          ✗ 265804
     http_req_receiving.............: avg=34.14µs  min=0s med=0s max=6.27ms  p(90)=109.4µs p(95)=201.5µs
     http_req_sending...............: avg=25.78µs  min=0s med=0s max=2.41ms  p(90)=0s      p(95)=199.88µs
     http_req_tls_handshaking.......: avg=234ns    min=0s med=0s max=62.37ms p(90)=0s      p(95)=0s
     http_req_waiting...............: avg=172.83µs min=0s med=0s max=11.46ms p(90)=517.4µs p(95)=534.9µs
     http_reqs......................: 265804  3544.02905/s
     iteration_duration.............: avg=279.8µs  min=0s med=0s max=87.76ms p(90)=658.9µs p(95)=701.5µs
     iterations.....................: 265804  3544.02905/s
     vus............................: 1       min=1        max=1
     vus_max........................: 1       min=1        max=1

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


running (1m15.0s), 00/10 VUs, 1349446 complete and 0 interrupted iterations
default ✓ [======================================] 00/10 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 1349446     ✗ 0
     data_received..................: 588 MB  7.8 MB/s
     data_sent......................: 49 MB   648 kB/s
     http_req_blocked...............: avg=230ns    min=0s med=0s      max=45.23ms p(90)=0s       p(95)=0s
     http_req_connecting............: avg=3ns      min=0s med=0s      max=669.4µs p(90)=0s       p(95)=0s
     http_req_duration..............: avg=311.06µs min=0s med=504.6µs max=13.26ms p(90)=640µs    p(95)=687.4µs
       { expected_response:true }...: avg=311.06µs min=0s med=504.6µs max=13.26ms p(90)=640µs    p(95)=687.4µs
     http_req_failed................: 0.00%   ✓ 0           ✗ 1349446
     http_req_receiving.............: avg=25.09µs  min=0s med=0s      max=12.04ms p(90)=0s       p(95)=160.2µs
     http_req_sending...............: avg=44.25µs  min=0s med=0s      max=10.38ms p(90)=0s       p(95)=506.8µs
     http_req_tls_handshaking.......: avg=56ns     min=0s med=0s      max=45.23ms p(90)=0s       p(95)=0s
     http_req_waiting...............: avg=241.71µs min=0s med=0s      max=12.97ms p(90)=600.79µs p(95)=651µs
     http_reqs......................: 1349446 17991.99628/s
     iteration_duration.............: avg=347.3µs  min=0s med=506.4µs max=47.75ms p(90)=652.69µs p(95)=699.2µs
     iterations.....................: 1349446 17991.99628/s
     vus............................: 1       min=1         max=9
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


running (1m15.0s), 00/20 VUs, 2232744 complete and 0 interrupted iterations
default ✓ [======================================] 00/20 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 2232744      ✗ 0
     data_received..................: 969 MB  13 MB/s
     data_sent......................: 80 MB   1.1 MB/s
     http_req_blocked...............: avg=243ns    min=0s med=0s      max=40.63ms p(90)=0s       p(95)=0s
     http_req_connecting............: avg=4ns      min=0s med=0s      max=936.2µs p(90)=0s       p(95)=0s
     http_req_duration..............: avg=380.44µs min=0s med=505.6µs max=20.01ms p(90)=658.59µs p(95)=731.6µs
       { expected_response:true }...: avg=380.44µs min=0s med=505.6µs max=20.01ms p(90)=658.59µs p(95)=731.6µs
     http_req_failed................: 0.00%   ✓ 0            ✗ 2232744
     http_req_receiving.............: avg=29.96µs  min=0s med=0s      max=12.41ms p(90)=0s       p(95)=221.6µs
     http_req_sending...............: avg=47.48µs  min=0s med=0s      max=13.56ms p(90)=0s       p(95)=506.1µs
     http_req_tls_handshaking.......: avg=49ns     min=0s med=0s      max=40.54ms p(90)=0s       p(95)=0s
     http_req_waiting...............: avg=302.98µs min=0s med=223.6µs max=18.8ms  p(90)=612.9µs  p(95)=673.1µs
     http_reqs......................: 2232744 29767.955791/s
     iteration_duration.............: avg=431.5µs  min=0s med=506.1µs max=43.05ms p(90)=681.9µs  p(95)=764.1µs
     iterations.....................: 2232744 29767.955791/s
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


running (1m15.0s), 00/50 VUs, 2788047 complete and 0 interrupted iterations
default ✓ [======================================] 00/50 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 2788047      ✗ 0
     data_received..................: 1.2 GB  16 MB/s
     data_sent......................: 100 MB  1.3 MB/s
     http_req_blocked...............: avg=249ns    min=0s med=0s      max=39.42ms p(90)=0s     p(95)=0s
     http_req_connecting............: avg=12ns     min=0s med=0s      max=1.88ms  p(90)=0s     p(95)=0s
     http_req_duration..............: avg=847.07µs min=0s med=768.9µs max=42.43ms p(90)=1.28ms p(95)=1.54ms
       { expected_response:true }...: avg=847.07µs min=0s med=768.9µs max=42.43ms p(90)=1.28ms p(95)=1.54ms
     http_req_failed................: 0.00%   ✓ 0            ✗ 2788047
     http_req_receiving.............: avg=44.77µs  min=0s med=0s      max=36.52ms p(90)=0s     p(95)=505.6µs
     http_req_sending...............: avg=36.5µs   min=0s med=0s      max=41.78ms p(90)=0s     p(95)=505.29µs
     http_req_tls_handshaking.......: avg=84ns     min=0s med=0s      max=39.33ms p(90)=0s     p(95)=0s
     http_req_waiting...............: avg=765.79µs min=0s med=699.2µs max=41.21ms p(90)=1.2ms  p(95)=1.41ms
     http_reqs......................: 2788047 37171.264786/s
     iteration_duration.............: avg=883.63µs min=0s med=799.1µs max=48.66ms p(90)=1.33ms p(95)=1.6ms
     iterations.....................: 2788047 37171.264786/s
     vus............................: 1       min=1          max=49
     vus_max........................: 50      min=50         max=50
