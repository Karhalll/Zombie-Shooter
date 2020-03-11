using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] AmmoType ammoType = AmmoType.Bullets;
    [SerializeField] int ammoAmount = 5;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Ammo>().IncreseCurrentAmmo(ammoType, ammoAmount);
            Destroy(gameObject);
        }
    }
}
