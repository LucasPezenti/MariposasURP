using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollMovement : MonoBehaviour
{
    private AudioManager audioManager;

    private float speed;
    private float speedX, speedY;
    private bool moved;
    private bool running;
    public static bool SScanMove;
    [SerializeField] private bool canRun;

    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Direction dir, lastDir;

    // Start is called before the first frame update
    void Start()
    {
        moved = false;
        speed = 1.4f;
        speedY = 0;
        running = false;
        SScanMove = true;
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!SScanMove || DialogueManager.onDialogue){
            moved = false;
            moveDirection.x = 0;
        }
        ProcessInputs();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs(){
        if(SScanMove && !DialogueManager.onDialogue){
            speedX = Input.GetAxisRaw("Horizontal");

            if(canRun){
                running = false;
                if(Input.GetKey("left shift")){
                    running = true;
                }
            }
            
            moveDirection = new Vector2(speedX, speedY).normalized;
        }
        
    }

    private void Move(){
        speed = 1.4f;
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y);
        
        moved = false;

        if(speedX > 0){
            moved = true;
            dir = Direction.RIGHT;
            lastDir = Direction.RIGHT;
        }

        else if(speedX < 0){
            moved = true;
            dir = Direction.LEFT;
            lastDir = Direction.LEFT;
        }

        if(running){
            speed = 3.2f;
        }else{
            speed = 1.4f;
        }
        
    }

    public float GetSpeedX(){
        return this.speedX;
    }

    public float GetSpeedY(){
        return this.speedY;
    }

    public bool GetMoved(){
        return this.moved;
    }

    public Vector2 GetMoveDirection(){
        return this.moveDirection;
    }

    public Direction GetDir(){
        return this.dir;
    }

    public Direction GetLastDir(){
        return this.lastDir;
    }
}
