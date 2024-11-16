using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutReader : MonoBehaviour
{
    public UIElement[] aboutLines;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameManager.GetInstance();
    }

    public void LoadAboutText()
    {
        if (System.IO.File.Exists(gameManager.GetAboutFilePath()))
        {
            string[] lines = System.IO.File.ReadAllLines(gameManager.GetAboutFilePath());
            foreach (string line in lines)
            {
                string[] info = line.Split('/');
                for (int i = 0; i < aboutLines.Length; i++)
                {
                    if (line.Contains(aboutLines[i].textID))
                    {
                        aboutLines[i].textToDisplay.text = info[1].Trim();
                    }
                }
            }
        }
    }
}
