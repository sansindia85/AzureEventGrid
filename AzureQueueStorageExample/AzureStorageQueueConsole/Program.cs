using System; // Namespace for Console output
using System.Configuration;
using System.Text; // Namespace for ConfigurationManager
using System.Threading.Tasks; // Namespace for Task
using Azure.Storage.Queues; // Namespace for Queue storage types
using Azure.Storage.Queues.Models; // Namespace for PeekedMessage

namespace AzureStorageQueueConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DequeueMessage();
        }

        public static void DequeueMessage()
        {
            //ToDO: Get the connection string from app settings
            var connectionString =
                @"DefaultEndpointsProtocol=https;AccountName=testbusinesstorage;AccountKey=Dm8SUVbN8WvU+LapoOGTB+O+u3b07Tt5WPY9cqTS6WOvNQSR1Os7DLvQkubIk0VnzMMiOrZeEk3j5ykZK8A3qw==;EndpointSuffix=core.windows.net";

            var queueClient = new QueueClient(connectionString, "test");

            if (queueClient.Exists())
            {
                // Get the next message
                QueueMessage[] retrievedMessage = queueClient.ReceiveMessages();

                // Process (i.e. print) the message in less than 30 seconds
                Console.WriteLine($"Dequeued message: '{Encoding.UTF8.GetString(Convert.FromBase64String(retrievedMessage[0].MessageText))}'");

                // Delete the message
                queueClient.DeleteMessage(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);
            }

        }
    }
}
