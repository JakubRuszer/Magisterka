using StreamingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace StreamingHosting
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Uruchamianie usługi...");
            using (ServiceHost host = new ServiceHost(typeof(Service)))
            {
                try
                {
                    host.Open();
                    Console.WriteLine("Usługa uruchomiona. Naciśnij Enter aby zakończyć.");
                    Console.ReadLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.WriteLine("Usługa zatrzymana.");
            Console.ReadLine();
        }
    }
}
