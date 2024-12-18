using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject textObj;
    [SerializeField] private KeyCode closeKey;
    [SerializeField] private BoxCollider2D triggerObj;
    [SerializeField] private bool isOpen;

    private void Start()
    {
        isOpen = false;
    }
    private void Update()
    {
        if (isOpen && Input.GetKeyDown(closeKey))
        {
            CloseTutorial();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Display tutorial  
            OpenTutorial();
        }
    }

    public void OpenTutorial()
    {
        animator.SetTrigger("Open");
        triggerObj.enabled = false;
        isOpen = true;
    }

    public void CloseTutorial()
    {
        animator.SetTrigger("Close");
    }

    public void ShowMissionText()
    {
        textObj.SetActive(true);
    }

    public void HideMissionText()
    {
        textObj.SetActive(false);
    }

    public Animator GetAnimator()
    {
        return this.animator;
    }

    public void SetAnimOver()
    {
        animator.SetTrigger("AnimationOver");
    }

    public bool GetIsOpen()
    {
        return this.isOpen;
    }
}
