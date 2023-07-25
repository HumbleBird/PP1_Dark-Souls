using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterCombatManager : MonoBehaviour
{
    CharacterManager character;

    LayerMask backStabLayer = 1 << 12;
    LayerMask riposteLayer = 1 << 13;

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
            character.characterAnimatorManager.PlayerTargetAnimation("Guard_Break_01", true);
        }
        else
        {
            character.characterAnimatorManager.PlayerTargetAnimation(blockAnimation, true);
        }
    }

    private void SuccessfullyCastSpell()
    {
        character.characterInventoryManager.currentSpell.SuccessfullyCastSpell(character);
        character.animator.SetBool("isFiringSpell", true);
    }

    public void AttemptBackStabOrRiposte()
    {

        if (character.characterStatsManager.currentStamina <= 0)
            return;

        RaycastHit hit;

        // Back Stab
        if (Physics.Raycast(character.criticalAttackRayCastStartPoint.position,
            transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = character.characterWeaponSlotManager.rightHandDamageCollider;

            if (enemyCharacterManager != null)
            {
                // 팀 ID 체크 (적 한테만 할 수 있게)
                character.transform.position = enemyCharacterManager.backStabCollider.criticalDamagerStandPosition.position;

                Vector3 rotationDirection = character.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - character.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(character.transform.rotation, tr, 500 * Time.deltaTime);
                character.transform.rotation = targetRotation;

                int criticalDamage = character.characterInventoryManager.rightWeapon.criticalDamagemuiltiplier * rightWeapon.physicalDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                character.characterAnimatorManager.PlayerTargetAnimation("Back Stab", true);
                enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayerTargetAnimation("Back Stabbed", true);
            }
        }

        // Riposte
        else if (Physics.Raycast(character.criticalAttackRayCastStartPoint.position,
            transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
        {
            // 팀 ID 체크 (적 한테만 할 수 있게)
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = character.characterWeaponSlotManager.rightHandDamageCollider;

            if (enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
            {
                character.transform.position = enemyCharacterManager.riposteCollider.criticalDamagerStandPosition.position;

                Vector3 rotationDirection = character.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - character.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(character.transform.rotation, tr, 500 * Time.deltaTime);
                character.transform.rotation = targetRotation;

                int criticalDamage = character.characterInventoryManager.rightWeapon.criticalDamagemuiltiplier * rightWeapon.physicalDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                character.characterAnimatorManager.PlayerTargetAnimation("Riposte", true);
                enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayerTargetAnimation("Riposted", true);
            }
        }
    }
}
