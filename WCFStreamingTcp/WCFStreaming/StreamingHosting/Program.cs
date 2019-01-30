using StreamingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
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
                    ServiceMetadataBehavior smb = host.Description.Behaviors.Find<ServiceMetadataBehavior>();
                    // If not, add one
                    if (smb == null)
                    {
                        smb = new ServiceMetadataBehavior();
                        host.Description.Behaviors.Add(smb);
                    }
                    smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                    host.AddServiceEndpoint(
                      ServiceMetadataBehavior.MexContractName,
                      MetadataExchangeBindings.CreateMexTcpBinding(),
                      "mex"
                    );
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
