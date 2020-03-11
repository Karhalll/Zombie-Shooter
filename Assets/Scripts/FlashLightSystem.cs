using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FlashLightSystem : MonoBehaviour
{
    [Tooltip("Decay in percentages per second.")]
    [Range(0.1f, 100f)]
    [SerializeField] float percentageDecay = 1f;
    [SerializeField] float minimumAngle = 40f;
    [SerializeField] TextMeshProUGUI percentageUIText = null;

    float batteryLife = 100f;

    Light myLight;
    float baseAngle;
    float baseIntensity;

    float onePercentOfAngel;
    float onePercentOfIntensity;

    private void Awake() 
    {
        myLight = GetComponent<Light>();
    }

    private void Start()
    {
        GetBases();
        CalculateBatteryPercantage();
    }

    private void Update() 
    {
        DecreaseBatteryLife();
        CalculateLightIntensity();
        UpadteUI();
    }

    public void RestoreBatteryInPercents(float percentageOfRestoration)
    {
        batteryLife = Mathf.Clamp(batteryLife + percentageOfRestoration, 0f, 100f);
    }

    private void UpadteUI()
    {
        percentageUIText.text = batteryLife.ToString("##") + " %";
    }

    private void DecreaseBatteryLife()
    {
        if (batteryLife < 0)
        {
            batteryLife = 0;
        }
        else if (batteryLife == 0)
        {
            return;
        }
        else
        {
            batteryLife -= percentageDecay * Time.deltaTime;
        }
    }

    private void CalculateLightIntensity()
    {
        myLight.intensity = Mathf.Clamp(batteryLife * onePercentOfIntensity, 0f, baseIntensity);
        myLight.spotAngle = Mathf.Clamp(batteryLife * onePercentOfAngel, minimumAngle, baseAngle);
    }

    private void GetBases()
    {
        baseAngle = myLight.spotAngle;
        baseIntensity = myLight.intensity;
    }

    private void CalculateBatteryPercantage()
    {
        onePercentOfAngel = (baseAngle - minimumAngle) / 100f;
        onePercentOfIntensity = (baseIntensity) / 100f;
    }
}
