using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Ex3.Models
{
    public class SaveModel : IModel
    {
        private IModel decorated;
        private string path;
        private int numOfIterations;
        private FileStream fs;

        public SaveModel(IModel model, string path, int numOfIterations)
        {
            this.decorated = model;
            this.path = path;
            this.numOfIterations = numOfIterations;
            CreateFile(path);
        }

        public SaveModel(SaveModel model, int numOfIterations)
        {
            this.decorated = model.decorated;
            this.path = model.path;
            this.numOfIterations = numOfIterations;
            CreateFile(path);
        }

        private void CreateFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            this.fs = File.Create(path);
        }

        public FlightData GetNextFlightData()
        {
            FlightData data = this.decorated.GetNextFlightData();

            if (numOfIterations > 0)
            {
                numOfIterations--;
                AddText($"{data.Lon},{data.Lat},{data.Throttle},{data.Rudder}{Environment.NewLine}");

                if (numOfIterations == 0)
                {
                    fs.Close();
                }
            }

            return data;
        }

        private void AddText(string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}