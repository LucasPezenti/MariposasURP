using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExamineManager : MonoBehaviour
{
    [SerializeField] private KeyCode closeKey;

    [SerializeField] public GameObject examineBox;
    [SerializeField] public Image itemImage;
    [SerializeField] private Item curItem;
    public bool isExamining { get; set; }

    /*
    private static ExamineManager Instance;

    public static ExamineManager GetInstance()
    {
        return Instance;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    */
    private void Start()
    {
        isExamining = false;
    }

    public void DisplayItem(Sprite itemImage){
        this.itemImage.sprite = itemImage;
        examineBox.SetActive(true);
        SideScrollMovement.SScanMove = false;
        TopDownMovement.TDCanMove = false;
        isExamining = true; 

    }

    public void CloseItemDisplay()
    {
        examineBox.SetActive(false);
        SideScrollMovement.SScanMove = true;
        TopDownMovement.TDCanMove = true;
        isExamining = false;
    }

}
