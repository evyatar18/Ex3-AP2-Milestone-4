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

        public bool IsAlive { get; private set; }

        public ConnectionModel(string serverIp, int serverPort)
        {
            this.client = new FlightClient(serverIp, serverPort);
            this.client.Open();
            IsAlive = true;
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

            try
            {
                // get lon
                this.client.SendLine("get /position/longitude-deg");
                double lon = RetrieveDouble();

                // get lat
                this.client.SendLine("get /position/latitude-deg");
                double lat = RetrieveDouble();

                // get lon
                this.client.SendLine("get /controls/engines/current-engine/throttle");
                double throttle = RetrieveDouble();

                // get lon
                this.client.SendLine("get /controls/flight/rudder");
                double rudder = RetrieveDouble();

                // build the data
                FlightData data = new FlightData(lat, lon, throttle, rudder);

                return data;
            }
            catch (Exception)
            {
                IsAlive = false;
                return null;
            }
            finally
            {
                // release the mutex even if an exception occurs
                mutex.ReleaseMutex();
            }
        }
    }
}
