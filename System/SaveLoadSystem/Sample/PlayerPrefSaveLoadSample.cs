using System;
using System.Collections.Generic;
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

        private void Awake()
        {
            _saveLoadSystem = new PlayerPrefSaveLoad();
            _saveLoadSystem.CustomKey ="SaveLoadSystemSample";
            _saveLoadSystem.SaveData("int",1,SaveIntCallback);
            _saveLoadSystem.SaveData("string","12345",SaveStringCallback);
            
            var sampleData = new SampleData()
            {
                name = "John Doe",
                age = 25,
                childrenNames = new List<string>(){"John Doe1","John Doe2","John Doe3"}
            };
            
            _saveLoadSystem.SaveData("object",sampleData,SaveObjectCallback);
            
            _saveLoadSystem.GetData<int>("int",GetIntCallback);
            _saveLoadSystem.GetData<string>("string",GetStringCallback);
            _saveLoadSystem.GetData<SampleData>("object",GetObjectCallback);
            _saveLoadSystem.ClearAllData();
        }

        private void GetObjectCallback(SaveLoadResult arg1, SampleData arg2)
        {
            Debug.Log($"GetObject {arg1} with value {JsonUtility.ToJson(arg2)}");
        }

        private void GetStringCallback(SaveLoadResult arg1, string arg2)
        {
            Debug.Log($"GetString {arg1} with value: {arg2}");
        }

        private void GetIntCallback(SaveLoadResult arg1, int arg2)
        {
            Debug.Log($"GetInt {arg1} with value: {arg2}");
        }


        private void SaveObjectCallback(SaveLoadResult obj)
        {
            Debug.Log($"Save object data state: {obj}");
        }

        private void SaveStringCallback(SaveLoadResult obj)
        {
            Debug.Log($"Save String state: {obj}");
        }

        private void SaveIntCallback(SaveLoadResult obj)
        {
            Debug.Log($"Save Int state: {obj}");
        }
    }
}