using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundMixerCtrl : MonoBehaviour
{    
    ////Saving Sound Settings

    //public AudioMixer _mixer;
    //public Slider _bgmSlider;
    //public Slider _sfxSlider;

    //public const string _MIXER_BGM = "BGMVolume";
    //public const string _MIXER_SFX = "SFXVolume";

    //public AudioManager _audioManager;
    //private void Awake()
    //{
    //    //_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    //    //_bgmSlider.onValueChanged.AddListener(SetBGMVolume);
    //    //_sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    //}

    //private void OnEnable()
    //{
    //    //if (_bgmSlider.isActiveAndEnabled != false)
    //    //{
    //        //_bgmSlider.onValueChanged.AddListener(SetBGMVolume);
    //        //_sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    //    //}
    //}
    //private void Update()
    //{
    //    if (_bgmSlider.isActiveAndEnabled != false)
    //    {
    //        _bgmSlider.onValueChanged.AddListener(SetBGMVolume);
    //        _sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    //    }
    //}

    //private void Start()
    //{
    //    //_audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    //    _bgmSlider.value = PlayerPrefs.GetFloat(AudioManager._BGM_KEY, 0.5f);
    //    _sfxSlider.value = PlayerPrefs.GetFloat(AudioManager._SFX_KEY, 0.5f);
    //}
    //private void OnDisable()
    //{
    //    PlayerPrefs.SetFloat(AudioManager._BGM_KEY, _bgmSlider.value);
    //    PlayerPrefs.SetFloat(AudioManager._SFX_KEY, _sfxSlider.value);
    //    //PlayerPrefs.SetFloat(_audioManager._BGM_KEY, _bgmSlider.value);
    //    //PlayerPrefs.SetFloat(_audioManager._SFX_KEY, _sfxSlider.value);
    //}
    //private void SetBGMVolume(float value)
    //{
    //    _mixer.SetFloat(_MIXER_BGM, Mathf.Log10(value) * 20);
    //}

    //private void SetSFXVolume(float value)
    //{
    //    _mixer.SetFloat(_MIXER_SFX, Mathf.Log10(value) * 20);
    //}
}
