using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] switchSprite;
    [SerializeField] private bool isOn;
    [SerializeField] private Light2D lightSource;

    [SerializeField] private LightController[] lights;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (isOn)
        {
            spriteRenderer.sprite = switchSprite[1];
            lightSource.color = Color.green;
        }
        else
        {
            spriteRenderer.sprite = switchSprite[0];
            lightSource.color = Color.red;
        }
    }

    public void SwitchLights()
    {
        for(int i = 0; i < lights.Length; i++)
        {
            if (lights[i].GetIsOn())
            {
                lights[i].TurnLightOff();
            }

            else
            {
                lights[i].TurnLightOn();
            }
        }
        if (!isOn)
        {
            spriteRenderer.sprite = switchSprite[1];
            lightSource.color = Color.green;
            isOn = true;
        }
        else
        {
            spriteRenderer.sprite = switchSprite[0];
            lightSource.color = Color.red;
            isOn = false;
        }
    }
}
