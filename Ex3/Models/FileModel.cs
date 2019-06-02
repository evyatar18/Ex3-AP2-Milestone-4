using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class FileModel : IModel
    {

        private IList<FlightData> DataList;
        private int index;

        public bool IsAlive { get; private set; }

        public FileModel(string path)
        {
            InitData(path);
            IsAlive = true;
        }

        public FileModel(FileModel model)
        {
            this.DataList = new List<FlightData>();
            foreach (FlightData data in model.DataList)
            {
                this.DataList.Add(new FlightData(data.Lat, data.Lon, data.Throttle, data.Rudder));
            }
            this.index = 0;
        }

        /// <summary>
        /// initialize all the flight data from the file to a queue
        /// </summary>
        /// <param name="path">the path to the file</param>
        private void InitData(string path)
        {
            FlightData[] arr = ReadData(path);

            this.DataList = new List<FlightData>();

            if (arr == null)
            {
                return;
            }

            foreach (FlightData data in arr)
            {
                this.DataList.Add(data);
            }
        }

        /// <summary>
        /// read the data and get it as FlightData
        /// </summary>
        /// <param name="path">the path to the file</param>
        /// <returns>the flight data as an array of FlightData</returns>
        private FlightData[] ReadData(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine($"The File '{path}' was not found!");
                return null;
            }
            string[] lines;

            lines = File.ReadAllLines(path);


            FlightData[] data = new FlightData[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                string[] split = lines[i].Split(',');
                double lon = double.Parse(split[0]);
                double lat = double.Parse(split[1]);
                double throttle = double.Parse(split[2]);
                double rudder = double.Parse(split[3]);

                data[i] = new FlightData(lat, lon, throttle, rudder);
            }

            return data;
        }

        public FlightData GetNextFlightData()
        {
            if (this.index >= this.DataList.Count)
            {
                IsAlive = false;
                return null;
            }
            int temp = this.index;
            this.index++;
            return this.DataList[temp];
        }
    }
}