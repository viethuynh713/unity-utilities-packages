// using System;
// using System.Collections.Generic;
// using Cysharp.Threading.Tasks;
// using Gley.AllPlatformsSave;
// using UnityEngine;
// using UnityEngine.Events;
// using VPackages.System.SaveLoadSystem;
//
// namespace VPackage.SaveLoad
// {
//     public enum SaveLoadResult
//     {
//         Success,
//         Fail,
//     }
//     public abstract class SaveLoadBase : ISaveLoadManager
//     {
//         private const string DefaultSavePath = "Application.persistentDataPath/LocalDataFile";
//         protected string SavePath = "";
//
//         public virtual void SaveData<T>(string key, T value, UnityAction<SaveLoadResult> callback = null) where T : class, new()
//         {
//             var path = GetPath(key);
//             Gley.AllPlatformsSave.API.Save<T>(value, path, (result, message) => CompleteSaveCallBack(callback,result,message), true);
//         }
//
//         private void CompleteSaveCallBack(UnityAction<SaveLoadResult> callback, SaveResult result, string message)
//         {
//             if (result == SaveResult.Error || result == SaveResult.EmptyData)
//             {
//                 Debug.LogError("Save fail: "+ message);
//                 callback?.Invoke(SaveLoadResult.Fail);
//             }
//             else
//             {
//                 Debug.Log("Save successfully");
//                 callback?.Invoke(SaveLoadResult.Success);
//
//             }
//         }
//
//         public virtual void GetData<T>(string key, UnityAction<SaveLoadResult,T> callback) where T : class, new()
//         {
//             var path = GetPath(key);
//             Gley.AllPlatformsSave.API.Load<T>(path, (value,result,message) => LoadCompleteCallBack(callback,value,result,message), true);
//         }
//
//         private void LoadCompleteCallBack<T>(UnityAction<SaveLoadResult,T> callback,T value, SaveResult result, string message) where T : class, new()
//         {
//             if (result == SaveResult.Success)
//             {
//                 callback?.Invoke(SaveLoadResult.Success, value);
//             }
//             else if(result == SaveResult.Error || result == SaveResult.EmptyData)
//             {
//                 Debug.LogError("load fail: "+ message);
//                 callback?.Invoke(SaveLoadResult.Fail, null);
//             }
//         }
//
//         public virtual void ClearData(string key)
//         {
//             var path = GetPath(key);
//             Gley.AllPlatformsSave.API.ClearFile(path);
//         }
//
//         public virtual void ClearAllData()
//         {
//             var path = GetFolderPath();
//
//             Gley.AllPlatformsSave.API.ClearAllData(path);
//
//         }
//
//         protected virtual string GetPath(string key)
//         {
//             if (string.IsNullOrEmpty(SavePath))
//             {
//                 return string.Concat(DefaultSavePath,"/", key);
//             }
//
//             return string.Concat(SavePath,"/", key);
//         }
//         protected virtual string GetFolderPath()
//         {
//             if (string.IsNullOrEmpty(SavePath))
//             {
//                 return DefaultSavePath;
//             }
//
//             return SavePath;
//         }
//     }
// }