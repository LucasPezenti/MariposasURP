using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    GameManager gameManager = GameManager.GetInstance();
    AudioManager audioManager = AudioManager.GetAudioInstance();

    [Header("Buttons")]
    [SerializeField] private Button newGameBtn;

    //[SerializeField] private Button resumeGameBtn;

    [SerializeField] private Button settingsBtn;
    
    [SerializeField] private Button aboutBtn;

    [SerializeField] private Button creditsBtn;

    [SerializeField] private Button quitBtn;

    [SerializeField] private Button controlsBtn;

    [SerializeField] private Button languageBtn;

    [SerializeField] private Button aboutNextBtn;

    [Header("Back Buttons")]
    [SerializeField] private Button backCreditsBtn;
    [SerializeField] private Button backAboutBtn;
    [SerializeField] private Button backOptionsBtn;
    [SerializeField] private Button backLanguageBtn;
    [SerializeField] private Button backControlsBptBtn;
    [SerializeField] private Button backControlsEngBtn;

    [Header("Screens")]
    public GameObject mainScreen;
    public GameObject settingsScreen;
    public GameObject aboutScreen;
    public GameObject aboutPage1;
    public GameObject aboutPage2;
    public GameObject creditScreen;
    public GameObject controlsScreenBpt;
    public GameObject controlsScreenEng;
    public GameObject languageScreen;

    public Animator creditsAnimator;
    private int aboutPage = 1;

    private void Awake(){
        audioManager = AudioManager.GetAudioInstance();

        newGameBtn.onClick.AddListener(StartNewGame);
        settingsBtn.onClick.AddListener(OpenSettings);
        creditsBtn.onClick.AddListener(OpenCredits);
        quitBtn.onClick.AddListener(QuitGame);
        controlsBtn.onClick.AddListener(OpenControls);
        languageBtn.onClick.AddListener(OpenLanguageSelection);
        aboutBtn.onClick.AddListener(OpenAboutScreen);
        aboutNextBtn.onClick.AddListener(NextPageAbout);

        backCreditsBtn.onClick.AddListener(BackCredits);
        backAboutBtn.onClick.AddListener(BackAbout);
        backOptionsBtn.onClick.AddListener(BackOptions);
        backLanguageBtn.onClick.AddListener(BackLanguage);
        backControlsBptBtn.onClick.AddListener(BackControlsBpt);
        backControlsEngBtn.onClick.AddListener(BackControlsEng);
    }

    private void Start()
    {
        audioManager.Play("BasementMusic");
    }

    public void StartNewGame(){
        audioManager.Play("ButtonFX01");
        audioManager.Stop("BasementMusic");
        SceneManager.LoadScene("02_Prologue");
    }

    // Desenvolver sistema de saves para utilizar a opção "Continuar jogo"
    /*
    public void ResumeGame(){

    }
    */

    //  Criar menu de configurações
    
    public void OpenSettings(){
        settingsScreen.SetActive(true);
        mainScreen.SetActive(false);
        audioManager.Play("ButtonFX01");
    }

    public void OpenControls()
    {
        if (gameManager.GetCurLanguage() == Languages.BPT)
        {
            controlsScreenBpt.SetActive(true);
        }

        else if(gameManager.GetCurLanguage() == Languages.ENG)
        {
            controlsScreenEng.SetActive(true);
        }
        audioManager.Play("ButtonFX01");

    }

    public void OpenLanguageSelection()
    {
        languageScreen.SetActive(true);
        audioManager.Play("ButtonFX01");
    }

    public void OpenAboutScreen()
    {
        aboutScreen.SetActive(true);
        GetComponent<AboutReader>().LoadAboutText();
        aboutPage1.SetActive(true);
        aboutNextBtn.gameObject.SetActive(true);
        aboutPage2.SetActive(false);
        aboutPage = 1;
        audioManager.Play("ButtonFX01");
    }

    public void NextPageAbout()
    {
        if (aboutPage == 1)
        {
            aboutPage2.SetActive(true);
            aboutPage1.SetActive(false);
            aboutPage++;
            audioManager.Play("ButtonFX01");
        }
        else if(aboutPage == 2)
        {
            aboutPage1.SetActive(true);
            aboutPage2.SetActive(false);
            aboutPage--;
            audioManager.Play("ButtonFX01");
        }
        
    }

    //  Criar tela de créditos

    public void OpenCredits(){        
        creditScreen.SetActive(true);
        GetComponent<CreditsReader>().LoadCredits();
        FindObjectOfType<CreditsMovement>().StartCredits();
        //creditsAnimator.SetTrigger("TriggerCredits");
        audioManager.Play("ButtonFX01");
        audioManager.Stop("BasementMusic");
        audioManager.Play("PrologueMusic");
    }

    public void QuitGame(){
        audioManager.Play("ButtonFX01");
        Debug.Log("Jogo fechado!");
        Application.Quit();
    }


    public void BackCredits()
    {
        creditScreen.SetActive(false);
        audioManager.Stop("PrologueMusic");
        audioManager.Play("BasementMusic");
        audioManager.Play("ButtonFX01");
    }

    public void BackAbout()
    {
        aboutScreen.SetActive(false);
        audioManager.Play("ButtonFX01");
    }

    public void BackOptions()
    {
        mainScreen.SetActive(true);
        settingsScreen.SetActive(false);
        audioManager.Play("ButtonFX01");
    }

    public void BackLanguage()
    {
        languageScreen.SetActive(false);
        audioManager.Play("ButtonFX01");
    }

    public void BackControlsBpt()
    {
        controlsScreenBpt.SetActive(false);
        audioManager.Play("ButtonFX01");
    }

    public void BackControlsEng()
    {
        controlsScreenEng.SetActive(false);
        audioManager.Play("ButtonFX01");
    }

}
