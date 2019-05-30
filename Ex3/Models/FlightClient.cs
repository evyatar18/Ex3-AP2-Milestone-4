using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Web;

namespace Ex3.Models
{
    public class FlightClient : IFlightClient
    {

        public FlightClient(string serverIp, int serverPort)
        {
            IP = serverIp;
            Port = (uint)serverPort;
        }

        private IPAddress ip;
        public string IP
        {
            get => ip.ToString();

            set
            {
                if (IsOpen)
                    throw new InvalidOperationException("Cannot change IP while connection is open!");

                // only set valid ip address
                if (!IPAddress.TryParse(value, out ip))
                    throw new ArgumentException($"Invalid IP inserted ({value}).");
            }
        }

        private int port;
        public uint Port
        {
            get => (uint)port;

            set
            {
                if (IsOpen)
                    throw new InvalidOperationException("Cannot change port while connection is open!");

                if (value < 65536)
                    this.port = (int)value;
                else
                    throw new ArgumentException($"Invalid port inserted({value}).");
            }
        }

        private TcpClient client = null;
        public bool IsOpen { get => client != null && client.Connected; }

        private StreamWriter clientWriter;
        private StreamReader clientReader;

        public void Close()
        {
            if (IsOpen)
            {
                try { clientWriter.Dispose(); clientReader.Dispose(); client.Close(); }
                catch (Exception) { }
            }

            client = null;
            clientWriter = null;
            clientReader = null;
        }

        public void Open()
        {
            if (IsOpen)
                throw new InvalidOperationException("Cannot open connection before closing current one!");

            try
            {
                /* connect to flightgear server */
                IPEndPoint endPoint = new IPEndPoint(ip, port);
                client = new TcpClient();
                client.Connect(endPoint);
                Console.WriteLine("Connected to " + endPoint.ToString());
            }
            catch (Exception) { client = null; return; }

            this.clientWriter = new StreamWriter(client.GetStream());
            this.clientReader = new StreamReader(client.GetStream());
        }

        public string GetLine() => clientReader.ReadLine();


        public void SendLine(string line) =>
            SendLines(new List<string> { line });

        public void SendLines(IList<string> lines)
        {
            if (!IsOpen)
                throw new InvalidOperationException("Cannot send lines to unopened socket.");

            foreach (string line in lines)
            {
                clientWriter.Write(line);
                clientWriter.Write('\r');
                clientWriter.Write('\n');
            }

            clientWriter.Flush();
        }
    }
}