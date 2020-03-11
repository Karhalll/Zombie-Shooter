using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera = null;

    [Header("Assets")]
    [SerializeField] ParticleSystem muzzleFlashVFXPrefab = null;
    [SerializeField] GameObject hitEffectPrefab = null;
    [SerializeField] float hitEffectDeathTime = 0.2f;
    [SerializeField] TextMeshProUGUI ammoUIText = null;

    [Header("Stats")]
    [SerializeField] AmmoType ammoType = AmmoType.Bullets;
    [SerializeField] float damage = 30f;
    [SerializeField] float range = 100f;
    [SerializeField] float timeBetwenShoots = 0.5f;

    bool canShoot = true;
    Ammo ammoSlot;

    private void OnEnable() 
    {
        canShoot = true;
    }

    private void Awake() 
    {
        ammoSlot = GameObject.FindGameObjectWithTag("Player").GetComponent<Ammo>();
    }

    void Update()
    {
        UpdateAmmoUI();
        if (Input.GetButtonDown("Fire1") && canShoot == true)
        {
            StartCoroutine(Shoot());
        }  
    }

    private void UpdateAmmoUI()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoUIText.text = currentAmmo.ToString("# ###");
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (HasAmmo()) 
        { 
            ammoSlot.ReduceCurrentAmmo(ammoType);
            PlayMuzzleFlash();
            ProccesRaycast(); 
        }
        yield return new WaitForSeconds(timeBetwenShoots);
        canShoot = true;
    }

    private bool HasAmmo()
    {
        if (ammoSlot.GetCurrentAmmo(ammoType) <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlashVFXPrefab.Play();
    }

    private void ProccesRaycast()
    {
        RaycastHit hit;

        bool hitSmthing = Physics.Raycast(
            FPCamera.transform.position,
            FPCamera.transform.forward,
            out hit,
            range
        );

        if (hitSmthing)
        {
            CreateHitImpact(hit);

            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject hitEffect = Instantiate(
            hitEffectPrefab,
            hit.point, 
            Quaternion.LookRotation(hit.normal)
        );

        Destroy(hitEffect, hitEffectDeathTime);
    }
}
