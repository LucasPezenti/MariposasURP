using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    //public Message[] messages;
    //public Actor[] actors;
    public string startDialogueID;
    public string endDialogueID;
    public bool repeat;
    public bool areaTrigger;
    public GameObject interactionObj;

    public void StartDialogue(){
        /*
        if(!repeat){
            FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
            interactionObj.SetActive(false);
        }else{
            FindObjectOfType<DialogueManager>().OpenDialogue(messages, actors);
        }
        */
        if (!DialogueManager.onDialogue)
        {
            DialogueManager.onDialogue = true;
            if (!repeat)
            {
                FindObjectOfType<DialogueManager>().LoadDialogue(startDialogueID, endDialogueID);
                interactionObj.SetActive(false);
            }
            else
            {
                FindObjectOfType<DialogueManager>().LoadDialogue(startDialogueID, endDialogueID);
            }
        }
    }

    public bool GetRepeat(){
        return repeat;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (areaTrigger)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartDialogue();
            }
        }
    }

}

[System.Serializable]
public class Message{
    public int charId;
    public string message;
}

[System.Serializable]
public class Actor{
    public string name;
    public int spriteId;
}
