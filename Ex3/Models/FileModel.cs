using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class FileModel : IModel
    {
        private Queue<FlightData> DataQueue;

        public FileModel(string path)
        {
            InitData(path);
        }

        /// <summary>
        /// initialize all the flight data from the file to a queue
        /// </summary>
        /// <param name="path">the path to the file</param>
        private void InitData(string path)
        {
            FlightData[] arr = ReadData(path);

            this.DataQueue = new Queue<FlightData>();

            if (arr == null)
            {
                return;
            }

            foreach (FlightData data in arr)
            {
                this.DataQueue.Enqueue(data);
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
                int lat = int.Parse(split[0]);
                int lon = int.Parse(split[1]);

                data[i] = new FlightData(lat, lon);
            }

            return data;
        }

        public FlightData GetNextFlightData()
        {
            if (this.DataQueue.Count == 0)
            {
                return null;
            }

            return this.DataQueue.Dequeue();
        }
    }
}