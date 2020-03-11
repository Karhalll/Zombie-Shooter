using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas = null;

    private void Start() 
    {
        gameOverCanvas.enabled = false;
    }

    public void HeandleDeath()
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0f;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
