using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float damage = 40f;

    PlayerHealth targetHealth = null;

    void Start()
    {
        targetHealth = GetComponent<EnemyAI>().GetTarget().GetComponent<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (targetHealth == null) { return; }
        
        targetHealth.TakeDamage(damage);
    }
}
