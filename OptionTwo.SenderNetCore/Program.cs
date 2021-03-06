using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using System;
using OptionTwo.Messages;

namespace OptionTwo.SenderNetCore
{
    class Program
    {
        private const string EndPointName = "Opt2.Hsn.Eco.PublishProduct";

        static async Task Main()
        {
            var endpointConfiguration = new EndpointConfiguration(EndPointName);
            var transport = endpointConfiguration.UseTransport<LearningTransport>();

            var routing = transport.Routing();
            routing.RouteToEndpoint(typeof(TestMessage), EndPointName);
            endpointConfiguration.SendOnly();

            var endpoint = await Endpoint.Start(endpointConfiguration);

            var key = Console.ReadKey();
            ILog logger = LogManager.GetLogger("SenderNetCore");
            logger.Info("Press Enter to send message. Press Escape to exit");

            while (key.Key != ConsoleKey.Escape)
            {
                if (key.Key == ConsoleKey.Enter)
                {
                    var testMessage = new TestMessage(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

                    logger.Info("Sending Message...");

                    await endpoint.Send(testMessage);
                }

                key = Console.ReadKey();
            }

            await endpoint.Stop();
        }
    }
}
