using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePillsFinish : MonoBehaviour
{
    public GameObject[] newObjects;
    public GameObject[] oldObjects;
    public bool pillsTaken;
    // Start is called before the first frame update
    void Start()
    {
        pillsTaken = false;
    }

    public void FinishPillsQuest()
    {
        if (pillsTaken)
        {
            FindObjectOfType<MissionManager>().SetMissionIndex(2);
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

    public void PillsTaken()
    {
        this.pillsTaken = true;
        FinishPillsQuest();
    }
}
