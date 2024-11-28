using UnityEditor;
using UnityEngine;

namespace VPackage.AudioSystem
{
	public class AudioManagerMonoBehavior : MonoBehaviour, IAudioManager
	{
		#region VARIABLES
		public static AudioManagerMonoBehavior Instance;
		private const float TIME_TO_CHECK_IDLE_AUDIO_SOURCE = 5f;

		[SerializeField] private AudioData database;

		private bool _isMuteMusic;

		private bool _isMuteSfx;

		#endregion

		#region UNITY METHOD
		private void GetAudioSetting()
		{
			if (PlayerPrefs.HasKey("MuteMusic"))
			{
				_isMuteMusic = PlayerPrefs.GetInt("MuteMusic") == 1 ? true : false;
			}
			else
			{
				_isMuteMusic = false;
				PlayerPrefs.SetInt("MuteMusic", 0);
			}

			if (PlayerPrefs.HasKey("MuteSFX"))
			{
				_isMuteSfx = PlayerPrefs.GetInt("MuteSFX") == 1 ? true : false;
			}
			else
			{
				_isMuteSfx = false;
				PlayerPrefs.SetInt("MuteSFX", 0);

            }
		}
		public void Start()
		{
			GetAudioSetting();
			//GetAudioData();
			foreach (var s in database.GetAudioPlayOnWake())
			{
				s.Source = CreateAudioSource(s);
				if (IsMuteAudio(s.Type))
				{
					s.Source.mute = _isMuteMusic;
				}

				s.Source.Play();
				
			}
		}

		private void GetAudioData()
		{
			string path = "AudioData/database";
			if (database == null)
			{
				// Load the database.asset from the Resources folder
				database = Resources.Load<AudioData>(path);
			}
    
		}


		#endregion

		#region CLASS METHODS

		private AudioSource CreateAudioSource(AudioItem a)
		{
			AudioSource s = gameObject.AddComponent<AudioSource>();
			s.clip = a.AudioClip;
			s.volume = a.Volume;
			s.playOnAwake = a.PlayOnAwake;
			s.priority = a.Priority;
			s.loop = a.IsLooping;
			return s;
		}

		private bool IsMuteAudio(AudioType type)
		{
			if (type == AudioType.BGM && _isMuteMusic) return true;
			if (type == AudioType.SFX && _isMuteSfx) return true;

			return false;
		}

		public void PlaySFX(AudioName audioName)
		{
			if (_isMuteSfx || audioName == AudioName.None) return;

			AudioItem audioItem = database.GetAudioByName(audioName);
			if (audioItem == null) return;
			else
			{
				if (audioItem.Source == null)
				{
					audioItem.Source = CreateAudioSource(audioItem);
				}

				audioItem.Source.PlayOneShot(audioItem.AudioClip, audioItem.Volume);
			}
		}

		public void PlaySoundFromSource(AudioName audioName, AudioSource audioSource, bool isChangeSound = false)
		{
			if (_isMuteSfx || audioName == AudioName.None) return;

			if (audioSource == null) return;

			if (audioSource.clip == null || isChangeSound)
			{
				AudioItem audioItem = database.GetAudioByName(audioName);
				if (audioItem == null) return;

				if (audioSource.clip == null)
				{
					audioSource.clip = audioItem.AudioClip;
					audioSource.volume = audioItem.Volume;
					audioSource.priority = audioItem.Priority;
					audioSource.playOnAwake = audioItem.PlayOnAwake;
					audioSource.spatialBlend = audioItem.SpatialBlend;
					audioSource.rolloffMode = AudioRolloffMode.Linear;
					audioSource.minDistance = 1;
					audioSource.maxDistance = 50;
				}
			}

			audioSource.PlayOneShot(audioSource.clip, audioSource.volume);

		}


		public void PlayMusic(AudioName audioName)
		{
			if (_isMuteMusic || audioName == AudioName.None) return;

			AudioItem audioItem = database.GetAudioByName(audioName);
			if (audioItem == null)return;
			
			if (audioItem.Source == null)
			{
                audioItem.Source = CreateAudioSource(audioItem);
			}

			if (!audioItem.Source.isPlaying)
			{
				audioItem.Source.Play();
			}
		}

		public void MuteMusic()
		{
			if (PlayerPrefs.HasKey("MuteMusic"))
			{
				_isMuteMusic = PlayerPrefs.GetInt("MuteMusic") == 1 ? true : false;
				foreach (var item in database.GetAudiosByType(AudioType.BGM))
				{
					if (item.Source == null)
					{
						continue;
					}
					item.Source.mute = _isMuteMusic;
				}
					
			}
		}

		public void MuteSfx()
		{
			if (PlayerPrefs.HasKey("MuteSFX"))
			{
				_isMuteSfx = PlayerPrefs.GetInt("MuteSFX") == 1 ? true : false;
                foreach (var item in database.GetAudiosByType(AudioType.SFX))
                {
                    if (item.Source == null)
                    {
                        continue;
                    }
                    item.Source.mute = _isMuteMusic;
                }
            
			}
		}

		public void StopMusic(AudioName audioName)
		{
			AudioItem s = database.GetAudioByName(audioName);
			if (s == null) return;
			else
			{
				if (s.Source != null)
				{
					s.Source.Stop();
				}
			}
		}
		public void StopMusic(AudioType type)
		{
			foreach (var s in database.GetAudiosByType(type))
			{
				if (s == null) continue;
				else
				{
					if (s.Source != null)
					{
						s.Source.Stop();
					}
				}
			}
		}
        public void PauseMusic(AudioName audioName)
		{
			AudioItem s = database.GetAudioByName(audioName);
			if (s == null) return;
			else
			{
				if (s.Source != null)
				{
					s.Source.Pause();
				}
			}
		}

		public void UnPauseMusic(AudioName audioName)
		{
            AudioItem s = database.GetAudioByName(audioName);
            if (s == null) return;
            else
            {
                if (s.Source != null)
                {
                    s.Source.UnPause();
                }
            }
        }

		


		#endregion
	}
}