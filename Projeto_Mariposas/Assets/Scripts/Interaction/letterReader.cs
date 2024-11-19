using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterReader : MonoBehaviour
{
    private GameManager gameManager;

    public TextMeshProUGUI messageText;

    [SerializeField] private int piecesLeft;
    [SerializeField] private bool lastPage;

    public GameObject letterArea;

    private int activeMessage = 0;
    public static bool readingLetter = false;

    private List<Piece> pieceList = new List<Piece>();
    Piece[] curPiece;
    private const string idPiece = "LetterID";

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.GetInstance();
        lastPage = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && readingLetter == true)
        {
            NextLetterPager();
        }
    }

    public void LoadLetterText(string idInicial, string idFinal, bool isLastPage)
    {
        if (System.IO.File.Exists(gameManager.GetUIFilePath()))
        {
            lastPage = isLastPage;
            activeMessage = 0;
            bool startReading = false;

            string[] lines = System.IO.File.ReadAllLines(gameManager.GetUIFilePath());
            foreach (string line in lines)
            {
                if (line.Contains(idInicial))
                {
                    startReading = true;
                    continue;
                }

                else if (line.Contains(idFinal))
                {
                    break;
                }

                if (startReading)
                {
                    string[] info = line.Split('/');
                    if (line.Contains(idPiece))
                    {
                        pieceList.Add(new Piece
                        {
                            message = info[1].Trim()
                        });       
                    }
                }
            }
            DisplayLetter();
        }
        else
        {
            Debug.Log("O arquivo não foi encontrado.");
        }
    }

    public void DisplayLetter()
    {
        Piece pieceToDisplay = pieceList[activeMessage];
        messageText.text = pieceToDisplay.message;
        readingLetter = true;
        letterArea.SetActive(true);
    }

    public void NextLetterPager()
    {
        activeMessage++;
        if (activeMessage < pieceList.Count)
        {
            DisplayLetter();
        }
        else
        {
            readingLetter = false;
            letterArea.SetActive(false);
            activeMessage = 0;
            piecesLeft--;
            pieceList.Clear();
            if (lastPage)
            {
                FindObjectOfType<ChangeLevel>().FadeToLevel(1);
            }
            
        }
    }

    public int GetPiecesLeft()
    {
        return this.piecesLeft;
    }
}
