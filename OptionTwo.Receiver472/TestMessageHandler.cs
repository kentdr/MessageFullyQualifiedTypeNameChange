using NServiceBus;
using NServiceBus.Logging;
using System.Threading.Tasks;
using OptionTwo.Receiver472.Messages;

namespace OptionTwo.Receiver472
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
