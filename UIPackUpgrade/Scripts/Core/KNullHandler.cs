// /*
//  * Author: DevDaoSi
//  * @2024
//  */
// using System;
// using UnityEngine;
//
// namespace Konzit.Core.NullObject
// {
//     public sealed class KNullHandler
//     {
//         static IInvoke invoke;
//
//         public static void Operation(object obj, Action callback)
//         {
//             invoke = obj == null ? new NullObject() : new NonNullObject();
//
//             Type objType = null;
//             try
//             {
//                 objType = obj.GetType();
//             }
//             catch (Exception e)
//             {
//                 Debug.Log($"<color=red> object null so not have any type </color>");
//             }
//
//             invoke.Invoke(callback, objType);
//         }
//
//         public class NonNullObject : IInvoke
//         {
//             public void Invoke(Action callback, Type type = null)
//             {
//                 callback?.Invoke();
//             }
//         }
//
//         public class NullObject : IInvoke
//         {
//             public void Invoke(Action callback, Type type = null)
//             {
//                 Debug.Log("Object null with object name: " + type.Name);
//             }
//         }
//     }
//
//     public interface IInvoke
//     {
//         void Invoke(Action callback, Type type = null);
//     }
// }
