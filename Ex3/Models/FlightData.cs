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

        private double rudder = 0;
        public double Rudder => rudder;

        private double throttle = 0;
        public double Throttle => throttle;

        public FlightData (double lat, double lon, double throttle, double rudder) {
            this.lat = lat;
            this.lon = lon;
            this.throttle = throttle;
            this.rudder = rudder;
        }
    }
}