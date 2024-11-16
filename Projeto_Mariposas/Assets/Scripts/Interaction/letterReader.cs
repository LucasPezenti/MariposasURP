using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class letterReader : MonoBehaviour
{
    public GameObject[] texts;
    public UIElement[] letterLines;
    GameManager gameManager;

    public TextMeshProUGUI messageText;

    private string idInicial;
    private string idFinal;

    public GameObject letterArea;

    private int activeMessage = 0;
    public static bool readingLetter = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && readingLetter == true)
        {
            NextLetterPager();
        }
    }



    public void LoadLetterText()
    {
        if (System.IO.File.Exists(gameManager.GetUIFilePath()))
        {
            activeMessage = 0;
            string[] lines = System.IO.File.ReadAllLines(gameManager.GetUIFilePath());
            foreach (string line in lines)
            {
                string[] info = line.Split('/');
                for (int i = 0; i < letterLines.Length; i++)
                {

                    if (line.Contains(letterLines[i].textID))
                    {
                        letterLines[i].textToDisplay.text = info[1].Trim();
                    }
                }
            }
        }
        DisplayLetter();
    }

    public void DisplayLetter()
    {
        for (int i = 0; i < letterLines.Length; i++)
        {
            texts[i].SetActive(false);
        }
        readingLetter = true;
        texts[activeMessage].SetActive(true);
        letterArea.SetActive(true);
    }

    public void NextLetterPager()
    {
        activeMessage++;
        if (activeMessage < letterLines.Length)
        {
            DisplayLetter();
        }
        else
        {
            readingLetter = false;
            letterArea.SetActive(false);
            activeMessage = 0;
            FindObjectOfType<ChangeLevel>().FadeToLevel(1);
        }
    }
}
