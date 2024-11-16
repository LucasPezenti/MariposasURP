using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public UIElement[] elements;

    private void Start()
    {
        LoadUI();
    }

    public void LoadUI()
    {
        if (System.IO.File.Exists(GameManager.GetInstance().GetUIFilePath()))
        {
            string[] lines = System.IO.File.ReadAllLines(GameManager.GetInstance().GetUIFilePath());
            foreach (string line in lines)
            {
                string[] info = line.Split('/');
                for(int i = 0; i < elements.Length; i++)
                {
                    if (line.Contains(elements[i].textID))
                    {
                        elements[i].textToDisplay.text = info[1].Trim();
                    }
                }
            }
        }
    }
}

[System.Serializable]
public class UIElement
{
    public string textID;
    public TextMeshProUGUI textToDisplay;
}