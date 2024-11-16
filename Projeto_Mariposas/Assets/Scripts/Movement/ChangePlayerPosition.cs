using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerPosition : MonoBehaviour
{
    private GameObject curTeleport;

    public void ChangeToArea()
    {
        if(curTeleport != null)
        {
            transform.position = curTeleport.GetComponent<ChangeArea>().GetDestination().position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleport"))
        {
            curTeleport = collision.gameObject;
            ChangeToArea();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleport"))
        {
            if(collision.gameObject == curTeleport)
            {
                curTeleport = null;
            }
        }
    }
}
