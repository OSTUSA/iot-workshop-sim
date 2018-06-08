﻿using System;
using System.Threading.Tasks;
using System.Timers;

namespace SimService
{
    public class CycleManager
    {
        readonly Timer _timer;
        readonly IDeviceManager _deviceManager;

        public CycleManager( IConfigService configService )
        {
            _deviceManager = new DeviceManager( configService );

            _timer = new Timer( 1000 ) { AutoReset = false };
            _timer.Elapsed += TickHandler;
        }

        private void TickHandler( object sender, ElapsedEventArgs e )
        {
            Console.WriteLine( "It is {0} and all is well", DateTime.Now );
            Task.WaitAll( _deviceManager.TickAsync() );
            _timer.Start();
        }

        public void Start() { _timer.Start(); }
        public void Stop() { _timer.Stop(); }
    }
}
