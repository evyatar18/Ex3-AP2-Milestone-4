using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex3.Models
{
    public interface IModel
    {
        /// <summary>
        /// Get the next FlightData
        /// </summary>
        /// <returns>The next FlightData</returns>
        FlightData GetNextFlightData();
    }
}
