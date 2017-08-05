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

            //ServiceConfig.ConfigFileName = "Headoffice.SL.Config";

            //HostFactory.Run(x =>
            //{
            //    x.Service<HeadofficeServiceLayer>(s =>
            //    {
            //        s.ConstructUsing(f => new HeadofficeServiceLayer());
            //        s.WhenStarted(r => r.Start());
            //        s.WhenStopped(r => r.Stop());
            //    });

            //    x.EnableServiceRecovery(r =>
            //    {
            //        r.RestartService(1); //first
            //        r.RestartService(1); //second
            //        r.RestartService(1); //subsequents
            //        r.SetResetPeriod(0);
            //    });

            //    x.StartAutomatically();
            //    x.RunAsLocalSystem();

            //    x.SetDescription("Avelon RMS Service Layer");
            //    x.SetDisplayName("Avelon RMS Service Layer");
            //    x.SetServiceName("AvelonRMSServiceLayer");

            //    //
            //    // We can use custom parameters: -cf <config file>
            //    //

            //    x.AddCommandLineDefinition("cf", v =>
            //    {
            //        ServiceConfig.ConfigFileName = v;
            //        Console.WriteLine("Using configuration file: {0}", v);
            //        Console.WriteLine();
            //    });
            //});
        }

        public class HeadofficeServiceLayer
        {
            public void Start()
            {
                Console.WriteLine("{0:hh:mm:ss:fff} - SetupRegularEndpoints - Start", DateTime.Now);
                ServiceLayer.SetupRegularEndpoints();
                Console.WriteLine("{0:hh:mm:ss:fff} - SetupRegularEndpoints - End", DateTime.Now);
            }

            public void Stop()
            {
            }
        }
    }
}
