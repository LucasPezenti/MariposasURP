using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleBox : Item
{
    [SerializeField] private Rooms boxRoom; //{ get; set; }
    [SerializeField] private bool onGround;
    [SerializeField] public Sprite boxImage;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Start()
    {
        onGround = true;
    }

    public override void Interaction()
    {
        throw new System.NotImplementedException();
    }

    public Rooms GetBoxRoom()
    {
        return this.boxRoom;
    }
    public bool GetOnGround()
    {
        return this.onGround;
    }

    public Sprite GetBoxImage()
    {
        return this.boxImage;
    }
    public void SetOnGround(bool onGround)
    {
        this.onGround = onGround;
    }

}
