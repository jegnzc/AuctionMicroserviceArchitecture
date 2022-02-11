using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;

namespace PaymentService
{
    public class Logger
    {

        private static Logger _instance;
        TelemetryConfiguration configuration = TelemetryConfiguration.CreateDefault();

        private Logger()
        {
            configuration.InstrumentationKey = "enter App Insights instrumentation key";
        }

        public static Logger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                }
                return _instance;

            }
        }


        public void LogMessage(string message)
        {
            var telemetryClient = new TelemetryClient(configuration);
            telemetryClient.TrackTrace(message);
            Console.WriteLine(message);

        }
    }
}
