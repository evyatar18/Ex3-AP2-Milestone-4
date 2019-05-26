using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Ex3.Models
{
    public class ConnectionModel : IModel
    {
        private static Mutex mutex = new Mutex();

        private IFlightClient client;

        public ConnectionModel(string serverIp, int serverPort)
        {
            this.client = new FlightClient(serverIp, serverPort);
            this.client.Open();
        }

        public FlightData GetNextFlightData()
        {
            mutex.WaitOne();  // Wait until it is safe to enter. 

            // get lat
            this.client.SendLine("get /position/latitude-deg");
            int lat = int.Parse(this.client.GetLine());

            // get lon
            this.client.SendLine("get /position/longitude-deg");
            int lon = int.Parse(this.client.GetLine());

            // build the data
            FlightData data = new FlightData(lat, lon);

            mutex.ReleaseMutex();  // Release the Mutex.

            return data;
        }
    }
}