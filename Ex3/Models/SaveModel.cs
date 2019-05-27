using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class SaveModel : IModel
    {
        private IModel decorated;
        private string path;

        public SaveModel(IModel model, string path)
        {
            this.decorated = model;
            this.path = path;
            File.WriteAllText(path, String.Empty);
        }

        public FlightData GetNextFlightData()
        {
            FlightData data = this.decorated.GetNextFlightData();
            using (StreamWriter sw = File.AppendText(this.path))
            {
                sw.WriteLine($"{data.Lat},{data.Lon}");
            }

            return data;
        }
    }
}