using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWall : MonoBehaviour
{

    public GameObject wall;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            wall.SetActive(true);
        }
    }
}
