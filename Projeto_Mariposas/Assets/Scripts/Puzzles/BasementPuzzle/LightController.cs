using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] lightSprite;
    [SerializeField] private Light2D lightSource;
    [SerializeField] private GameObject blocker;
    [SerializeField] private GameObject[] moths;
    [SerializeField] private bool isOn;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (isOn)
        {
            spriteRenderer.sprite = lightSprite[1];
            for (int i=0; i < moths.Length; i++) 
            { 
                moths[i].gameObject.SetActive(true); 
            }
            lightSource.enabled = true;
            blocker.gameObject.SetActive(true);
        }
        else
        {
            spriteRenderer.sprite = lightSprite[0];
            for (int i = 0; i < moths.Length; i++)
            {
                moths[i].gameObject.SetActive(false);
            }
            lightSource.enabled = false;
            blocker.gameObject.SetActive(false);
        }
    }

    public void TurnLightOn()
    {
        spriteRenderer.sprite = lightSprite[1];
        for (int i = 0; i < moths.Length; i++)
        {
            moths[i].gameObject.SetActive(true);
        }
        lightSource.enabled = true;
        blocker.gameObject.SetActive(true);
        isOn = true;
    }

    public void TurnLightOff()
    {
        spriteRenderer.sprite = lightSprite[0];
        for (int i = 0; i < moths.Length; i++)
        {
            moths[i].gameObject.SetActive(false);
        }
        lightSource.enabled = false;
        blocker.gameObject.SetActive(false);
        isOn = false;
    }

    public bool GetIsOn()
    {
        return this.isOn;
    }
}
