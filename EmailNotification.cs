using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class EmailNotification : IEmailNotification
    {
        public void Send(string message)
        {
            Console.WriteLine("Sending email with messsage: " + message);
        }
    }
}
