using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera = null;

    [Header("Assets")]
    [SerializeField] ParticleSystem muzzleFlashVFXPrefab = null;
    [SerializeField] GameObject hitEffectPrefab = null;
    [SerializeField] float hitEffectDeathTime = 0.2f;

    [Header("Stats")]
    [SerializeField] float damage = 30f;
    [SerializeField] float range = 100f;
    [SerializeField] float timeBetwenShoots = 0.5f;

    bool canShoot = true;
    Ammo ammoSlot;

    private void Awake() 
    {
        ammoSlot = GameObject.FindGameObjectWithTag("Player").GetComponent<Ammo>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && canShoot == true)
        {
            StartCoroutine(Shoot());
        }  
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (HasAmmo()) 
        { 
            ammoSlot.ReduceCurrentAmmo();
            PlayMuzzleFlash();
            ProccesRaycast(); 
        }
        yield return new WaitForSeconds(timeBetwenShoots);
        canShoot = true;
    }

    private bool HasAmmo()
    {
        if (ammoSlot.GetCurrentAmmo() <= 0)
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
