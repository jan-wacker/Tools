using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Startup
{
    class Program
    {
        static void Main(string[] args)
        {
            string betreff = "Server Neustart erfolgreich";

            var rechnerName = Environment.MachineName;
            string nachricht = $"Der Rechner [{rechnerName}] wurde neugestartet aktuelle Uhrzeit: '{DateTime.Now}'";

            Console.WriteLine("Neustart Email Sender");
            EMailVersender.SendeEmail(betreff, nachricht);
        }
    }
}
