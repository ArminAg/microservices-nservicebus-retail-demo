using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            Console.Title = "ClientUI";

            // EndpointConfiguration class is where we define all the settings that determine how our endpoint will operate
            var endpointConfiguration = new EndpointConfiguration("ClientUI");
            // This setting defines the transport that NServiceBus will use to send and receive messages
            var transport = endpointConfiguration.UseTransport<MsmqTransport>();
            // When sending messages, an endpoint needs to serialize message objects to a stream, and then deserialize the stream back to a message object on the receiving end
            endpointConfiguration.UseSerialization<JsonSerializer>();
            // A persistence is required to store some data in between handling messages
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.SendFailedMessagesTo("error");
            endpointConfiguration.EnableInstallers();

            var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();

            await endpointInstance.Stop().ConfigureAwait(false);

        }
    }
}
