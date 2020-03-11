using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] AmmoSlot[] ammoSlots = null;

    [System.Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType = AmmoType.Bullets;
        public int ammoAmount = 100;
    }

    public int GetCurrentAmmo(AmmoType ammoType)
    {
        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);

        if (ammoSlot != null)
        {
            return ammoSlot.ammoAmount;
        }

        return 0;
    }

    public void IncreseCurrentAmmo(AmmoType ammoType, int ammoAmount)
    {
        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);

        if (ammoSlot != null)
        {
            ammoSlot.ammoAmount += ammoAmount;
        }
    }

    public void ReduceCurrentAmmo(AmmoType ammoType)
    {
        AmmoSlot ammoSlot = GetAmmoSlot(ammoType);

        if (ammoSlot != null)
        {
            ammoSlot.ammoAmount--;
        }
    }

    private AmmoSlot GetAmmoSlot(AmmoType ammoType)
    {
        foreach (AmmoSlot slot in ammoSlots)
        {
            if (slot.ammoType == ammoType)
            {
                return slot;
            }
        }

        return null;
    }
}
