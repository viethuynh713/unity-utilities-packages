using UnityEngine;

namespace VPackage.AudioSystem
{
    public interface IAudioManager
    {
        public void PlaySFX(AudioName audioName);
        void PlaySoundFromSource(AudioName audioName, AudioSource audioSource, bool isChangeSound = false);
        void PlayMusic(AudioName audioName);
        void MuteMusic();
        void MuteSfx();
        void StopMusic(AudioName audioName);
        void StopMusic(AudioType type);
        void PauseMusic(AudioName audioName);
        void UnPauseMusic(AudioName audioName);
    }
}