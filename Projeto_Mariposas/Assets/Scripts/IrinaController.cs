using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IrinaController : MonoBehaviour
{

    Animator animator;
    private string curState;
    public Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool moved;
    private float moveX;
    private float moveY;
    public float  moveSpd;

    private bool onScene;

    private int right_dir = 0, left_dir = 1;
    private int dir = 0, last_dir = 0;
    const string PLAYER_IDLE_R = "Ira_Idle_R";
    const string PLAYER_IDLE_L = "Ira_Idle_L";
    const string PLAYER_WALK_R = "Ira_Walk_R";
    const string PLAYER_WALK_L = "Ira_Walk_L";
    const string IRINA_WAKE_UP = "Irina_WakeUp";
    const string IRINA_FALL = "Irina_LayDown";

    void Start()
    {
        animator = GetComponent<Animator>();
        moved = false;
        moveX = 0;
        moveY = 0;
        onScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
        AnimatePlayer();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void ProcessInputs(){
        if(onScene || DialogueManager.onDialogue == true){
            moveDirection.x = 0;
            moved = false;
            return;
        }
        moveX = Input.GetAxisRaw("Horizontal");

        moveDirection = new Vector2(moveX, moveY).normalized;

    }

    private void Move(){

        rb.velocity = new Vector2(moveDirection.x * moveSpd, moveDirection.y);

        moved = false;
        moveSpd = 1.4f;

        if(moveX > 0){
            moved = true;
            dir = right_dir;
            last_dir = right_dir;
        }
        else if(moveX < 0){
            moved = true;
            dir = left_dir;
            last_dir = left_dir;
        }
    }

    private void AnimatePlayer(){

        // Idle animation
        if(!moved){
            if(last_dir == right_dir){
                ChangeAnimationState(PLAYER_IDLE_R);
            }
            else if(last_dir == left_dir){
                ChangeAnimationState(PLAYER_IDLE_L);
            }
        }

        // Walking animation
        else if(moved){
            if(dir == right_dir){
                ChangeAnimationState(PLAYER_WALK_R);
            }
            else if(dir == left_dir){
                ChangeAnimationState(PLAYER_WALK_L);
            }
        }


    }

    public void ChangeAnimationState(string newState){

        // Stop an animaiton from interrupting itself
        if(curState == newState){
            return;
        }

        animator.Play(newState);
        curState = newState;

    }

    public void SetOnSceneOn(){
        onScene = true;
    }

    public void SetOnSceneOff(){
        onScene = false;
    }

}
