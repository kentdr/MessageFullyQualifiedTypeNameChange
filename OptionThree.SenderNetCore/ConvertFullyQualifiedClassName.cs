using NServiceBus.MessageMutator;
using System.Threading.Tasks;

namespace OptionThree.SenderNetCore
{
    public class ConvertFullyQualifiedClassName : IMutateOutgoingTransportMessages
    {
        private const string EnclosedMessageTypeHeader = "NServiceBus.EnclosedMessageTypes";
        private const string TargetTestMessageAssemblyQualifiedName = "OptionThree.Receiver472.Messages.TestMessage, OptionThree.Receiver472, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";

        public Task MutateOutgoing(MutateOutgoingTransportMessageContext context)
        {
            if (context.OutgoingHeaders.ContainsKey(EnclosedMessageTypeHeader) &&
                context.OutgoingHeaders[EnclosedMessageTypeHeader].Contains("TestMessage"))
            {
                context.OutgoingHeaders[EnclosedMessageTypeHeader] = TargetTestMessageAssemblyQualifiedName;
            }

            return Task.CompletedTask;
        }
    }
}
