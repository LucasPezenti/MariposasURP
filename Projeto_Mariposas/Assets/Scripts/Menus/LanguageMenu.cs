using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LanguageMenu : MonoBehaviour
{
    // Buttons
    [SerializeField] private Button BPT_Btn;
    [SerializeField] private Button ENG_Btn;
    [SerializeField] private Button confirmBtn;

    public GameObject splash;
    public GameObject timeline;
    public GameObject menu;

    GameManager gameManager;
    AudioManager audioManager;


    private void Log(string conteudo)
    {
        conteudo = DateTime.Now + " " + conteudo + Environment.NewLine;
        System.IO.File.AppendAllText("mariposas.log", conteudo);
    }

    private void Awake()
    {
        gameManager = GameManager.GetInstance();
        audioManager = AudioManager.GetAudioInstance();

        BPT_Btn.onClick.AddListener(SelectBPT);
        ENG_Btn.onClick.AddListener(SelectENG);

        confirmBtn.onClick.AddListener(ConfirmSelection);
        confirmBtn.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            timeline.SetActive(false);
            splash.SetActive(false);
            menu.SetActive(true);
        }
    }

    public void SelectBPT(){

        //GameManager.GetInstance().SetDialogueFilePath(Application.dataPath + "/DIAG_BPT.txt");
        //GameManager.GetInstance().SetUIFilePath(Application.dataPath + "/UIUX_BPT.txt");
        gameManager.ChangeLanguage(Languages.BPT);
        //Log(GameManager.GetInstance().GetDialogueFile());
        //Log(Application.dataPath);
        FindAnyObjectByType<UIManager>().LoadUI();
        //Debug.Log(GameManager.GetInstance().GetUIFilePath());
        confirmBtn.gameObject.SetActive(true);

        audioManager.Play("ButtonFX01");
    }

    public void SelectENG(){
        //GameManager.GetInstance().SetDialogueFilePath(Application.dataPath + "/DIAG_ENG.txt");
        //GameManager.GetInstance().SetUIFilePath(Application.dataPath + "/UIUX_ENG.txt");
        gameManager.ChangeLanguage(Languages.ENG);
        FindAnyObjectByType<UIManager>().LoadUI();
        confirmBtn.gameObject.SetActive(true);
        audioManager.Play("ButtonFX01");
    }

    public void ConfirmSelection(){
        SceneManager.LoadScene("01_MainMenu");
        audioManager.Play("ButtonFX01");
    }
}