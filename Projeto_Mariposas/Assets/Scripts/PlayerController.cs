using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpd;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 lastMoveDirection;

    public Animator anim;
    public Transform holdSpot;
    public LayerMask pickUpMask;
    public Vector3 pickDirection{ get; set; }
    public GameObject itemHolding;

    private bool running = false;
    private float moveX = 0;
    private float moveY = 0;
    
    void Start()
    {
        pickDirection = new Vector2(0, 0);
        DialogueManager.onDialogue = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        Animate();
    }

    // Physics Calculation
    void FixedUpdate(){
        Move();
    }
    
    private void ProcessInputs(){
        if(DialogueManager.onDialogue == true){
            moveX = 0;
            moveY = 0;
            if((moveX == 0 && moveY == 0) && moveDirection.x != 0 || moveDirection.y != 0){
            lastMoveDirection = moveDirection;
            }
            Animate();
            moveDirection.x = 0;
            moveDirection.y = 0;
            return;
        }

        if(anim.GetBool("TakingPill")){
            return;
        }
        else if(itemHolding == null && DialogueManager.onDialogue == false && Input.GetKey(KeyCode.Q)){
            moveX = 0;
            moveY = 0;
            if((moveX == 0 && moveY == 0) && moveDirection.x != 0 || moveDirection.y != 0){
            lastMoveDirection = moveDirection;
            }
            anim.SetBool("TakingPill", true);
            moveDirection.x = 0;
            moveDirection.y = 0;
            return;
        }

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        running = false;
        anim.SetBool("IsRunning", false);
        if(Input.GetKey("left shift")){
            running = true;
            anim.SetBool("IsRunning", true);
        }

        if((moveX == 0 && moveY == 0) && moveDirection.x != 0 || moveDirection.y != 0){
            lastMoveDirection = moveDirection;
        }

        moveDirection = new Vector2(moveX, moveY).normalized;

        // Pick up direction
        if(moveDirection.sqrMagnitude > .1f){
            pickDirection = moveDirection.normalized;
        }

        if(itemHolding == null && Input.GetKeyDown(KeyCode.E)){
            PickUp();
        }
        else if(itemHolding != null && DialogueManager.onDialogue == false && Input.GetKeyDown(KeyCode.E)){
            Release();
        }

    }

    private void Move(){
        rb.velocity = new Vector2(moveDirection.x * moveSpd, moveDirection.y * moveSpd);

        if(running){
            moveSpd = 3.5f;
        }
        else{
            moveSpd = 1.9f;
        }

        // Movement direction
        if(moveX > 0){  // Right
            holdSpot.transform.localPosition = new Vector2(0.22f, 0.6f);
            if(itemHolding != null) itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else if(moveX < 0){ // Left
            holdSpot.transform.localPosition = new Vector2(-0.22f, 0.6f);
            if(itemHolding != null) itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        if(moveY > 0){  // Up
            holdSpot.transform.localPosition = new Vector2(0, 0.65f);
            if(itemHolding != null) itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else if(moveY < 0){ // Down
            holdSpot.transform.localPosition = new Vector2(0, 0.55f);
            if(itemHolding != null) itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        
        
    }

    private void PickUp(){
        Collider2D pickUpItem = Physics2D.OverlapCircle(transform.position + pickDirection*.5f, .3f, pickUpMask);
        if(pickUpItem){
            itemHolding = pickUpItem.gameObject;
            itemHolding.transform.position = holdSpot.position;
            itemHolding.transform.parent = holdSpot.transform;
            if(itemHolding.GetComponent<Rigidbody2D>()){
                itemHolding.GetComponent<Rigidbody2D>().simulated = false;                
            //Debug.Log("Item grabbed");
            }
            anim.SetBool("HoldingBox", true);
            if(lastMoveDirection.y == -1){
                itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
        }
    }

    private void Release(){
        itemHolding.transform.position = transform.position + pickDirection * .5f;
        itemHolding.transform.parent = null;
        itemHolding.GetComponent<SpriteRenderer>().sortingOrder = 0;
        if(itemHolding.GetComponent<Rigidbody2D>()){
            itemHolding.GetComponent<Rigidbody2D>().simulated = true;
        }
        itemHolding = null;
        anim.SetBool("HoldingBox", false);
        //Debug.Log("Item released"); 
    }

    public void EndPill(){
        anim.SetBool("TakingPill", false);
    }
    public void Animate(){
        anim.SetFloat("AnimMoveX", moveDirection.x);
        anim.SetFloat("AnimMoveY", moveDirection.y);
        anim.SetFloat("AnimMoveMagnitude", moveDirection.magnitude);
        anim.SetFloat("AnimLastMoveX", lastMoveDirection.x);
        anim.SetFloat("AnimLastMoveY", lastMoveDirection.y);
    }
    
}
