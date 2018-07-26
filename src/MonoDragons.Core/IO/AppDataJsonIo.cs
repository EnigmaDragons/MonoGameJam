using System;
using System.IO;
using Newtonsoft.Json;

namespace MonoDragons.Core.IO
{
    public sealed class AppDataJsonIo
    {
        private readonly string _gameStorageFolder;

        public AppDataJsonIo(string gameFolderName)
        {
            _gameStorageFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), gameFolderName);
        }

        public T Load<T>(string saveName)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(GetSavePath(saveName)));
        }

        public void Save(string saveName, object data)
        {
            if (!Directory.Exists(_gameStorageFolder))
                Directory.CreateDirectory(_gameStorageFolder);
            File.WriteAllText(GetSavePath(saveName), JsonConvert.SerializeObject(data));
        }

        public bool HasSave(string saveName)
        {
            return File.Exists(GetSavePath(saveName));
        }

        public void Delete(string saveName)
        {
            File.Delete(GetSavePath(saveName));
        }

        private string GetSavePath(string saveName)
        {
            return Path.Combine(_gameStorageFolder, saveName + ".json");
        }
    }
}
