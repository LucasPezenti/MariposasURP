using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPiece : MonoBehaviour
{
    [SerializeField] private LetterReader reader;
    [SerializeField] private string[] startID;
    [SerializeField] private string[] endID;
    [SerializeField] private int pieceIndex;
    [SerializeField] private bool isLastPiece;
    [SerializeField] private GameObject letterObj;

    public void TriggerLetter()
    {
        pieceIndex = reader.GetPiecesLeft() - 1;
        reader.LoadLetterText(startID[pieceIndex], endID[pieceIndex], isLastPiece);
        letterObj.SetActive(false);
    }

    public void SetLetterPieceActive(bool isActive)
    {
        letterObj.SetActive(isActive);
    }
}

[System.Serializable]
public class Piece
{
    public string message;
}