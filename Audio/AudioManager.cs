using UnityEngine;
using System;

namespace MyUnity.Utilities
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;
        public static AudioManager Instance { get { return _instance; } }
        [Header("Sound List")]
        [SerializeField] Sound[] _sounds;
        public Sound[] getSounds { get { return _sounds; }}
        [SerializeField] Sound _currentBGM;
        public Sound currentBGM { set { _currentBGM = value; } get { return _currentBGM; } }


        private void Awake()
        {
            this.gameObject.tag = "AudioManager";
            // create singleton
            if (_instance != null && _instance != this)
                Destroy(this.gameObject);
            else
                _instance = this;

            DontDestroyOnLoad(this.gameObject);

            CreateAudioList();
        }

        private void CreateAudioList(){
            foreach (Sound eachSound in _sounds)
            {
                eachSound.source = this.gameObject.AddComponent<AudioSource>();
                eachSound.source.clip = eachSound.clip;

                eachSound.source.volume = eachSound.volume;
                eachSound.source.pitch = eachSound.pitch;

                if (eachSound.type == SoundType.BGM)
                    Loop(eachSound.name);

                if (eachSound.playOnStart)
                {
                    Play(eachSound.name);
                    _currentBGM = eachSound;
                }
            }
        }
        /// <summary>
        /// Enables an audio to play sound
        /// </summary>
        /// <param name="name">sound used</param>
        public void Play(string name)
        {
            Sound _sound = FindSound(name);

            if (_sound == null)
            {
                Debug.LogError($"{name} does not exist");
                return;
            }

            if (_sound.type == SoundType.BGM)
            {
                Stop(_currentBGM.name);
                _currentBGM = _sound;
            }

            _sound.source.Play();
        }
        /// <summary>
        /// Disables an audio to play sound
        /// </summary>
        /// <param name="name">sound used</param>
        public void Stop(string name)
        {
            Sound _sound = FindSound(name);

            if (_sound == null) return;
            _sound.source.Stop();
        }
        /// <summary>
        /// Enables an audio to repeat
        /// </summary>
        /// <param name="name">sound used</param>
        public void Loop(string name)
        {
            Sound _sound = FindSound(name);

            if (_sound == null) return;

            _sound.source.loop = true;
        }
        /// <summary>
        /// Changes the volume of the sound given
        /// </summary>
        /// <param name="name">sound used</param>
        /// <param name="value">amount in which the audio changes</param>
        public void ChangeVolume(string name, float value)
        {
            Sound _sound = FindSound(name);
            if (_sound == null) return;

            _sound.source.volume = Mathf.Clamp(value, 0f, 1f);
        }
        /// <summary>
        /// Changes the pitch of the sound given |
        /// Example use cases:
        /// - Randomizing sfx to prevent audio fatigue
        /// - making a song creepy by slowing it down
        /// </summary>
        /// <param name="name">sound used</param>
        /// <param name="value">amount in which the audio changes</param>
        public void ChangePitch(string name, float value)
        {
            Sound _sound = FindSound(name);
            if (_sound == null) return;

            _sound.source.pitch = Mathf.Clamp(value, 0f, 3f);
        }
        /// <summary>
        /// Finds the sound if it exists in the Audio system; else return null
        /// </summary>
        /// <param name="name">sound used</param>
        private Sound FindSound(string name)
        {
            if (string.IsNullOrEmpty(name)) return null;

            Sound foundSound = Array.Find(_sounds, sound => sound.name == name);

            return foundSound;
        }
    }
}
