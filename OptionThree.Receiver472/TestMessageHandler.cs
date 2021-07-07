using NServiceBus;
using NServiceBus.Logging;
using System.Threading.Tasks;
using OptionThree.Receiver472.Messages;

namespace OptionThree.Receiver472
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
