using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    Animator animator;
    private string curState;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState){

        // Stop an animaiton from interrupting itself
        if(curState == newState){
            return;
        }

        animator.Play(newState);
        curState = newState;

    }
}
