using Yarp.ReverseProxy.Forwarder;
using Yarp.Telemetry.Consumption;

namespace Worldpay.US.ReverseProxy
{
    public class ForwarderTelemetry : IForwarderTelemetryConsumer
    {
        /// Called before forwarding a request.
        public void OnForwarderStart(DateTime timestamp, string destinationPrefix)
        {
            Console.WriteLine($"Forwarder Telemetry [{timestamp:HH:mm:ss.fff}] => OnForwarderStart :: Destination prefix: {destinationPrefix}");
        }

        /// Called after forwarding a request.
        public void OnForwarderStop(DateTime timestamp, int statusCode)
        {
            Console.WriteLine($"Forwarder Telemetry [{timestamp:HH:mm:ss.fff}] => OnForwarderStop :: Status: {statusCode}");
        }

        /// Called before <see cref="OnForwarderStop(DateTime, int)"/> if forwarding the request failed.
        public void OnForwarderFailed(DateTime timestamp, ForwarderError error)
        {
            Console.WriteLine($"Forwarder Telemetry [{timestamp:HH:mm:ss.fff}] => OnForwarderFailed :: Error: {error.ToString()}");
        }

        /// Called when reaching a given stage of forwarding a request.
        public void OnForwarderStage(DateTime timestamp, ForwarderStage stage)
        {
            Console.WriteLine($"Forwarder Telemetry [{timestamp:HH:mm:ss.fff}] => OnForwarderStage :: Stage: {stage.ToString()}");
        }

        /// Called periodically while a content transfer is active.
        public void OnContentTransferring(DateTime timestamp, bool isRequest, long contentLength, long iops, TimeSpan readTime, TimeSpan writeTime)
        {
            Console.WriteLine($"Forwarder Telemetry [{timestamp:HH:mm:ss.fff}] => OnContentTransferring :: Is request: {isRequest}, Content length: {contentLength}, IOps: {iops}, Read time: {readTime:s\\.fff}, Write time: {writeTime:s\\.fff}");
        }

        /// Called after transferring the request or response content.
        public void OnContentTransferred(DateTime timestamp, bool isRequest, long contentLength, long iops, TimeSpan readTime, TimeSpan writeTime, TimeSpan firstReadTime)
        {
            Console.WriteLine($"Forwarder Telemetry [{timestamp:HH:mm:ss.fff}] => OnContentTransferred :: Is request: {isRequest}, Content length: {contentLength}, IOps: {iops}, Read time: {readTime:s\\.fff}, Write time: {writeTime:s\\.fff}");
        }

        /// Called before forwarding a request from `ForwarderMiddleware`, therefore is not called for direct forwarding scenarios.
        public void OnForwarderInvoke(DateTime timestamp, string clusterId, string routeId, string destinationId)
        {
            Console.WriteLine($"Forwarder Telemetry [{timestamp:HH:mm:ss.fff}] => OnForwarderInvoke:: Cluster id: {clusterId}, Route Id: {routeId}, Destination: {destinationId}");
        }
    }
}
