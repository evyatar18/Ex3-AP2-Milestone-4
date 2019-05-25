using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
            throw new NotImplementedException();
        }
    }
}