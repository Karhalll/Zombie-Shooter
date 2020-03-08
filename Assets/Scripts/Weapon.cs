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

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        PlayMuzzleFlash();
        ProccesRaycast();
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
