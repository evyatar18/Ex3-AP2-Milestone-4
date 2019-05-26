using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class ConnectionModel : IModel
    {
        private IFlightClient client;

        public ConnectionModel(string serverIp, int serverPort)
        {
            this.client = new FlightClient(serverIp, serverPort);
            this.client.Open();
        }

        public FlightData GetNextFlightData()
        {
            string[] requests = { "get /position/latitude-deg", "get /position/longitude-deg" };
            this.client.SendLine("get /position/latitude-deg");
            int lat = int.Parse(this.client.GetLine());

            this.client.SendLine("get /position/longitude-deg");
            int lon = int.Parse(this.client.GetLine());

            return new FlightData(lat, lon);
        }
    }
}