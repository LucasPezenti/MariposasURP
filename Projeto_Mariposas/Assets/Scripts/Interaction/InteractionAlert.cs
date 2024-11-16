using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionAlert : MonoBehaviour
{
    [SerializeField] private GameObject interactionAlert;
    [SerializeField] private GameObject dialogueAlert;
    private bool isInteractable;
    private bool isDialogue;

    void Start(){
        isInteractable = false;
    }

    void Update(){
        if(!isInteractable){ // Se necessário, Adicionar verificação de diálogo
            interactionAlert.SetActive(false);
            dialogueAlert.SetActive(false);
        }
        else{
            if (isDialogue)
            {
                dialogueAlert.SetActive(true);
            }
            else
            {
                interactionAlert.SetActive(true);
            }
        }
    }
    public void TurnAlertOn(bool isDialogue)
    {
        isInteractable = true;
        this.isDialogue = isDialogue;
    }

    public void TurnAlertOff()
    {
        isInteractable = false;
    }

}
