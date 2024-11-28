using System.Collections.Generic;
using UnityEngine;

namespace VPackage.AudioSystem
{
    
    [System.Serializable]
    public class AudioItem
    {
        public AudioName AudioName;

        public AudioType Type;

        public AudioClip AudioClip;

        [Range(0f, 1f)] public float Volume = 1f;

        [Range(0, 256)] public int Priority = 128;

        [Range(0, 1)] public float SpatialBlend;

        [HideInInspector] public AudioSource Source;

        public bool IsLooping;

        public bool PlayOnAwake;
    }
    [CreateAssetMenu(fileName = "New Audio Data", menuName = "ScriptableObjects/Audio/AudioData")]

    public class AudioData : ScriptableObject
    {
        [SerializeField] private AudioItem[] listItem;

        public AudioItem GetAudioByName(AudioName audioName)
        {
            foreach (var item in listItem)
            {
                if(item.AudioName == audioName)return item;
            }
            Debug.LogError($"Not found sound by name {audioName}");
            return null;
        }   
        public List<AudioItem> GetAudiosByName(AudioName audioName) 
        {
            var result = new List<AudioItem>();
            foreach(var item in listItem)
            {
                if(item.AudioName == audioName)result.Add(item);
            } 
            if(result.Count <= 0) Debug.LogError($"Not found sounds by name {audioName}");

            return result;
        }

        public List<AudioItem> GetAudioPlayOnWake()
        {
            var result = new List<AudioItem>();
            foreach (var item in listItem)
            {
                if (item.PlayOnAwake) result.Add(item);
            }

            return result;
        }

        public List <AudioItem> GetAudiosByType(AudioType type)
        {
            var result = new List<AudioItem>();
            foreach (var item in listItem)
            {
                if (item.Type == type) result.Add(item);
            }
            return result;
        }
    }
}