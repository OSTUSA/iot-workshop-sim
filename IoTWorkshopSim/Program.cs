using System;
using Topshelf;

namespace IoTWorkshopSim
{
    class Program
    {
        static void Main( string[] args )
        {
            var configService = new ConfigService();
            var rc = HostFactory.Run( x =>                                   
            {
                x.Service<CycleManager>( s =>                                   
                {
                    s.ConstructUsing( name => new CycleManager( configService ) );                
                    s.WhenStarted( tc => tc.Start() );                         
                    s.WhenStopped( tc => tc.Stop() );                          
                } );
                x.RunAsLocalSystem();                                       

                x.SetDescription( "Device Simulator" );                   
                x.SetDisplayName( "Device Simulator" );                                  
                x.SetServiceName( "OST Device Simulator" );

                x.StartAutomatically();
            } );                                                             

            var exitCode = (int)Convert.ChangeType( rc, rc.GetTypeCode() );
            Environment.ExitCode = exitCode;
        }
    }
}
