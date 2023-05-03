using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EnemyStatus : Status
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        animator.Play("Damage_01");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.Play("Dead_01");
        }
    }
}
