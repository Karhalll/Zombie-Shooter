using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [Range(0f, 100f)]
    [SerializeField] float percentageOfRestoration = 50f;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponentInChildren<FlashLightSystem>().RestoreBatteryInPercents(percentageOfRestoration);
            Destroy(gameObject);
        }
    }
}
