using Akka.Actor;
using Akka.DI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class NotificationActor : UntypedActor
    {
        private readonly IEmailNotification emailNotification;
        private readonly IActorRef childActor;

        public NotificationActor(IEmailNotification emailNotification)
        {
            this.emailNotification = emailNotification;
            childActor = Context.ActorOf(Context.System.DI().Props<TextNotificationActor>());
        }

        protected override void OnReceive(object message)
        {
            Console.WriteLine("Message received: " + message);
            this.emailNotification.Send(message.ToString());
            childActor.Tell(message);
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(  //postoji i AllForOneStrategy, tu ce sva deca da dobiju instrukcije, ne samo ono koje je prijavilo izuzetak
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromMinutes(1),
                localOnlyDecider: ex =>
                {
                    if (ex is ArgumentException)
                        return Directive.Resume;
                    if (ex is NullReferenceException)
                        return Directive.Restart;
                    return Directive.Stop;
                }
             );
        }

        protected override void PreStart()
        {
            Console.WriteLine("Actor started!");
        }

        protected override void PostStop()
        {
            Console.WriteLine("Actor stopped!");
        }
    }
}
