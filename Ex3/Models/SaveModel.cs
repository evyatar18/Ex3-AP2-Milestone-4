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
        private int numOfIterations;

        public SaveModel(IModel model, string path, int numOfIterations)
        {
            this.decorated = model;
            this.path = path;
            this.numOfIterations = numOfIterations;
            File.WriteAllText(path, String.Empty);
        }

        public SaveModel(SaveModel model, int numOfIterations)
        {
            this.decorated = model.decorated;
            this.path = model.path;
            this.numOfIterations = numOfIterations;
            File.WriteAllText(path, String.Empty);
        }

        public FlightData GetNextFlightData()
        {
            FlightData data = this.decorated.GetNextFlightData();

            if (numOfIterations > 0)
            {
                numOfIterations--;
                using (StreamWriter sw = File.AppendText(this.path))
                {
                    sw.WriteLine($"{data.Lat},{data.Lon}");
                }
            }

            return data;
        }
    }
}