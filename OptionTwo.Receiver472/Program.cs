using NServiceBus;
using NServiceBus.MessageMutator;
using System;

namespace OptionTwo.Receiver472
{
    class Program
    {
        private const string EndPointName = "Opt2.Hsn.Eco.PublishProduct";

        static void Main()
        {
            var endpointConfiguration = new EndpointConfiguration(EndPointName);

            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            
            var recoverability = endpointConfiguration.Recoverability();
            recoverability.DisableLegacyRetriesSatellite();
            recoverability.Immediate(immediate => { immediate.NumberOfRetries(0); });
            recoverability.Delayed(delayed =>
            {
                var numberOfRetries = delayed.NumberOfRetries(0);
                numberOfRetries.TimeIncrease(TimeSpan.FromSeconds(30));
            });

            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.RegisterMessageMutator(new ConvertFullyQualifiedClassName());
            endpointConfiguration.EnableInstallers();

            var endpoint = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

            Console.WriteLine("Press any key to quit");
            Console.ReadKey();
        }
    }
}
