using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSelector : MonoBehaviour
{
    [SerializeField] private Button BPT_Btn;
    [SerializeField] private Button ENG_Btn;
    GameManager gameManager = GameManager.GetInstance();
    AudioManager audioManager = AudioManager.GetAudioInstance();

    private void Awake()
    {
        BPT_Btn.onClick.AddListener(SelectBPT);
        ENG_Btn.onClick.AddListener(SelectENG);
    }

    public void SelectBPT()
    {
        gameManager.ChangeLanguage(Languages.BPT);
        FindAnyObjectByType<UIManager>().LoadUI();
        audioManager.Play("ButtonFX01");
    }

    public void SelectENG()
    {
        gameManager.ChangeLanguage(Languages.ENG);
        FindAnyObjectByType<UIManager>().LoadUI();
        audioManager.Play("ButtonFX01");
    }
}
