using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Ex3.Models
{
    public class ConnectionModel : IModel
    {
        private Mutex mutex = new Mutex();

        private IFlightClient client;

        public ConnectionModel(string serverIp, int serverPort)
        {
            this.client = new FlightClient(serverIp, serverPort);
            this.client.Open();
        }

        private double RetrieveDouble()
        {
            string line = client.GetLine();

            double ret = double.NaN;

            double.TryParse(line.Split('\'')[1], out ret);
            return ret;
        }

        public FlightData GetNextFlightData()
        {
            mutex.WaitOne();  // Wait until it is safe to enter. 

            // get lat
            this.client.SendLine("get /position/latitude-deg");
            double lat = RetrieveDouble();

            // get lon
            this.client.SendLine("get /position/longitude-deg");
            double lon = RetrieveDouble();

            // build the data
            FlightData data = new FlightData(lat, lon);

            mutex.ReleaseMutex();  // Release the Mutex.

            return data;
        }
    }
}
