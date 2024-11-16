using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    GameManager GameManager = GameManager.GetInstance();

    public Sprite[] portraitSprites;

    public GameObject dialogueBox;
    public Image charImage;
    public TextMeshProUGUI charName;
    public TextMeshProUGUI messageText;

    private string idInicial;
    private string idFinal;
    private const string idActor = "ActorLine";
    private const string idMessage = "MessageLine";
    private List<Message> messageList  = new List<Message>();
    private List<Actor> actorList  = new List<Actor>();

    Message[] curMessages;
    Actor[] curChars;
    int activeMessage = 0;
    public static bool onDialogue = false;

    //  Método manual, colocando os diálogos no unity pelo DialogueTrigger
    /*
    public void OpenDialogue(Message[] messages, Actor[] actors){
        if(!onDialogue){
            curMessages = messages;     // Carrega a lista de mensagens do diálogo atual
            curChars = actors;          // Carrega a lista de personagens do diálogo atual
            activeMessage = 0;          // Identificado, volta para a primmeira mensagem da lista
            onDialogue = true;          
            //Debug.Log("Dialogue started! Number of messages: " + messages.Length);
            DisplayMessage();   
        }
    }
    */

    //  Versão manual
    /*
    public void DisplayMessage(){
        Message messageDisplay = curMessages[activeMessage];    // Pega na lista a mensagem que será mostrada
        messageText.text = messageDisplay.message;              // Mostra a mensagem
        Actor charDisplay = curChars[messageDisplay.charId];    // Pega na lista o personagem que está falando
        charName.text = charDisplay.name;                       // Mostra o nome do personagem
        //charImage.sprite = charDisplay.sprite;                  // Mostra a sprite do personagem
        dialogueBox.SetActive(true);                            // Torna a caixa de diálogo visível
    }
    */

    //  Método novo, lendo os diálogos a partir do arquivo .txt
    public void LoadDialogue(string startID, string endID){
        if(File.Exists(GameManager.GetInstance().GetDialogueFilePath())){
            activeMessage = 0;
            bool startReading = false;
            idInicial = startID;
            idFinal = endID;
            string[] lines = File.ReadAllLines(GameManager.GetInstance().GetDialogueFilePath());

            foreach (string line in lines)
            {
                if (line.Contains(idInicial)){
                    startReading = true;
                    continue;
                }
                
                else if (line.Contains(idFinal)){
                    break;
                }

                if (startReading)
                {
                    string[] info = line.Split('/');
                    if(line.Contains(idActor)){
                        actorList.Add(new Actor 
                        {
                            name = info[1].Trim(),
                            spriteId = int.Parse(info[2].Trim())
                        });
                    }
                    else if(line.Contains(idMessage)){
                        messageList.Add(new Message 
                        {
                            charId = int.Parse(info[1].Trim()),
                            message = info[2].Trim()
                        }); 
                    }
                    
                }
            }
            //onDialogue = true; 
            //Debug.Log("Dialogue started! Number of messages: " + messageList.Count);         
            DisplayMessage();   
        }

        else{
            Debug.Log("O arquivo não foi encontrado.");
        }
    }

    //  Versão com leitura de txt
    public void DisplayMessage(){
        Message messageDisplay = messageList[activeMessage];        // Pega na lista a mensagem que será mostrada
        messageText.text = messageDisplay.message;                  // Mostra a mensagem
        Actor charDisplay = actorList[messageDisplay.charId];       // Pega na lista o personagem que está falando
        charName.text = charDisplay.name;                           // Mostra o nome do personagem
        charImage.sprite = portraitSprites[charDisplay.spriteId];   // Mostra a imagem do personagem
        onDialogue = true;
        dialogueBox.SetActive(true);                                // Ativa o objeto da caixa de diálogo
    }

    public void NextMessage(){
        activeMessage++;
        if(activeMessage < messageList.Count){
            DisplayMessage();
        }else{
            onDialogue = false;
            dialogueBox.SetActive(false);
            activeMessage = 0;
            actorList.Clear();
            messageList.Clear();
            //Debug.Log("Dialogue ended");
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Space) && onDialogue == true){
            NextMessage();
        }
    }

}
