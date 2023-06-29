using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsManager : MonoBehaviour
{
    CharacterStatsManager characterStatsManager;

    [Header("Damage FX")]
    public GameObject bloodSplatterFX;

    [Header("Weapon FX")]
    public WeaponFX rightWeaponFX;
    public WeaponFX leftWeaponFX;

    [Header("Poison")]
    public bool isPoisoned;
    public float poisonBuildup = 0; // 독 수치 100에 도달한 후 플레이어를 중독시키는 시간 경과에 따른 빌드업
    public float poisonAmount = 100; // 플레이어가 무독화되기 전에 처리해야 하는 독의 양
    public float defaultPoisonAmount; // 중독되기까지의 독 수치 기본 값
    public float poisonTimer = 2; // 독 피해 사이의 시간 간격
    public int poisonDamage = 1;
    float timer;

    protected virtual void Awake()
    {
        characterStatsManager = GetComponent<CharacterStatsManager>();
    }

    public virtual void PlayWeaponFX(bool isLeft)
    {
        if(isLeft == false)
        {
            if(rightWeaponFX != null)
            {
                rightWeaponFX.PlayWeaponFX();
            }
        }
        else
        {
            if (leftWeaponFX != null)
            {
                leftWeaponFX.PlayWeaponFX();
            }
        }
    }

    public virtual void PlayBloodSplatterFX(Vector3 bloodSplatterLocation)
    {
        GameObject blood = Instantiate(bloodSplatterFX, bloodSplatterLocation, Quaternion.identity);
    }

    public virtual void HandleAllBuildUpEffects()
    {
        if (characterStatsManager.isDead)
            return;

        HandlePoisonBuildUp();
        HandleIsPoisonedEffect();
    }

    protected virtual void HandlePoisonBuildUp()
    {
        if (isPoisoned)
            return;

        if(poisonBuildup > 0 && poisonBuildup < 100)
        {
            poisonBuildup = poisonBuildup - 1 * Time.deltaTime;
        }
        else if (poisonBuildup >= 100)
        {
            isPoisoned = true;
            poisonBuildup = 0;
        }
    }

    protected virtual void HandleIsPoisonedEffect()
    {
        if(isPoisoned)
        {
            if(poisonAmount > 0 )
            {
                timer += Time.deltaTime;

                if(timer >= poisonTimer)
                {
                    characterStatsManager.TakePoisonDamage(poisonDamage);
                    timer = 0;
                }

                poisonAmount = poisonAmount - 1 * Time.deltaTime;
            }
            else
            {
                isPoisoned = false;
                poisonAmount = defaultPoisonAmount;
            }
        }
    }
}
