using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPuzzleFinish : MonoBehaviour
{
    [SerializeField] private GameObject[] newObjects;
    [SerializeField] private GameObject[] oldObjects;
    [SerializeField] private int boxesLeft;
    [SerializeField] private bool boxPuzzleOver;

    [SerializeField] private MissionManager missionManager;

    void Start()
    {
        boxPuzzleOver = false;
    }

    public void FinishBoxPuzzle()
    {
        if (boxPuzzleOver)
        {
            missionManager.SetMissionIndex(1);

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

    public void DeliverBox()
    {
        boxesLeft--;
        if (boxesLeft <= 0)
        {
            boxPuzzleOver = true;
            FinishBoxPuzzle();
        }
    }
}
