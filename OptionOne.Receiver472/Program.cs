using NServiceBus;

using OptionOne.Messages;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionOne.Receiver472
{
    class Program
    {
        private const string EndPointName = "Opt1.Hsn.Eco.PublishProduct";

        static void Main()
        {
            var endpointConfiguration = new EndpointConfiguration(EndPointName);

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningMessagesAs(type => type.Namespace == "OptionOne.Messages");

            RoutingSettings<LearningTransport> routing = transport.Routing();

            var recoverability = endpointConfiguration.Recoverability();
            recoverability.DisableLegacyRetriesSatellite();
            recoverability.Immediate(immediate => { immediate.NumberOfRetries(0); });
            recoverability.Delayed(delayed =>
            {
                var numberOfRetries = delayed.NumberOfRetries(0);
                numberOfRetries.TimeIncrease(TimeSpan.FromSeconds(30));
            });

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}
