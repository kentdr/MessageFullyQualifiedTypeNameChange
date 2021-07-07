using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using System;
using OptionOne.Messages;

namespace OptionOne.SenderNetCore
{
    class Program
    {
        private const string EndPointName = "Opt1.Hsn.Eco.PublishProduct";

        static async Task Main()
        {
            var endpointConfiguration = new EndpointConfiguration(EndPointName);
            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningMessagesAs(type => type.Namespace == "OptionOne.Messages");

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
