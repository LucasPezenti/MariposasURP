using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothSwarmScript : MonoBehaviour
{
    private Animator animator;
    private string curState;
    public bool moveR;

    //Small Moths animations
    const string MOTH_SWARM_R = "MothSwarm";
    const string MOTH_SWARM_L = "MothSwarmL";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateSmallMoths();
    }

    public void AnimateSmallMoths()
    {
        if (moveR)
        {
            ChangeAnimationState(MOTH_SWARM_R);
        }
        else
        {
            ChangeAnimationState(MOTH_SWARM_L);
        }
    }

    public void ChangeAnimationState(string newState)
    {
        // Stop an animaiton from interrupting itself
        if (curState == newState)
        {
            return;
        }

        animator.Play(newState);
        curState = newState;
    }
}
