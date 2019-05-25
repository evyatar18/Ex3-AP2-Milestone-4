using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    /// <summary>
    /// An object that contains a specific lat and lon
    /// </summary>
    public class FlightData
    {
        private int lat = 0;
        public int Lat => lat;

        private int lon = 0;
        public int Lon => lon;

        public FlightData (int lat, int lon) {
            this.lat = lat;
            this.lon = lon;
        }
    }
}