using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MessagesManagement
{
    public class SendMessage : IMessageSender
    {
        const string ServiceBusConnectionString = "Endpoint=sb://ap-service-bus.servicebus.windows.net/;SharedAccessKeyName=ap-test-send;SharedAccessKey=G+UWgLqmFG22cMfvCyizeSXREOIvT3/8TylThk8b86I=";
        const string TopicName = "ap-test-topic";
        static ITopicClient topicClient;

        public async Task SendMessagesAsync(Object obj, string TopicName)
        {

            try
            {
                topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
                string messageBody = JsonConvert.SerializeObject(obj);
                Message message = new Message(Encoding.UTF8.GetBytes(messageBody));
                await topicClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
