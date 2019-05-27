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
                model = this.models[$"{serverIp}:{serverPort}"];
                return model;
            }

            this.models[$"{serverIp}:{serverPort}"] = new ConnectionModel(serverIp, serverPort);
            model = this.models[$"{serverIp}:{serverPort}"];
            return model;
        }

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