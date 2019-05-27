using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ex3.Models
{
    public class MainModel
    {
        private IDictionary<string, IModel> models;

        public MainModel()
        {
            this.models = new ConcurrentDictionary<string, IModel>();
        }

        /// <summary>
        /// Add a ConnectionModel
        /// </summary>
        /// <param name="serverIp">The ip</param>
        /// <param name="serverPort">The port</param>
        /// <returns>The ConnectionModel with the ip and the port</returns>
        public IModel AddConnectionModel(string serverIp, int serverPort)
        {
            IModel model;
            if (this.models.ContainsKey($"{serverIp}:{serverPort}"))
            {
                model = this.models[$"{serverIp}:{serverPort}"];
                return model;
            }

            this.models[$"{serverIp}:{serverPort}"] = new ConnectionModel(serverIp, serverPort);
            model = this.models[$"{serverIp}:{serverPort}"];
            return model;
        }

        /// <summary>
        /// Add a SaveModel
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <param name="serverIp">The ip</param>
        /// <param name="serverPort">The port</param>
        /// <returns>The ConnectionModel with the ip and the port and the file</returns>
        public IModel AddSaveModel(string path, string serverIp, int serverPort)
        {
            IModel model;
            if (this.models.ContainsKey($"{path}:{serverIp}:{serverPort}"))
            {
                return null;
            }

            IModel decorated = AddConnectionModel(serverIp, serverPort);
            this.models[$"{path}:{serverIp}:{serverPort}"] = new SaveModel(decorated, path);
            model = this.models[$"{serverIp}:{serverPort}"];
            return model;
        }

        /// <summary>
        /// Add a FileModel
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>The FileModel with the file</returns>
        public IModel AddFileModel(string path)
        {
            IModel model;
            if (this.models.ContainsKey(path))
            {
                model = this.models[path];
                return new FileModel((FileModel)model);
            }

            this.models[path] = new FileModel(path);
            model = this.models[path];
            return model;
        }
    }
}