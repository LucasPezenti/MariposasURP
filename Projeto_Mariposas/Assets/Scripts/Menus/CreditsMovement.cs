using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMovement : MonoBehaviour
{

    public int velocidade = 50;
    public Transform startPoint;


    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 4000)
        {
            StartCredits();
        }
        transform.Translate(0, Time.deltaTime * velocidade, 0);        
    }

    public void StartCredits()
    {
        transform.position = startPoint.position;
    }
}
