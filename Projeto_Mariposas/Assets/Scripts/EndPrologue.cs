using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndPrologue : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    public void PlayCutscene(){
        director.Play();
    }
    
}
