using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] public string itemName { get; set; }

    public abstract void Interaction();
}
