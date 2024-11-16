using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsReader : MonoBehaviour
{
    public UIElement[] creditLines;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.GetInstance();
        LoadCredits();
    }

    public void LoadCredits()
    {
        if (System.IO.File.Exists(gameManager.GetCreditsFilePath()))
        {
            string[] lines = System.IO.File.ReadAllLines(gameManager.GetCreditsFilePath());
            foreach (string line in lines)
            {
                string[] info = line.Split(';');
                for (int i = 0; i < creditLines.Length; i++)
                {
                    if (line.Contains(creditLines[i].textID))
                    {
                        creditLines[i].textToDisplay.text = info[1].Trim();
                    }
                }
            }
        }
    }
}
