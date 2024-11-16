using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


interface IInteractable{
    public void Interact();
}

public class Interactable : MonoBehaviour
{

    private bool inRange;
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private UnityEvent interactAction;
    public bool isDialogue;
    public GameObject alert;
    public InteractionAlert interactionAlert;

    //[SerializeField] GameObject alertObject;

    void Start()
    {
        interactionAlert = FindObjectOfType<InteractionAlert>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange && !DialogueManager.onDialogue){
            Interact();
        }
    }

    private void Interact(){
        if(Input.GetKeyDown(interactKey)){
            interactionAlert.TurnAlertOff();
            interactAction.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            inRange = true;
            interactionAlert.TurnAlertOn(isDialogue);
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            inRange = false;
            interactionAlert.TurnAlertOff();
        }
    }

    public bool GetInRange(){
        return this.inRange;
    }
}
