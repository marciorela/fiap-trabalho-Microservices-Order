using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;
using System.Text;

namespace Messages.Service
{
    public class Message: IDisposable
    {
        private ServiceBusClient s_client;
        private ServiceBusAdministrationClient s_adminClient;
        private const string SubscriptionName = "paulista_store";

        public Message()
        {
            var config = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json")
              .Build();

            var connection = config.GetConnectionString("ServiceBus");

            s_client = new ServiceBusClient(connection);
            s_adminClient = new ServiceBusAdministrationClient(connection);
        }

        public async Task Send(string topicName, IMessage message)
        {
            var s_sender = s_client.CreateSender(topicName);

            try
            {
                await CreateTopic(topicName);

                await CreateSubscription(topicName, SubscriptionName);

                await s_adminClient.DeleteRuleAsync(topicName, SubscriptionName, RuleProperties.DefaultRuleName);
                await s_adminClient.CreateRuleAsync(topicName, SubscriptionName, new CreateRuleOptions(RuleProperties.DefaultRuleName, new TrueRuleFilter()));

                ServiceBusMessage msg = new();

                var bodySerialized = JsonConvert.SerializeObject(message);
                var bodyByteArray = Encoding.UTF8.GetBytes(bodySerialized);

                msg.Body = new BinaryData(bodyByteArray);
                msg.MessageId = Guid.NewGuid().ToString();

                await s_sender.SendMessageAsync(msg);
            }
            finally
            {
                await s_sender.CloseAsync();
            }
        }

        private async Task CreateTopic(string topicName)
        {
            var found = false;
            var topics = s_adminClient.GetTopicsAsync();
            await foreach (var topic in topics)
            {
                if (topic.Name == topicName)
                {
                    found = true;
                    break;
                }
            }

            if (!found) { 
                await s_adminClient.CreateTopicAsync(topicName);
            }
        }

        private async Task CreateSubscription(string topicName, string subscriptionName)
        {
            var found = false;
            var subs = s_adminClient.GetSubscriptionsAsync(topicName);
            await foreach (var sub in subs)
            {
                if (sub.SubscriptionName == subscriptionName)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                await s_adminClient.CreateSubscriptionAsync(topicName, subscriptionName);
            }
        }

        public void Receive()
        {

        }

        public async void Dispose()
        {
            await s_client.DisposeAsync();
        }
    }
}