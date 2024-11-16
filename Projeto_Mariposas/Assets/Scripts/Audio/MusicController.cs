using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    private AudioManager audioManager;
    public bool startWithAudio;
    public string musicName;
    public string triggerName;
    public bool stopAudios;

    void Start()
    {
        audioManager = AudioManager.GetAudioInstance();
        if (stopAudios)
        {
            audioManager.StopAll();
        }
        if (startWithAudio)
        {
            audioManager.Play(musicName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!audioManager.GetAudio(triggerName).source.isPlaying)
            {
                audioManager.Play(triggerName);
            }
            
            audioManager.GetAudio(musicName).source.volume = .3f;
        }
    }


}
