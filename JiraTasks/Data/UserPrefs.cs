using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace JiraTasks.Data
{
    public class UserPrefs
    {
        private string Path { get; set; }
        private string Filename { get; set; }
        public Dictionary<string, string> LinkedTaskList { get; set; }

        public UserPrefs(string path, string filename)
        {
            Path = path;
            Filename = filename;
            LinkedTaskList = new Dictionary<string, string>();
        }

        public bool Load()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
                File.Create($@"{Path}/{Filename}").Dispose();
                return false;
            }
            try
            {
                var fileText = File.ReadAllText($@"{Path}/{Filename}");
                var deserialized = JsonConvert.DeserializeObject<UserPrefs>(fileText);
                LinkedTaskList = deserialized.LinkedTaskList;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Save()
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }
            string serialized = JsonConvert.SerializeObject(this);
            File.WriteAllText($@"{Path}/{Filename}", serialized);
        }
    }
}