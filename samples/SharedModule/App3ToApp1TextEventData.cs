using Volo.Abp.EventBus;

namespace SharedModule
{
    /// <summary>
    /// Used to send a text message from App3 to App1.
    /// </summary>
    [EventName("Test.App3ToApp1Text")] //Optional event name
    public class App3ToApp1TextEventData
    {
        public string TextMessage { get; set; }

        public App3ToApp1TextEventData()
        {

        }

        public App3ToApp1TextEventData(string textMessage)
        {
            TextMessage = textMessage;
        }
    }
}