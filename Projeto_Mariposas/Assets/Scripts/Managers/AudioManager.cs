using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    [SerializeField] private bool mute;
    [SerializeField] private Button muteButton;

    public static AudioManager audioInstance;

    public static AudioManager GetAudioInstance()
    {
        return audioInstance;
    }

    void Awake()
    {
        if(audioInstance == null)
        {
            audioInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        //muteButton.onClick.AddListener(SwitchMute);
    }

    private void Start()
    {
        mute = false;
        /*
        foreach (Sound s in sounds)
        {
            s.source.volume = 0.5f;
        }
        */
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) 
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return; 
        }
        if (!mute) { s.source.Play(); }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

    public void SwitchMute()
    {
        if (mute)
        {
            foreach (Sound s in sounds)
            {
                if(s.type == SoundType.MUSIC) { s.source.volume = 0.3f; }

                else if(s.type == SoundType.SFX) { s.source.volume = 0.15f; }
            }
            mute = false;
        }

        else
        {
            foreach (Sound s in sounds)
            {
                s.source.volume = 0f;
            }
            mute = true;
        }
    }

    public void TurnAudioOff()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = 0f;
        }
        mute = true;
    }

    public void TurnAudioOn()
    {
        foreach (Sound s in sounds)
        {
            if (s.type == SoundType.MUSIC) { s.source.volume = 0.3f; }

            else if (s.type == SoundType.SFX) { s.source.volume = 0.15f; }
        }
        mute = false;
    }

    public Sound GetAudio(string name)
    { 
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s;
    }

    public bool GetMute()
    {
        return this.mute;
    }

    public void SetMute(bool mute)
    {
        this.mute = mute;
    }
}

[System.Serializable]
public class Sound
{

    public string name;
    public AudioClip clip;
    public SoundType type;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

