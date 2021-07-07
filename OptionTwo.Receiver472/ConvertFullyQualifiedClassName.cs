using System;
using NServiceBus.MessageMutator;
using OptionTwo.Receiver472.Messages;
using System.Threading.Tasks;

namespace OptionTwo.Receiver472
{
    public class ConvertFullyQualifiedClassName : IMutateIncomingTransportMessages
    {
        private const string EnclosedMessageTypeHeader = "NServiceBus.EnclosedMessageTypes";

        public Task MutateIncoming(MutateIncomingTransportMessageContext context)
        {
            if (context.Headers.ContainsKey(EnclosedMessageTypeHeader) &&
                context.Headers[EnclosedMessageTypeHeader].Contains("TestMessage"))
            {
                Type messageType = typeof(TestMessage);

                context.Headers[EnclosedMessageTypeHeader] = messageType.AssemblyQualifiedName;
            }

            return Task.CompletedTask;
        }
    }
}
