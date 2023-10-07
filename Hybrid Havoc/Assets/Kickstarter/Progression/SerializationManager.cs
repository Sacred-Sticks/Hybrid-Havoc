using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Kickstarter.Progression
{
    public abstract class SerializationManager : MonoBehaviour
    {
        private class Datapoint
        {
            public string FileLocation { get; }

            public Datapoint(string fileLocation)
            {
                FileLocation = fileLocation;
            }
        }
        private class Datapoint<TDataType> : Datapoint
        {
            public TDataType Data
            {
                get
                {
                    return SaveData.Invoke();
                }
            }
            public Action<TDataType> LoadData { get; }
            public Func<TDataType> SaveData { get; }

            public Datapoint(string fileLocation, Action<TDataType> loadData, Func<TDataType> saveData) : base(fileLocation)
            {
                LoadData = loadData;
                SaveData = saveData;
            }
        }
        
        private readonly List<Datapoint> allData = new List<Datapoint>();
        
        private void SaveData<TDataType>(TDataType data, string fileName) where TDataType : ISerializable, new()
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            var formatter = new BinaryFormatter();
            var stream = new FileStream(filePath, FileMode.Create);

            formatter.Serialize(stream, data.Serialize());
            stream.Close();
        }

        private bool LoadData<TType>(string fileName, out TType output) where TType : ISerializable, new()
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            if (!File.Exists(filePath))
            {
                output = default;
                return false;
            }
            var formatter = new BinaryFormatter();
            var stream = new FileStream(filePath, FileMode.Open);

            output = new TType();
            output.Deserialize((string)formatter.Deserialize(stream));
            stream.Close();

            return true;
        }

        protected void AddData<TDataType>(string fileLocation, Action<TDataType> loadData, Func<TDataType> saveData)
        {
            var matchingLocation = allData.Where(d => d.FileLocation == fileLocation);
            if (matchingLocation.Any())
                return;
            allData.Add(new Datapoint<TDataType>(fileLocation, loadData, saveData));
        }

        public void RemoveData<TDataType>(string fileLocation)
        {
            var matchingLocation = allData.Where(d => d.FileLocation == fileLocation)
                .ToArray();
            if (matchingLocation.Length == 0)
                return;
            var datapoint = matchingLocation.First();
            allData.Remove(datapoint);
        }

        protected void SaveAll<TDataType>() where TDataType : ISerializable, new()
        {
            allData.ForEach(d =>
            {
                var data = (Datapoint<TDataType>)d;
                SaveData<TDataType>(data.SaveData(), data.FileLocation);
            });
        }

        protected void LoadAll<TType>() where TType : ISerializable, new()
        {
            allData.ForEach(d =>
            {
                LoadData(d.FileLocation, out TType output);
                var datapoint = (Datapoint<TType>)d;
                datapoint.LoadData(output);
            });
        }
    }
}
