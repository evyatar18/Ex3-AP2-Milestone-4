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
        private double lat = 0;
        public double Lat => lat;

        private double lon = 0;
        public double Lon => lon;

        public FlightData (double lat, double lon) {
            this.lat = lat;
            this.lon = lon;
        }
    }
}