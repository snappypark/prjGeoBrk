using UnityEngine;

namespace nj
{ 
public class AppAudio : MonoSingleton<AppAudio>
{
    public enum eMusicType
    {
        outgame = 0,
        ingame,
    }

    public enum eSoundType
    {
        hit = 0,
    }

    [SerializeField] AudioClip[] _musics;
    [SerializeField] AudioSource _musicSource;

    [SerializeField] AudioClip[] _sounds;
    [SerializeField] AudioSource[] _soundSources;
    int _soundIdx = 0;

    bool _mute = false;
    public bool IsMute { get { return _mute; } }
    public void Mute()
    {
        _mute = true;
        for (int i = 0; i < _soundSources.Length; ++i)
            _soundSources[i].mute = true;
    }
    public void UnMute()
    {
        _mute = false;
        for (int i = 0; i < _soundSources.Length; ++i)
            _soundSources[i].mute = false;
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PlayMusic(eMusicType type)
    {
        return;
        //_musicSource.clip = _musics[(int)type];
      //  _musicSource.loop = true;
      //  _musicSource.Play();
    }

    const float de = 0.043f;

    delay _delaySound0 = new delay(de);
    delay[] _delaySound = new delay[] {
        new delay(de), new delay(de), new delay(de), new delay(de),
        new delay(de), new delay(de), new delay(de), new delay(de),
        new delay(de), new delay(de), new delay(de), new delay(de),
    };
    public void PlaySound(eSoundType type)
    {
        if (!_delaySound0.IsEnd())
            return;
        _delaySound0.Reset();
        _soundSources[_soundIdx].clip = _sounds[(int)type];
       //     _soundSources[_soundIdx].pitch = pitch_;
        _soundSources[_soundIdx].Play();
        
        ++_soundIdx;
        _soundIdx = _soundIdx % _soundSources.Length;
    }
    // TODO: fade in out, mute
}
}