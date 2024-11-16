using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFaseArea : MonoBehaviour
{
    public int nextLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<ChangeLevel>().FadeToLevel(nextLevel);
        }
        
    }
}
