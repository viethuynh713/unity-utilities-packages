using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VPackages.System.SaveLoadSystem.impl;

namespace VPackages.System.SaveLoadSystem.Sample
{
    
    public class PlayerPrefSaveLoadSample : MonoBehaviour
    {
        public class SampleData
        {
            public string name;
            public int age;
            public List<string> childrenNames;
        }
        private ISaveLoadSystem _saveLoadSystem;

        private async void Awake()
        {
            _saveLoadSystem = new PlayerPrefSaveLoad();
            _saveLoadSystem.CustomKey = "SaveLoadSystemSample";
            
            // Save data using async methods
            var intSaveResult = await _saveLoadSystem.SaveData("int", 1);
            Debug.Log($"Save int state: {intSaveResult}");
            
            var stringSaveResult = await _saveLoadSystem.SaveData("string", "12345");
            Debug.Log($"Save String state: {stringSaveResult}");
            
            var sampleData = new SampleData()
            {
                name = "John Doe",
                age = 25,
                childrenNames = new List<string>(){"John Doe1","John Doe2","John Doe3"}
            };
            
            var objectSaveResult = await _saveLoadSystem.SaveData("object", sampleData);
            Debug.Log($"Save object data state: {objectSaveResult}");
            
            // Get data using async methods
            var intValue = await _saveLoadSystem.GetData<int>("int");
            Debug.Log($"GetInt value: {intValue}");
            
            var stringValue = await _saveLoadSystem.GetData<string>("string");
            Debug.Log($"GetString value: {stringValue}");
            
            var objectValue = await _saveLoadSystem.GetData<SampleData>("object");
            Debug.Log($"GetObject value: {JsonUtility.ToJson(objectValue)}");
            
            _saveLoadSystem.ClearAllData();
        }
    }
}