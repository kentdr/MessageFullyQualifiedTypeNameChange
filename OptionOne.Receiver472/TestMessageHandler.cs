using NServiceBus;
using NServiceBus.Logging;
using OptionOne.Messages;

using System.Threading.Tasks;

namespace OptionOne.Receiver472
{
    public class TestMessageHandler : IHandleMessages<TestMessage>
    {
        private static readonly ILog logger = LogManager.GetLogger<TestMessageHandler>();

        public Task Handle(TestMessage message, IMessageHandlerContext context)
        {
            logger.Info("Message Handled.");
            return Task.CompletedTask;
        }
    }
}
