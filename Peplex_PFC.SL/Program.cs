using System;
using Topshelf;

namespace Peplex_PFC.SL
{
    public class Program
    {
        private static void Main()
        {
            Console.SetWindowSize(Console.WindowWidth, Console.WindowHeight/2);

            HostFactory.Run(x =>
            {
                x.Service<HeadofficeServiceLayer>(sc =>
                {
                    sc.ConstructUsing(name => new HeadofficeServiceLayer());
                    sc.WhenStarted(rn => rn.Start());
                    sc.WhenStopped(rn => rn.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("Servicio para acceder a la información de la biblioteca multimedia de la aplicación Peplex.");
                x.SetDisplayName("Peplex Console");
                x.SetServiceName("Peplex Service");
            });
        }

        public class HeadofficeServiceLayer
        {
            public void Start()
            {
                Console.WriteLine("{0:hh:mm:ss:fff} - SetupRegularEndpoints - Start", DateTime.Now);
                ServiceLayer.SetupRegularEndpoints();
                Console.WriteLine("{0:hh:mm:ss:fff} - SetupRegularEndpoints - End", DateTime.Now);
                Console.WriteLine("{0:hh:mm:ss:fff} - SetupRestEndpoints - Start", DateTime.Now);
                ServiceLayer.SetupRestEndpoints();
                Console.WriteLine("{0:hh:mm:ss:fff} - SetupRestEndpoints - End", DateTime.Now);
            }

            public void Stop()
            {
            }
        }
    }
}
