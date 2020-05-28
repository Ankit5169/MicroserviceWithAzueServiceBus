using Microsoft.Azure.ServiceBus;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessagesManagement
{
    public class ReceiveMessage: IMessageReceiver
    {
        const string ServiceBusConnectionString = "Endpoint=sb://ap-service-bus.servicebus.windows.net/;SharedAccessKeyName=ap-test-listen;SharedAccessKey=mJMLV/jBd5FFSmGPiZB3yCGiA1fzx4SJ4U4SElA0YF4=";
        const string TopicName = "ap-test-topic";
        const string SubscriptionName = "ap-sub-send";
        private readonly IRestaurantBusiness _restaurantBusiness;
        static ISubscriptionClient subscriptionClient;

        public ReceiveMessage(IRestaurantBusiness restaurantBusiness)
        {
            _restaurantBusiness = restaurantBusiness;
            //subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, TopicName, SubscriptionName);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, TopicName, SubscriptionName);
            // Configure the message handler options in terms of exception handling, number of concurrent messages to deliver, etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of concurrent calls to the callback ProcessMessagesAsync(), set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether MessagePump should automatically complete the messages after returning from User Callback.
                // False below indicates the Complete will be handled by the User Callback as in `ProcessMessagesAsync` below.
                AutoComplete = false
            };

            // Register the function that processes messages.
            subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }
        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var id = JsonConvert.DeserializeObject<object>(Encoding.UTF8.GetString(message.Body));

            var result = _restaurantBusiness.ItemInStock(1,2);

            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
            // Note: Use the cancellationToken passed as necessary to determine if the subscriptionClient has already been closed.
            // If subscriptionClient has already been closed, you can choose to not call CompleteAsync() or AbandonAsync() etc.
            // to avoid unnecessary exceptions.
        }

        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
