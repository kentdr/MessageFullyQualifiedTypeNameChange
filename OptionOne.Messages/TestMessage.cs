namespace OptionOne.Messages
{
    public class TestMessage
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
