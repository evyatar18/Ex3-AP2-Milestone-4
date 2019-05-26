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

        public IModel AddConnectionModel(string serverIp, int serverPort)
        {
            IModel model;
            if (this.models.ContainsKey($"{serverIp}:{serverPort}"))
            {
                this.models.TryGetValue($"{serverIp}:{serverPort}", out model);
                return model;
            }

            this.models[$"{serverIp}:{serverPort}"] = new ConnectionModel(serverIp, serverPort);
            this.models.TryGetValue($"{serverIp}:{serverPort}", out model);
            return model;
        }

        public IModel AddFilenModel(string path)
        {
            IModel model;
            if (this.models.ContainsKey(path))
            {
                return null;
            }

            this.models[path] = new FileModel(path);
            this.models.TryGetValue(path, out model);
            return model;
        }
    }
}