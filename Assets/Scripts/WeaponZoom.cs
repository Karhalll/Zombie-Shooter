using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera mainCamera = null;

    [Header("FOV")]
    [SerializeField] float zoomedIn = 30f;
    [SerializeField] float zoomedOut = 60f;

    [Header("Sensitivity multiplier")]
    [SerializeField] float sensIn = 0.25f;
    [SerializeField] float sensOut = 1f;

    bool zoomedInToggle = false;

    MouseLook mouseLook;
    float basicSensX;
    float basicSensY;


    private void Start() 
    {
        mouseLook = GetComponentInParent<RigidbodyFirstPersonController>().mouseLook;
        basicSensX = mouseLook.XSensitivity;
        basicSensY = mouseLook.YSensitivity;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedInToggle == false)
            {
                zoomedInToggle = true;
                mainCamera.fieldOfView = zoomedIn;

                mouseLook.XSensitivity = basicSensX * sensIn;
                mouseLook.YSensitivity = basicSensY * sensIn;
            }
            else
            {
                zoomedInToggle = false;
                mainCamera.fieldOfView = zoomedOut;

                mouseLook.XSensitivity = basicSensX * sensOut;
                mouseLook.YSensitivity = basicSensY * sensOut;
            }
        }
    }
}
