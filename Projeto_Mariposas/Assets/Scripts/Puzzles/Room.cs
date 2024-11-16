using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private Rooms room;
    [SerializeField] private bool done;

    private void Start()
    {
        done = false;
        if(room == Rooms.OUTSIDE) { done = true; }
    }

    public Rooms GetRoom()
    {
        return this.room;
    }

    public bool GetDone()
    {
        return this.done;
    }

    public void SetDone(bool done)
    {
        this.done = done;
    }
}

public enum Rooms
{
    OUTSIDE,
    LIVINGROOM,
    OFFICE,
    BATHROOM,
    KITCHEN,
    BEDROOM,
    BABYROOM
}
