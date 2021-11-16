using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<IEmailNotification, EmailNotification>();
            serviceCollection.AddScoped<NotificationActor>();
            serviceCollection.AddScoped<TextNotificationActor>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var actorSystem = ActorSystem.Create("test-actor-system");
            actorSystem.UseServiceProvider(serviceProvider);

            var actor = actorSystem.ActorOf(actorSystem.DI().Props<NotificationActor>()); //ako se Aktor ne kreira na ovaj nacin bice tretira kao obicna klasa

            Console.WriteLine("Enter message: ");
            while (true)
            {
                var message = Console.ReadLine();
                if (message == "q")
                    break;
                actor.Tell(message);
            }

            //actor.Tell("Hello there!"); //aktivira onReceive metodu
            actorSystem.Stop(actor);
            Console.ReadLine();

        }
    }
}
