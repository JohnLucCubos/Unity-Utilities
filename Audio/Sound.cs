using UnityEngine;

namespace MyUnity.Utilities
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public SoundType type;
        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;
        [Range(0.1f, 3f)]
        public float pitch;

        public bool playOnStart;

        [HideInInspector]
        public AudioSource source;
    }

    [System.Serializable]
    public enum SoundType
    {
        BGM,
        SFX
    }
}