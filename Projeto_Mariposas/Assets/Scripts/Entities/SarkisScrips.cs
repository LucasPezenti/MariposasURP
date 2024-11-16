using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarkisScrips : MonoBehaviour
{

    public int speed;

    private AudioManager audioManager;
    private Animator animator;
    private string curState;

    // Animações Sarkis
    const string SARKIS_IDLE_R = "Sarkis_IdleR";
    const string SARKIS_IDLE_L = "Sarkis_IdleL";
    const string SARKIS_IDLE_U = "Sarkis_IdleU";
    const string SARKIS_IDLE_D = "Sarkis_IdleD";

    const string SARKIS_WALK_R = "Sarkis_WalkR";
    const string SARKIS_WALK_L = "Sarkis_WalkL";
    const string SARKIS_WALK_U = "Sarkis_WalkU";
    const string SARKIS_WALK_D = "Sarkis_WalkD";

    const string SARKI_PHONE_R = "Sarkis_PhoneR";
    const string SARKIS_PHONE_L = "Sarkis_PhoneL";
    const string SARKIS_PHONE_U = "Sarkis_PhoneU";
    const string SARKIS_PHONE_D = "Sarkis_PhoneD";

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = AudioManager.GetAudioInstance();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateSarkis();
    }

    public void AnimateSarkis()
    {
        // Sarkis idle

        // Sarkis walking

        // Sarkis on the phone
    }

    public void GetPhone()
    {
        ChangeAnimationState(SARKIS_PHONE_D);
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
