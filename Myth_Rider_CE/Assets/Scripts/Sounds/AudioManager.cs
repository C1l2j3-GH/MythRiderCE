using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //Loading Sound Settings
    [NonReorderable]
    public Sound[] BGM;
    [NonReorderable]
    public Sound[] SoundEffects;
    public static AudioManager amInstance;

    //KS
    [Header("KS Addition")]
    [Tooltip("KS Addition")]
    public AudioMixerGroup _bgmMixer;
    public AudioMixerGroup _sfxMixer;

    public const string _BGM_KEY = "bgmVolume";
    public const string _SFX_KEY = "sfxVolume";

    public const string _MIXER_BGM = "BGMVolume";
    public const string _MIXER_SFX = "SFXVolume";

    [SerializeField] AudioMixer _mixer;
    public Slider _bgmSlider;
    public Slider _sfxSlider;
    public Slider _bgmSliderMM;
    public Slider _sfxSliderMM;

    public float _bgmSliderValue;
    public float _sfxSliderValue;

    void Awake()
    {
        if (amInstance == null)
        {
            amInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        foreach (Sound s in BGM)
        {
            s.src = gameObject.AddComponent<AudioSource>();
            s.src.clip = s.clip;
            s.src.volume = s.volume;
            s.src.pitch = s.pitch;
            s.src.outputAudioMixerGroup = _bgmMixer;
        }
        foreach (Sound s in SoundEffects)
        {
            s.src = gameObject.AddComponent<AudioSource>();
            s.src.clip = s.clip;
            s.src.volume = s.volume;
            s.src.pitch = s.pitch;
            s.src.outputAudioMixerGroup = _sfxMixer;
        }
    }
    public void PlayBGM(string name)
    {
        Sound s = Array.Find(BGM, sound => sound.name == name);
        s.src.loop = true;
        s.src.Play();
    }
    public void PlaySF(string name)
    {
        Sound s = Array.Find(SoundEffects, sound => sound.name == name);
        s.src.Play();
    }

    public void PlaySFLooped(string name)
    {
        Sound s = Array.Find(SoundEffects, sound => sound.name == name);
        s.src.loop = true;
        s.src.Play();
    }
    public void StopBGM(string name)
    {
        Sound s = Array.Find(BGM, sound => sound.name == name);
        s.src.Stop();
    }
    public void StopSF(string name)
    {
        Sound s = Array.Find(SoundEffects, sound => sound.name == name);
        s.src.Stop();
    }
    public AudioSource GetMusicSource(string name)
    {
        return Array.Find(BGM, sound => sound.name == name).src;
    }
    public void StopAllSF()
    {
        foreach (Sound s in SoundEffects)
        {
            s.src.Stop();
        }
    }
    public void StopAllBGM()
    {
        foreach (Sound s in BGM)
        {
            s.src.Stop();
        }
    }

    //KS
    private void Update()
    {
        if (Menus._currentScene.buildIndex == 1)
        {
            if (_bgmSliderMM == null && GameObject.FindGameObjectWithTag("MMBGMSlider").GetComponent<Slider>() != null)
            {
                _bgmSliderMM = GameObject.FindGameObjectWithTag("MMBGMSlider").GetComponent<Slider>();
                PlayerPrefs.SetFloat(_BGM_KEY, _bgmSliderMM.value);
                _bgmSliderMM.onValueChanged.AddListener(SetBGMVolume);
            }

            if (_sfxSliderMM == null && GameObject.FindGameObjectWithTag("MMSFXSlider").GetComponent<Slider>() != null)
            {
                _sfxSliderMM = GameObject.FindGameObjectWithTag("MMSFXSlider").GetComponent<Slider>();
                PlayerPrefs.SetFloat(_SFX_KEY, _sfxSliderMM.value);
                _sfxSliderMM.onValueChanged.AddListener(SetSFXVolume);
            }
        }
        else
        {
            if (_bgmSlider == null && GameObject.FindGameObjectWithTag("BGMSlider").GetComponent<Slider>() != null)
            {
                _bgmSlider = GameObject.FindGameObjectWithTag("BGMSlider").GetComponent<Slider>();
                PlayerPrefs.SetFloat(_BGM_KEY, _bgmSlider.value);
                _bgmSlider.onValueChanged.AddListener(SetBGMVolume);
            }

            if (_sfxSlider == null && GameObject.FindGameObjectWithTag("SFXSlider").GetComponent<Slider>() != null)
            {
                _sfxSlider = GameObject.FindGameObjectWithTag("SFXSlider").GetComponent<Slider>();
                PlayerPrefs.SetFloat(_SFX_KEY, _sfxSlider.value);
                _sfxSlider.onValueChanged.AddListener(SetSFXVolume);
            }
        }
    }

    public void LoadVolume() //Volume saved in SoundMixerCtrl.cs
    {
        if (PlayerPrefs.HasKey(_SFX_KEY))
        {
            float bgmVolume = PlayerPrefs.GetFloat(_BGM_KEY, 0.5f);
            float sfxVolume = PlayerPrefs.GetFloat(_SFX_KEY, 0.5f);

            _mixer.SetFloat(_MIXER_BGM, Mathf.Log10(bgmVolume * 20));
            _mixer.SetFloat(_MIXER_SFX, Mathf.Log10(sfxVolume * 20));
        }
        else
        {
            float bgmVolume = _bgmSliderValue;
            float sfxVolume = _sfxSliderValue;

            _mixer.SetFloat(_MIXER_BGM, Mathf.Log10(bgmVolume * 20));
            _mixer.SetFloat(_MIXER_SFX, Mathf.Log10(sfxVolume * 20));
        }

    }

    private void SetBGMVolume(float value)
    {
        _mixer.SetFloat(_MIXER_BGM, Mathf.Log10(value) * 20);
        _bgmSliderValue = Mathf.Log10(value) * 20;
    }

    private void SetSFXVolume(float value)
    {
        _mixer.SetFloat(_MIXER_SFX, Mathf.Log10(value) * 20);
        _sfxSliderValue = Mathf.Log10(value) * 20;
    }
}
