using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterPiece : MonoBehaviour
{
    [SerializeField] private LetterReader reader;
    [SerializeField] private int numOfMessages;
    [SerializeField] private GameObject letterObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLetterPieceActive(bool isActive)
    {
        letterObj.SetActive(isActive);
    }
}
