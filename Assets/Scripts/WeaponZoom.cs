using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera mainCamera = null;
    [SerializeField] float zoomedIn = 30f;
    [SerializeField] float zoomedOut = 60f;

    bool zoomedInToggle = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedInToggle == false)
            {
                zoomedInToggle = true;
                mainCamera.fieldOfView = zoomedIn;
            }
            else
            {
                zoomedInToggle = false;
                mainCamera.fieldOfView = zoomedOut;
            }
        }
    }
}
