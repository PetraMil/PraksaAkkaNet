using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class TextNotificationActor : UntypedActor
    {
        protected override void PreStart()
        {
            Console.WriteLine("TextNotification child started!");
        }

        protected override void PostStop()
        {
            Console.WriteLine("TextNotification child stopped!");
        }

        protected override void OnReceive(object message)
        {
            if (message.ToString() == "n")
                throw new NullReferenceException();
            if (message.ToString() == "e")
                throw new ArgumentException();
            if (string.IsNullOrEmpty(message.ToString()))
                throw new Exception();

            Console.WriteLine("Sending text message: " + message);
            //throw new Exception("Test"); //vidi se da roditelj aktor restartuje dete ukoliko dodje do greske
        }
    }
}
