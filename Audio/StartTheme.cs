using MyUnity.Utilities;
using UnityEngine;

public class StartTheme : MonoBehaviour
{
    [SerializeField] Sound _sound;

    void Start() => PlayTheme();
    private void PlayTheme(){
        if(_sound.clip == null) return;
        _sound.source = this.gameObject.AddComponent<AudioSource>();
        _sound.source.clip = _sound.clip;
        _sound.source.volume = _sound.volume;
        _sound.source.pitch = _sound.pitch;
        _sound.source.loop = true;

        _sound.source.Play();
    }
}
