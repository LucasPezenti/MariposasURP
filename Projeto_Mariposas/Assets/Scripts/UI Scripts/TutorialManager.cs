using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private UIElement[] tutorialElement;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject textObj;

    private void Start()
    {
        gameManager = GameManager.GetInstance();
        LoadTutorial();
    }

    public void LoadTutorial()
    {
        if (System.IO.File.Exists(gameManager.GetUIFilePath()))
        {
            string[] lines = System.IO.File.ReadAllLines(GameManager.GetInstance().GetUIFilePath());
            foreach (string line in lines)
            {
                string[] info = line.Split('/');
                for (int i = 0; i < tutorialElement.Length; i++)
                {
                    if (line.Contains(tutorialElement[i].textID))
                    {
                        tutorialElement[i].textToDisplay.text = info[1].Trim();
                    }
                }
            }
        }
    }

    public void DisplayTutorial(int elementIndex)
    {

    }

    public void OpenTutorial()
    {
        animator.SetTrigger("Open");
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
}
