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


running (1m15.0s), 0/1 VUs, 175991 complete and 0 interrupted iterations
default ✓ [======================================] 0/1 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 175991      ✗ 0
     data_received..................: 78 MB   1.0 MB/s
     data_sent......................: 6.3 MB  85 kB/s
     http_req_blocked...............: avg=420ns    min=0s med=0s      max=47.78ms  p(90)=0s      p(95)=0s
     http_req_connecting............: avg=60ns     min=0s med=0s      max=10.6ms   p(90)=0s      p(95)=0s
     http_req_duration..............: avg=338µs    min=0s med=503.6µs max=233.26ms p(90)=782.1µs p(95)=826.4µs
       { expected_response:true }...: avg=338µs    min=0s med=503.6µs max=233.26ms p(90)=782.1µs p(95)=826.4µs
     http_req_failed................: 0.00%   ✓ 0           ✗ 175991
     http_req_receiving.............: avg=63.54µs  min=0s med=0s      max=9.13ms   p(90)=280.7µs p(95)=331.9µs
     http_req_sending...............: avg=48.86µs  min=0s med=0s      max=2.33ms   p(90)=61.3µs  p(95)=505.4µs
     http_req_tls_handshaking.......: avg=211ns    min=0s med=0s      max=37.18ms  p(90)=0s      p(95)=0s
     http_req_waiting...............: avg=225.59µs min=0s med=0s      max=231.18ms p(90)=517.4µs p(95)=525.59µs
     http_reqs......................: 175991  2346.523399/s
     iteration_duration.............: avg=422.68µs min=0s med=506.7µs max=281.05ms p(90)=842.6µs p(95)=877.3µs
     iterations.....................: 175991  2346.523399/s
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


running (1m15.0s), 00/10 VUs, 807827 complete and 0 interrupted iterations
default ✓ [======================================] 00/10 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 807827       ✗ 0
     data_received..................: 357 MB  4.8 MB/s
     data_sent......................: 29 MB   388 kB/s
     http_req_blocked...............: avg=258ns    min=0s med=0s       max=46.25ms p(90)=0s      p(95)=0s
     http_req_connecting............: avg=6ns      min=0s med=0s       max=1.04ms  p(90)=0s      p(95)=0s
     http_req_duration..............: avg=534.18µs min=0s med=549.29µs max=27.34ms p(90)=702.8µs p(95)=783.86µs
       { expected_response:true }...: avg=534.18µs min=0s med=549.29µs max=27.34ms p(90)=702.8µs p(95)=783.86µs
     http_req_failed................: 0.00%   ✓ 0            ✗ 807827
     http_req_receiving.............: avg=52.06µs  min=0s med=0s       max=13.71ms p(90)=160.2µs p(95)=503.8µs
     http_req_sending...............: avg=28.57µs  min=0s med=0s       max=8.62ms  p(90)=0s      p(95)=504.6µs
     http_req_tls_handshaking.......: avg=98ns     min=0s med=0s       max=46.25ms p(90)=0s      p(95)=0s
     http_req_waiting...............: avg=453.55µs min=0s med=515.1µs  max=27.34ms p(90)=657µs   p(95)=710.9µs
     http_reqs......................: 807827  10770.652178/s
     iteration_duration.............: avg=581.66µs min=0s med=581.4µs  max=49.21ms p(90)=737.9µs p(95)=842.5µs
     iterations.....................: 807827  10770.652178/s
     vus............................: 1       min=1          max=10
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


running (1m15.0s), 00/20 VUs, 1384558 complete and 0 interrupted iterations
default ✓ [======================================] 00/20 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 1384558     ✗ 0
     data_received..................: 609 MB  8.1 MB/s
     data_sent......................: 50 MB   665 kB/s
     http_req_blocked...............: avg=244ns    min=0s med=0s       max=45.32ms p(90)=0s      p(95)=0s
     http_req_connecting............: avg=9ns      min=0s med=0s       max=1.31ms  p(90)=0s      p(95)=0s
     http_req_duration..............: avg=651.54µs min=0s med=624.7µs  max=40.51ms p(90)=914.8µs p(95)=1.07ms
       { expected_response:true }...: avg=651.54µs min=0s med=624.7µs  max=40.51ms p(90)=914.8µs p(95)=1.07ms
     http_req_failed................: 0.00%   ✓ 0           ✗ 1384558
     http_req_receiving.............: avg=36.87µs  min=0s med=0s       max=14.84ms p(90)=108µs   p(95)=315.2µs
     http_req_sending...............: avg=30.57µs  min=0s med=0s       max=9.57ms  p(90)=0s      p(95)=504.6µs
     http_req_tls_handshaking.......: avg=80ns     min=0s med=0s       max=42.32ms p(90)=0s      p(95)=0s
     http_req_waiting...............: avg=584.09µs min=0s med=570.79µs max=40ms    p(90)=842.9µs p(95)=969.8µs
     http_reqs......................: 1384558 18460.03572/s
     iteration_duration.............: avg=699.39µs min=0s med=657.2µs  max=48.12ms p(90)=986.7µs p(95)=1.13ms
     iterations.....................: 1384558 18460.03572/s
     vus............................: 1       min=1         max=19
     vus_max........................: 20      min=20        max=20


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


running (1m15.0s), 00/50 VUs, 1784026 complete and 0 interrupted iterations
default ✓ [======================================] 00/50 VUs  1m15s

     ✓ status was 200

     checks.........................: 100.00% ✓ 1784026      ✗ 0
     data_received..................: 773 MB  10 MB/s
     data_sent......................: 64 MB   857 kB/s
     http_req_blocked...............: avg=343ns   min=0s med=0s     max=51.16ms p(90)=0s      p(95)=0s
     http_req_connecting............: avg=22ns    min=0s med=0s     max=2.06ms  p(90)=0s      p(95)=0s
     http_req_duration..............: avg=1.33ms  min=0s med=1.28ms max=37.18ms p(90)=1.99ms  p(95)=2.28ms
       { expected_response:true }...: avg=1.33ms  min=0s med=1.28ms max=37.18ms p(90)=1.99ms  p(95)=2.28ms
     http_req_failed................: 0.00%   ✓ 0            ✗ 1784026
     http_req_receiving.............: avg=62.35µs min=0s med=0s     max=34.47ms p(90)=310.9µs p(95)=506.7µs
     http_req_sending...............: avg=41.74µs min=0s med=0s     max=27.5ms  p(90)=0s      p(95)=505.9µs
     http_req_tls_handshaking.......: avg=142ns   min=0s med=0s     max=39.59ms p(90)=0s      p(95)=0s
     http_req_waiting...............: avg=1.22ms  min=0s med=1.19ms max=33.11ms p(90)=1.85ms  p(95)=2.12ms
     http_reqs......................: 1784026 23786.203714/s
     iteration_duration.............: avg=1.38ms  min=0s med=1.32ms max=53.87ms p(90)=2.04ms  p(95)=2.34ms
     iterations.....................: 1784026 23786.203714/s
     vus............................: 1       min=1          max=49
     vus_max........................: 50      min=50         max=50