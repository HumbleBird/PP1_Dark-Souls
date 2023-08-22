using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterCombatManager : MonoBehaviour
{
    CharacterManager character;

    [Header("Combat Trasnforms")]
    public Transform backstabReceiverTransform;
    public Transform ripostedReceiverTransform;

    public LayerMask characterLayer;
    public float criticalAttackRange = 0.7f;

    [Header("Last Amount Of Poise Damage Taken")]
    public int previousPoiseDamageTaken;

    [Header("Attack Type")]
    public AttackType currentAttackType;

    [Header("Attack Animations")]
    public string oh_light_attack_01 = "OH_Light_Attack_01";
    public string oh_light_attack_02 = "OH_Light_Attack_02";
    public string oh_heavy_attack_01 = "OH_Heavy_Attack_01";
    public string oh_heavy_attack_02 = "OH_Heavy_Attack_02";
    public string oh_running_attack_01 = "OH_Running_Attack_01";
    public string oh_jumping_attack_01 = "OH_Jumping_Attack_01";

    public string oh_charge_attack_01 = "OH_Charging_Attack_Charge_01";
    public string oh_charge_attack_02 = "OH_Charging_Attack_Charge_02";

    public string th_light_attack_01 = "TH_Light_Attack_01";
    public string th_light_attack_02 = "TH_Light_Attack_02";
    public string th_heavy_attack_01 = "TH_Heavy_Attack_01";
    public string th_heavy_attack_02 = "TH_Heavy_Attack_02";
    public string th_running_attack_01 = "TH_Running_Attack_01";
    public string th_jumping_attack_01 = "TH_Jumping_Attack_01";

    public string th_charge_attack_01 = "TH_Charging_Attack_Charge_01";
    public string th_charge_attack_02 = "TH_Charging_Attack_Charge_02";

    public string weapon_art = "Weapon Art";
    public int pendingCriticalDamage;
    public string lastAttack;



    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public virtual void SetBlockingAbsorptionsFromBlockingWeapon()
    {
        if (character.isUsingRightHand)
        {
            character.characterStatsManager.blockingPhysicalDamageAbsorption = character.characterInventoryManager.rightWeapon.physicalBlockingDamageAbsorption;
            character.characterStatsManager.blockingFireDamageAbsorption = character.characterInventoryManager.rightWeapon.fireBlockingDamageAbsorption;
            character.characterStatsManager.blockingStabilityRating = character.characterInventoryManager.rightWeapon.stability;
        }
        else if (character.isUsingLeftHand)
        {
            character.characterStatsManager.blockingPhysicalDamageAbsorption = character.characterInventoryManager.leftWeapon.physicalBlockingDamageAbsorption;
            character.characterStatsManager.blockingFireDamageAbsorption = character.characterInventoryManager.leftWeapon.fireBlockingDamageAbsorption;
            character.characterStatsManager.blockingStabilityRating = character.characterInventoryManager.leftWeapon.stability;
        }
    }

    public virtual void DrainStaminaBasedOnAttack()
    {
        // 여기에 공격 스테미너 감소 코드를 넣고 싶다면 넣어도 됨
    }

    public virtual void AttemptBlock(DamageCollider attackingWeapon, float physicalDamage, float fireDamage, string blockAnimation)
    {
        float staminaDamageAbsorption = ((physicalDamage + fireDamage) * attackingWeapon.guardBreakModifier) *
            (character.characterStatsManager.blockingStabilityRating / 100);

        float staminaDamage = ((physicalDamage + fireDamage) * attackingWeapon.guardBreakModifier) - staminaDamageAbsorption;

        character.characterStatsManager.currentStamina -= staminaDamage;

        if(character.characterStatsManager.currentStamina <= 0 )
        {
            character.isBlocking = false;
            character.characterAnimatorManager.PlayTargetAnimation("Guard_Break_01", true);
        }
        else
        {
            character.characterAnimatorManager.PlayTargetAnimation(blockAnimation, true);
        }
    }

    private void SuccessfullyCastSpell()
    {
        character.characterInventoryManager.currentSpell.SuccessfullyCastSpell(character);
        character.animator.SetBool("isFiringSpell", true);
    }

    public void AttemptBackStabOrRiposte()
    {
        if (character.isInteracting)
            return;

        if (character.characterStatsManager.currentStamina <= 0)
            return;

        RaycastHit hit;

        if(Physics.Raycast(character.criticalAttackRayCastStartPoint.transform.position, character.transform.TransformDirection(Vector3.forward),
            out hit, criticalAttackRange, characterLayer))
        {
            CharacterManager enemyCharacter = hit.transform.GetComponent<CharacterManager>();
            Vector3 directionFromChracterToEnemy = transform.position - enemyCharacter.transform.position;
            float dotValue = Vector3.Dot(directionFromChracterToEnemy, enemyCharacter.transform.forward);

            Debug.Log("Current Dot value is " + dotValue);

            if(enemyCharacter.canBeRiposted)
            {
                // Attempt Ripsote
                if(dotValue <= 1.2f && dotValue >= 0.6f)
                {
                    AttemptRipste(hit);
                    return;
                }
            }    

            // Attempt backstab
            if(dotValue >= -0.7 && dotValue <= -0.6f)
            {
                AttemptBackStab(hit);
            }
        }
    }

    IEnumerator ForceMoveCharacterToEnemyBackStabPosition (CharacterManager characterPerformingBackStab)
    {
        for (float timer = 0.05f; timer < 0.5f; timer = timer + 0.05f)
        {
            Quaternion backstabRotation = Quaternion.LookRotation(characterPerformingBackStab.transform.forward); 
            transform.rotation = Quaternion.Slerp(transform.rotation, backstabRotation, 1);
            transform.parent = characterPerformingBackStab.characterCombatManager.backstabReceiverTransform;
            transform.localPosition = characterPerformingBackStab.characterCombatManager.backstabReceiverTransform.localPosition;
            transform.parent = null;
            Debug.Log("Running corountine");
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator ForceMoveCharacterToEnemyRiposetebPosition (CharacterManager characterPerformingRiposte)
    {
        for (float timer = 0.05f; timer < 0.5f; timer = timer + 0.05f)
        {
            Quaternion backstabRotation = Quaternion.LookRotation(-characterPerformingRiposte.transform.forward); 
            transform.rotation = Quaternion.Slerp(transform.rotation, backstabRotation, 1);
            transform.parent = characterPerformingRiposte.characterCombatManager.ripostedReceiverTransform;
            transform.localPosition = characterPerformingRiposte.characterCombatManager.ripostedReceiverTransform.localPosition;
            transform.parent = null;
            Debug.Log("Running corountine");
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void GetBackStabbed (CharacterManager characterPerformingBackStab)
    {
        //PLAY SOUND FX
        character.isBeingBackstabbed = true;

        StartCoroutine(ForceMoveCharacterToEnemyBackStabPosition(characterPerformingBackStab));

        //FORCE LOCK POSITION
        character.characterAnimatorManager.PlayTargetAnimation("Back_Stabbed_01", true);
    }

    public void GetRiposeted(CharacterManager characterPerformingBackStab)
    {
        //PLAY SOUND FX
        character.isBeingRiposted = true;

        StartCoroutine(ForceMoveCharacterToEnemyRiposetebPosition(characterPerformingBackStab));

        //FORCE LOCK POSITION
        character.characterAnimatorManager.PlayTargetAnimation("Riposted_01", true);
    }

    private void AttemptBackStab(RaycastHit hit)
    {
        CharacterManager enemycharacter = hit.transform.GetComponent<CharacterManager>();

        if (enemycharacter != null)
        {
            if (!enemycharacter.isBeingBackstabbed || !enemycharacter.isBeingRiposted)
            {
                //We make it so the enemy cannot be damaged whilst being critically damaged EnableIsInvulnerable();
                character.isPerformingBackstab = true;
                character.characterAnimatorManager.EraseHandIKForWeapon();

                character.characterAnimatorManager.PlayTargetAnimation("Back_Stab_01", true);

                float criticalDamage = (character.characterInventoryManager.rightWeapon.criticalDamagemuiltiplier *
                    (character.characterInventoryManager.rightWeapon.physicalDamage +
                    character.characterInventoryManager.rightWeapon.fireDamage));

                int roundedCriticalDamage = Mathf.RoundToInt(criticalDamage);
                enemycharacter.characterCombatManager.pendingCriticalDamage = roundedCriticalDamage;
                enemycharacter.characterCombatManager.GetBackStabbed(character);
            }
        }
    }

    private void AttemptRipste(RaycastHit hit)
    {
        CharacterManager enemycharacter = hit.transform.GetComponent<CharacterManager>();

        if(enemycharacter != null)
        {
            if(!enemycharacter.isBeingBackstabbed || !enemycharacter.isBeingRiposted)
            {
                //We make it so the enemy cannot be damaged whilst being critically damaged EnableIsInvulnerable();
                character.isPerformingRipost = true;
                character.characterAnimatorManager.EraseHandIKForWeapon();
                
                character.characterAnimatorManager.PlayTargetAnimation("Riposte_01", true);

                float criticalDamage = (character.characterInventoryManager.rightWeapon.criticalDamagemuiltiplier *
                    (character.characterInventoryManager.rightWeapon.physicalDamage +
                    character.characterInventoryManager.rightWeapon.fireDamage));

                int roundedCriticalDamage = Mathf.RoundToInt(criticalDamage);
                enemycharacter.characterCombatManager.pendingCriticalDamage = roundedCriticalDamage;
                enemycharacter.characterCombatManager.GetRiposeted(character);
            }
        }
    }

    private void EnableIsInvulnerable()
    {
        character.animator.SetBool("isInvulnerable", true);
    }

    private void ApplyPendingDamage()
    {
        character.characterStatsManager.TakeDamageNoAnimation(pendingCriticalDamage, 0);
    }

    public void EnableCanBeParried()
    {
        character.canBeParryied = true;
    }

    public void DisableCanbeParried()
    {
        character.canBeParryied = false;

    }
}
