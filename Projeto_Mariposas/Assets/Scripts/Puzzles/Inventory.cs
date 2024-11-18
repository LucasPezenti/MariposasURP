using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Inventory : MonoBehaviour
{
    [SerializeField] private TopDownMovement topDownMovement;

    [Header("Inventory Settings")]
    [SerializeField] private bool isOpen;

    [Header("Selection Settings")]
    [SerializeField] private GameObject selection;
    [SerializeField] private Transform[] selectionPoints;
    [SerializeField] private int selectId;

    [Header("Item Splashs")]
    [SerializeField] private GameObject inventoryObj;
    [SerializeField] private GameObject pillsSplash;
    [SerializeField] private GameObject insecticideSplash;
    [SerializeField] private GameObject hammerSplash;
    [SerializeField] private GameObject lanternOffSplash;
    [SerializeField] private GameObject lanternOnSplash;

    [Header("Item check")]
    [SerializeField] private bool hasPills;
    [SerializeField] private bool hasInsecticide;
    [SerializeField] private bool hasHammer;
    [SerializeField] private bool hasLantern;

    [Header("Triggers")]
    [SerializeField] private bool takingPills;
    [SerializeField] private bool usingSpray;
    [SerializeField] private bool missionPills;
    [SerializeField] private GameObject deadMoths;

    [Header("Lantern Settings")]
    [SerializeField] private GameObject lanternComand;
    [SerializeField] private GameObject lanternOnComandSplash;
    [SerializeField] private GameObject lanternOffComandSplash;
    [SerializeField] private Light2D lightSource;
    [SerializeField] private bool isDark;
    [SerializeField] private bool lanternOn;
    [SerializeField] private bool holdingLantern;

    [Header("Tutorial")]
    [SerializeField] private TutorialTrigger inventoryNavTutorial;
    [SerializeField] private TutorialTrigger useItemTutorial;
    [SerializeField] private bool useItemDone;

    //public static bool onInventory;

    private void Awake()
    {
        topDownMovement = GetComponent<TopDownMovement>();
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
        missionPills = true;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInputs();
    }

    private void ProcessInputs()
    {
        if (isOpen)
        {
            if(Input.GetKeyDown(KeyCode.S))
            {
                if (inventoryNavTutorial.GetIsOpen())
                {
                    inventoryNavTutorial.CloseTutorial();
                }
                if (!useItemDone)
                {
                    useItemTutorial.OpenTutorial();
                    useItemDone = true;
                }
                selectId++;
                if (selectId > 3)
                {
                    selectId = 0;
                }
                selection.transform.position = selectionPoints[selectId].transform.position;
                selection.transform.rotation = selectionPoints[selectId].transform.rotation;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                if (inventoryNavTutorial.GetIsOpen())
                {
                    inventoryNavTutorial.CloseTutorial();
                }
                if (!useItemDone)
                {
                    useItemTutorial.OpenTutorial();
                    useItemDone = true;
                }
                selectId--;
                if (selectId < 0)
                {
                    selectId = 3;
                }
                selection.transform.position = selectionPoints[selectId].transform.position;
                selection.transform.rotation = selectionPoints[selectId].transform.rotation;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (selectId == 0 && hasPills) // Tomar remédios
                {
                    TakePills();
                    CloseInventory();
                    topDownMovement.SetOnInventory(false);
                } 

                if (selectId == 3 && hasLantern) // Pegar Lanterna
                {
                    HoldLantern();
                    CloseInventory();
                    topDownMovement.SetOnInventory(false);
                } 
            }
        }
        
    }

    public void OpenInventory()
    {
        isOpen = true;
        inventoryObj.SetActive(true);
        if (hasPills) { pillsSplash.SetActive(true); }
        
        if (hasInsecticide) { insecticideSplash.SetActive(true); }
        
        if (hasHammer) { hammerSplash.SetActive(true); }
        
        if (hasLantern)
        {
            if (lanternOn)
            {
                lanternOnSplash.SetActive(true);
                lanternOffSplash.SetActive(false);
            }
            else
            {
                lanternOffSplash.SetActive(true);
                lanternOnSplash.SetActive(false);
            }
        }
    }

    public void CloseInventory()
    {
        isOpen = false;
        inventoryObj.SetActive(false);
    }

    public void SwitchLantern()
    {
        if (holdingLantern)
        {
            if (lanternOn)
            {
                lanternOn = false;
                lightSource.enabled = false;
                lanternOffComandSplash.SetActive(true);
                lanternOnComandSplash.SetActive(false);

            }
            else
            {
                lanternOn = true;
                lightSource.enabled = true;
                lanternOnComandSplash.SetActive(true);
                lanternOffComandSplash.SetActive(false);

            }
        }
    }

    public void HoldLantern()
    {
        if (hasLantern)
        {
            if (!holdingLantern)
            {
                holdingLantern = true;
                lanternComand.SetActive(true);
                lanternOffComandSplash.SetActive(true);
                lanternOn = true;
                lightSource.enabled = true;
            }
            else
            {
                holdingLantern = false;
                lanternComand.SetActive(false);
                lanternOffComandSplash.SetActive(false);
                lanternOn = false;
                lightSource.enabled = false;
            }
        }
    }

    public void TakePills()
    {
        if (hasPills)
        {
            takingPills = true;
            if (missionPills)
            {
                FindObjectOfType<TakePillsFinish>().PillsTaken();
                missionPills = false;
            }
            
        }
    }

    public void ColectPills()
    {
        hasPills = true;
    }

    public void ColectInsecticide()
    {
        hasInsecticide = true;
    }

    public void ColectHammer()
    {
        hasHammer = true;
    }

    public void ColectLantern()
    {
        hasLantern = true;
    }

    public bool GetPills()
    {
        return this.hasPills;
    }

    public bool GetInsecticide()
    {
        return this.hasInsecticide;
    }

    public bool GetHammer()
    {
        return this.hasHammer;
    }

    public bool GetLantern()
    {
        return this.hasLantern;
    }

    public bool GetHoldingLantern()
    {
        return this.holdingLantern;
    }

    public bool GetLanternOn()
    {
        return this.lanternOn;
    }

    public bool GetTakingPills()
    {
        return this.takingPills;
    }

    public void SetTakingPillsOff()
    {
        this.takingPills = false;
        TopDownMovement.TDCanMove = true;
    }

    public bool GetUsingSpray()
    {
        return this.usingSpray;
    }
    public void SetSprayOff()
    {
        this.usingSpray = false;
        deadMoths.SetActive(false);
        TopDownMovement.TDCanMove = true;
    }
    public void SetSprayOn(GameObject moth)
    {
        if (hasInsecticide)
        {
            this.usingSpray = true;
            this.deadMoths = moth;
        }
    }

    public bool GetIsOpen()
    {
        return this.isOpen;
    }

}
