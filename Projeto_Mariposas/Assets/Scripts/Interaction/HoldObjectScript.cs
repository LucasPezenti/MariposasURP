using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObjectScript : MonoBehaviour
{
    public GameObject ExamineAlert;

    [SerializeField] private Transform holdSpot;
    [SerializeField] private LayerMask pickUpMask;
    private Vector3 pickDirection { get; set; }
    public GameObject itemHolding;
    private TopDownMovement PlayerMovement;

    [SerializeField] private bool isHolding;

    private void Awake()
    {
        PlayerMovement = GetComponent<TopDownMovement>();
    }


    // Start is called before the first frame update
    void Start()
    {
        itemHolding = null;
        isHolding = false;
        pickDirection = new Vector2(0,0);
    }

    void FixedUpdate(){
        ChangePosition();
    }

    private void ChangePosition(){
        if (PlayerMovement.GetMoveDirection().sqrMagnitude > .1f)
        {
            pickDirection = PlayerMovement.GetMoveDirection().normalized;
        }

        if (PlayerMovement.dir == Direction.RIGHT){
            holdSpot.transform.localPosition = new Vector2(0.22f, 0.58f);
        }
        else if(PlayerMovement.dir == Direction.LEFT){
            holdSpot.transform.localPosition = new Vector2(-0.22f, 0.58f);
        }
        if(PlayerMovement.dir == Direction.UP){
            holdSpot.transform.localPosition = new Vector2(0, 0.83f);
        }
        else if(PlayerMovement.dir == Direction.DOWN){
            holdSpot.transform.localPosition = new Vector2(0, 0.53f);       
        }
    }

    public void PickUp(GameObject pickUpItem){
        itemHolding = pickUpItem.gameObject;
        itemHolding.transform.position = holdSpot.position;
        itemHolding.transform.parent = holdSpot.transform;
        itemHolding.GetComponent<SpriteRenderer>().enabled = false;
        if (itemHolding.GetComponent<Rigidbody2D>())
        {
            itemHolding.GetComponent<Rigidbody2D>().simulated = false;
            //Debug.Log("Item grabbed");
        }
    }

    public void Release(){
        if(itemHolding != null)
        {
            itemHolding.transform.position = transform.position + pickDirection * .4f;
            itemHolding.transform.parent = null;
            itemHolding.GetComponent<SpriteRenderer>().enabled = true;
            if (itemHolding.GetComponent<Rigidbody2D>())
            {
                itemHolding.GetComponent<Rigidbody2D>().simulated = true;
            }
            itemHolding = null;
            //Debug.Log("Item released");
        }

    }

    public bool GetIsHolding(){
        return this.isHolding;
    }
}
