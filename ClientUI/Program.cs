using Messages.Commands;
using NServiceBus;
using NServiceBus.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientUI
{
    class Program
    {
        static ILog log = LogManager.GetLogger<Program>();

        static async Task RunLoop(IEndpointInstance endpointInstance)
        {
            while (true)
            {
                log.Info("Press 'P' to place an order, or 'Q' to quit.");
                var key = Console.ReadKey();
                Console.WriteLine();

                switch (key.Key)
                {
                    case ConsoleKey.P:
                        // Instantiate the command
                        var command = new PlaceOrderCommand
                        {
                            OrderId = Guid.NewGuid().ToString()
                        };

                        // Send the command to the local endpoint
                        log.Info($"Sending PlaceOrderCommand, OrderId = { command.OrderId }");
                        // The Local part means that we are not sending to an external endpoint (in a different process) so we 
                        // intend to handle the message in the same endpoint that sent it
                        await endpointInstance.SendLocal(command)
                            .ConfigureAwait(false);
                        break;

                    case ConsoleKey.Q:
                        return;

                    default:
                        log.Info("Unknown input. PLease try again.");
                        break;
                }
            }
        }

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

            await RunLoop(endpointInstance).ConfigureAwait(false);

            await endpointInstance.Stop().ConfigureAwait(false);
        }
    }
}
