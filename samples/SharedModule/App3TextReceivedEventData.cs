using Volo.Abp.EventBus;

namespace SharedModule
{
    /// <summary>
    /// Used to indicate that App3 has received a text message.
    /// </summary>
    [EventName("Test.App3TextReceived")] //Optional event name
    public class App3TextReceivedEventData
    {
        public string ReceivedText { get; set; }

        public App3TextReceivedEventData()
        {

        }

        public App3TextReceivedEventData(string receivedText)
        {
            ReceivedText = receivedText;
        }
    }
}