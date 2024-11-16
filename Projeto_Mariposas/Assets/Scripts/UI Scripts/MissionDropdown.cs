using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionDropdown : MonoBehaviour
{
    private AudioManager audioManager;

    [SerializeField] private Animator animator;
    [SerializeField] private GameObject missionText;

    private void Start()
    {
        audioManager = AudioManager.GetAudioInstance();
    }

    public void OpenDropdown()
    {
        animator.SetTrigger("OpenTab");
        audioManager.Play("ButtonFX01");
    }

    public void CloseDropdown()
    {
        animator.SetTrigger("CloseTab");
        audioManager.Play("ButtonFX01");
    }

    public void ShowMissionText()
    {
        missionText.SetActive(true);
    }

    public void HideMissionText()
    {
        missionText.SetActive(false);
    }

    public Animator GetAnimator()
    {
        return this.animator;
    }

    public void SetAnimOver(bool isOver)
    {
        animator.SetBool("AnimIsOver", isOver);
    }
}
