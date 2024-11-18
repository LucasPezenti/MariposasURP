using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownMovement : MonoBehaviour
{
    [SerializeField] private float speed { get; set; }
    [SerializeField] private float speedX { get; set; }
    [SerializeField] private float speedY { get; set; }
    [SerializeField] public bool moved { get; set; }
    [SerializeField] private bool running { get; set; }
    
    [SerializeField] public static bool TDCanMove;
    [SerializeField] private bool canRun;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private HoldObjectScript holdObjectScript;
    [SerializeField] public Direction dir { get; set; }
    [SerializeField] public Direction lastDir { get; set; }


    [Header("Environment info")]
    [SerializeField] private Room roomInstance;
    [SerializeField] private Rooms curRoom;
    [SerializeField] private inRangeOf curInRange;
    [SerializeField] private GameObject objInRange;

    [Header("Interaction info")]
    [SerializeField] private InteractionAlert interactionAlert;

    [Header("Box info")]
    [SerializeField] private bool hasBox;
    [SerializeField] private PuzzleBox curBox;
    [SerializeField] private BoxPuzzleManager boxManager;
    [SerializeField] private ExamineManager examineManager;
    [SerializeField] private DialogueTrigger wrongBoxTrigger;
    

    [Header("Inventory Info")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private bool onInventory;

    [Header("Tutorials")]
    [SerializeField] private TutorialTrigger examineTutorial;
    [SerializeField] private bool examineTutorialDone;
    [SerializeField] private TutorialTrigger closeExamineTutorial;
    [SerializeField] private bool closeExamineDone;
    [SerializeField] private TutorialTrigger inventoryNavTutorial;
    [SerializeField] private bool inventoryNavDone;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        holdObjectScript = GetComponent<HoldObjectScript>();
        inventory = GetComponent<Inventory>();
        wrongBoxTrigger = GetComponent<DialogueTrigger>();
        boxManager = null;
        curBox = null;

    }
    // Start is called before the first frame update
    void Start()
    {
        speed = 1.4f;
        moved = false;
        running = false;

        curBox = null;
        curRoom = Rooms.OUTSIDE;
        curInRange = inRangeOf.NOTHING;

        hasBox = false;

        examineTutorialDone = false;
        closeExamineDone = false;
        inventoryNavDone = false;

        onInventory = false;

        TDCanMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!TDCanMove || DialogueManager.onDialogue || onInventory){
            moved = false;
            moveDirection.x = 0;
            moveDirection.y = 0;
        }
        MovementInputs();
        if (!DialogueManager.onDialogue)
        {
            InteractionInputs();
        }
    }

    void FixedUpdate(){
        Move();
    }

    private void MovementInputs(){
        if(TDCanMove && !DialogueManager.onDialogue && !onInventory){
            speedX = Input.GetAxisRaw("Horizontal");
            speedY = Input.GetAxisRaw("Vertical");

            if(canRun){
                running = false;
                if(Input.GetKey("left shift")){
                    running = true;
                }
            }

            moveDirection = new Vector2(speedX, speedY).normalized;
        }
    }

    private void InteractionInputs()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Show dialogue
            if (curInRange == inRangeOf.DIALOGUE && !onInventory && !examineManager.isExamining) 
            {
                objInRange.GetComponent<DialogueTrigger>().StartDialogue();
            }

            // Grab box
            else if (curInRange == inRangeOf.BOX && !hasBox && !onInventory && !examineManager.isExamining) 
            {
                Debug.Log("Pegou");
                GrabBox(objInRange);
                interactionAlert.TurnAlertOff();
                if (!examineTutorialDone)
                {
                    examineTutorial.OpenTutorial();
                    examineTutorialDone = true;
                }
            }

            // Unpack box
            else if (hasBox && curRoom == curBox.GetBoxRoom() && !onInventory && !examineManager.isExamining) 
            {
                Debug.Log("Sala correta");
                boxManager.Unpack(curRoom);
                roomInstance.SetDone(true);
                interactionAlert.TurnAlertOff();
                hasBox = false;
                Destroy(curBox.gameObject);
                curBox = null;
            }

            // Wrong place
            else if (hasBox && curRoom != curBox.GetBoxRoom() && !onInventory && !examineManager.isExamining) 
            {
                // Dialogo de "Lugar errado"
                interactionAlert.TurnAlertOff();
                wrongBoxTrigger.StartDialogue();
                Debug.Log("Lugar errado");
            }

            else if (curInRange == inRangeOf.LIGHTSWITCH && !onInventory && !examineManager.isExamining)
            {
                objInRange.GetComponent<LightSwitch>().SwitchLights();
                Debug.Log("Interruptor pressionado");
            }
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            
            // Turn Examine screen on and off
            if (hasBox && !examineManager.isExamining && !onInventory)
            {
                examineManager.DisplayItem(curBox.GetBoxImage());
                if (!closeExamineDone)
                {
                    closeExamineTutorial.OpenTutorial();
                    closeExamineDone = true;
                }
            }
            else if (hasBox && examineManager.isExamining)
            {
                examineManager.CloseItemDisplay();
            }
        }

        else if (Input.GetKeyDown(KeyCode.F)) // Turn flashlight on
        {
            inventory.SwitchLantern();
            //Debug.Log("can move = " + TDCanMove);
        }

        else if (Input.GetKeyDown(KeyCode.Tab)) // Open inventory
        {
            if (!onInventory && !examineManager.isExamining) 
            { 
                inventory.OpenInventory();
                onInventory = true;
                if (!inventoryNavDone)
                {
                    inventoryNavTutorial.OpenTutorial();
                    inventoryNavDone = true;
                }
            }

            else 
            { 
                inventory.CloseInventory();
                onInventory = false;
            }
        }
    }

    private void Move(){
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y  * speed);
        
        moved = false;
        
        // Verify if Player moved
        if(speedX != 0 || speedY != 0){
            moved = true;
        }

        // Moved RIGHT
        if(speedX > 0){
            dir = Direction.RIGHT;
            lastDir = Direction.RIGHT;
        }

        // Moved LEFT
        else if(speedX < 0){
            dir = Direction.LEFT;
            lastDir = Direction.LEFT;
        }

        // Moved UP
        if(speedY > 0){
            dir = Direction.UP;
            lastDir = Direction.UP;
        }

        // Moved DOWN
        else if(speedY < 0){
            dir = Direction.DOWN;
            lastDir = Direction.DOWN;
        }
        
        if(running){
            speed = 3.2f;
        }else{
            speed = 1.4f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Room"))
        {
            this.curRoom = collision.GetComponent<Room>().GetRoom();
            this.roomInstance = collision.GetComponent<Room>();
            this.boxManager = collision.GetComponent<BoxPuzzleManager>();
            //Debug.Log(curRoom);
            if (hasBox && !roomInstance.GetDone()) 
            { 
                interactionAlert.TurnAlertOn(false); 
            }
        }
        else
        {
            objInRange = collision.gameObject;
        }

        if (collision.gameObject.CompareTag("Dialogue"))
        {
            //can start dialogue
            curInRange = inRangeOf.DIALOGUE;
            interactionAlert.TurnAlertOn(true);
        }
        else if (collision.gameObject.CompareTag("PickUp"))
        {
            //can carry object
            curInRange = inRangeOf.BOX;
            interactionAlert.TurnAlertOn(false);
        }
        
        else if (collision.gameObject.CompareTag("LightSwitch"))
        {
            //can turn lights on and off (basement)
            curInRange = inRangeOf.LIGHTSWITCH;
            interactionAlert.TurnAlertOn(false);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Dialogue"))
        {
            curInRange = inRangeOf.NOTHING;
            interactionAlert.TurnAlertOff();
        }
        else if (collision.gameObject.CompareTag("PickUp"))
        {
            curInRange = inRangeOf.NOTHING;
            interactionAlert.TurnAlertOff();
        }
        else if (collision.gameObject.CompareTag("LightSwitch"))
        {
            //can turn lights on and off (basement)
            curInRange = inRangeOf.NOTHING;
            interactionAlert.TurnAlertOff();
        }

        if (collision.gameObject.CompareTag("Room"))
        {
            this.curRoom = Rooms.OUTSIDE;
            roomInstance = null;
            interactionAlert.TurnAlertOff();
            //Debug.Log(curRoom);
        }

    }

    public void GrabBox(GameObject box)
    {
        holdObjectScript.PickUp(objInRange);
        curBox = box.GetComponent<PuzzleBox>();
        hasBox = true;
    }

    public void StopMoving(){
        speedX = 0;
        speedY = 0;
    }

    public Vector2 GetMoveDirection(){
        return this.moveDirection;
    }


    public bool GetHasBox()
    {
        return this.hasBox;
    }

    public bool GetOnInventory()
    {
        return this.onInventory;
    }

    public void SetTDCanMove(bool canMove)
    {
        TDCanMove = canMove;
        Debug.Log("can move = " + TDCanMove);
    }

    public void SetOnInventory(bool onInventory)
    {
        this.onInventory = onInventory;
        Debug.Log("on inventory = " + this.onInventory);
    }

    public void SetDirection(int direction)
    {
        if (direction == 2)
        {
            this.dir = Direction.DOWN;
            this.lastDir = Direction.DOWN;
        }

        else if (direction == 4)
        {
            this.dir = Direction.LEFT;
            this.lastDir = Direction.LEFT;
        }

        else if(direction == 6)
        {
            this.dir = Direction.RIGHT;
            this.lastDir = Direction.RIGHT;
        }

        else if(direction == 8)
        {
            this.dir = Direction.UP;
            this.lastDir = Direction.UP;
        }

        else
        {
            this.dir = Direction.DOWN;
            this.lastDir = Direction.DOWN;
        }
    }
}

public enum inRangeOf
{
    NOTHING,
    BOX,
    DIALOGUE,
    ITEM,
    LIGHTSWITCH
}
