using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    [Header("Missions")]
    [SerializeField] private UIElement[] missionList;
    [SerializeField] private UIElement[] missionStepsList;
    [SerializeField] private int missionIndex;

    [Header("Button Settings")]
    [SerializeField] private Button missionBtn;
    [SerializeField] private Animator buttonAnimator;
    [SerializeField] private bool isButtonPressed;

    [Header("Dropdown Settings")]
    [SerializeField] private MissionDropdown missionDropdown;
    [SerializeField] private bool isDropdownActive;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = AudioManager.GetAudioInstance();
    }

    private void Start()
    { 
        missionBtn.onClick.AddListener(SwitchDropdown);
        isDropdownActive = false;
        LoadMission();
    }

    public void LoadMission()
    {
        if (System.IO.File.Exists(GameManager.GetInstance().GetUIFilePath()))
        {
            string[] lines = System.IO.File.ReadAllLines(GameManager.GetInstance().GetUIFilePath());
            foreach (string line in lines)
            {
                string[] info = line.Split('/');
                if (line.Contains(missionList[missionIndex].textID))
                {
                    missionList[missionIndex].textToDisplay.text = info[1].Trim();
                }
                if (line.Contains(missionStepsList[missionIndex].textID))
                {
                    missionStepsList[missionIndex].textToDisplay.text = info[1].Trim();
                }
            }
        }
        buttonAnimator.SetTrigger("NewMission");
        
        Debug.Log("Nova missao");
    }

    private void SwitchDropdown()
    {
        //buttonAnimator.SetBool("NewMission", false);
        if (!isDropdownActive)
        {
            missionDropdown.OpenDropdown();
            isDropdownActive = true;
        }
        else
        {
            missionDropdown.CloseDropdown();
            isDropdownActive = false;
        }
    }

    public void SetMissionIndex(int index)
    {
        missionIndex = index;
        LoadMission();
    }
}
