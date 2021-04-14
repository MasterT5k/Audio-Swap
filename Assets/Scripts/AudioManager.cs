using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _audioMixer = null;
    [SerializeField]
    private AudioSource _music01Source = null, _music02Source = null, _music03Source = null, _sFXSource, _waterSource, _windSource;
    [SerializeField]
    private bool _mainMenu = false;

    private enum MusicType
    {
        Air,
        Land,
        Water,
        Null
    }
    private MusicType _currentMusic = MusicType.Null;
    private AudioSource _sourceCurrent;

    private void OnEnable()
    {
        Player.onMusicChange += Music;
    }

    private void OnDisable()
    {
        Player.onMusicChange -= Music;
    }

    private void Start()
    {
        if (_mainMenu == false)
        {
            _music01Source.volume = 0;
            _music02Source.volume = 0;
            _music03Source.volume = 0;
        }

        float masterVolume = SaveSystem.GetSetting("MasterVolume");
        float musicVolume = SaveSystem.GetSetting("MusicVolume");
        float sFXVolume = SaveSystem.GetSetting("SFXVolume");
        UIManager.Instance.SetVolumeSliders(masterVolume, musicVolume, sFXVolume);
    }

    private void Music(int type)
    {
        MusicType nextMusic = (MusicType)type;

        if (_sourceCurrent == null)
        {
            switch (nextMusic)
            {
                case MusicType.Air:
                    _sourceCurrent = _music01Source;
                    break;
                case MusicType.Land:
                    _sourceCurrent = _music02Source;
                    break;
                case MusicType.Water:
                    _sourceCurrent = _music03Source;
                    break;
                default:
                    Debug.LogError("MusicType is out of range.");
                    break;
            }
            _sourceCurrent.volume = 1;
            _sourceCurrent.Play();
            _currentMusic = nextMusic;
            return;
        }

        if (_currentMusic != nextMusic)
        {
            StartCoroutine(MusicCrossfade(nextMusic));
            _currentMusic = nextMusic;
        }
    }

    IEnumerator MusicCrossfade(MusicType musicToPlay)
    {
        AudioSource sourceNext = null;

        switch (musicToPlay)
        {
            case MusicType.Air:
                sourceNext = _music01Source;
                break;
            case MusicType.Land:
                sourceNext = _music02Source;
                break;
            case MusicType.Water:
                sourceNext = _music03Source;
                break;
            default:
                Debug.LogError("MusicType is out of range.");
                break;
        }

        if (sourceNext != null)
        {
            sourceNext.Play();
            while (sourceNext.volume < 1)
            {
                yield return new WaitForSeconds(0.1f);
                sourceNext.volume += 0.1f;
                _sourceCurrent.volume -= 0.1f;
            }
            _sourceCurrent.Stop();
            _sourceCurrent = sourceNext;
        }
        else
        {
            Debug.LogError("SourceNext is NULL");
            yield return null;
        }
    }

    public void SetMasterVolume(float sliderValue)
    {
        _audioMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
        SaveSystem.SaveSetting("MasterVolume", sliderValue);
    }

    public void SetMusicVolume(float sliderValue)
    {
        _audioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        SaveSystem.SaveSetting("MusicVolume", sliderValue);
    }

    public void SetSFXVolume(float sliderValue)
    {
        _audioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        SaveSystem.SaveSetting("SFXVolume", sliderValue);
    }
}
