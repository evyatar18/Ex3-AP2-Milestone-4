using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3.Models
{
    interface IFlightClient
    {
        string IP { get; set; }
        uint Port { get; set; }

        bool IsOpen { get; }

        void Open();
        void Close();

        void SendLine(string line);
        string ReadLine();
    }
}
