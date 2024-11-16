using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBasement : MonoBehaviour
{
    public GameObject[] newObjects;
    public GameObject[] oldObjects;
    private bool poraoChecked;
    // Start is called before the first frame update
    void Start()
    {
        poraoChecked = false;
    }

    public void MakeDinner()
    {
        if (poraoChecked)
        {
            FindObjectOfType<MissionManager>().SetMissionIndex(3);
            for (int i = 0; i < newObjects.Length; i++)
            {
                newObjects[i].SetActive(true);
            }
            for (int i = 0; i < oldObjects.Length; i++)
            {
                oldObjects[i].SetActive(false);
            }
        }
    }

    public void SetPoraoCheckTrue()
    {
        this.poraoChecked = true;
        MakeDinner();
    }
}
