using NServiceBus;

namespace OptionTwo.Receiver472.Messages
{
    public class TestMessage : IMessage
    {
        public TestMessage(string valueOne, string valueTwo)
        {
            ValueOne = valueOne;
            ValueTwo = valueTwo;
        }

        public string ValueOne { get; set; }

        public string ValueTwo { get; set; }
    }
}
