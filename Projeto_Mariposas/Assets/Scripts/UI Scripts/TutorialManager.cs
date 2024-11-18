using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private UIElement[] tutorialList;

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
                for(int i = 0; i < tutorialList.Length; i++)
                {
                    if (line.Contains(tutorialList[i].textID))
                    {
                        tutorialList[i].textToDisplay.text = info[1].Trim();
                    }
                }
            }
        }
    }
}
