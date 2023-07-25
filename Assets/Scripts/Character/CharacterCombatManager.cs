using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterCombatManager : MonoBehaviour
{
    CharacterManager character;

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
}
