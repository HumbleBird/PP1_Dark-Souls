using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class EnemyStatus : CharacterStatus
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public  void TakeDamage(int damage)
    {
        if (isDead)
            return;

        currentHealth = currentHealth - damage;

        animator.Play("Damage_01");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            animator.Play("Dead_01");
            isDead = true;
        }
    }
}
