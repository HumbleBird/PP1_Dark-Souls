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
    [HideInInspector] public string oh_light_attack_01 = "OH_Light_Attack_01";
    [HideInInspector]public string oh_light_attack_02 = "OH_Light_Attack_02";
    [HideInInspector]public string oh_heavy_attack_01 = "OH_Heavy_Attack_01";
    [HideInInspector]public string oh_heavy_attack_02 = "OH_Heavy_Attack_02";
    [HideInInspector]public string oh_running_attack_01 = "OH_Running_Attack_01";
    [HideInInspector]public string oh_jumping_attack_01 = "OH_Jumping_Attack_01";

    [HideInInspector]public string oh_charge_attack_01 = "OH_Charging_Attack_Charge_01";
    [HideInInspector]public string oh_charge_attack_02 = "OH_Charging_Attack_Charge_02";

    [HideInInspector]public string th_light_attack_01 = "TH_Light_Attack_01";
    [HideInInspector]public string th_light_attack_02 = "TH_Light_Attack_02";
    [HideInInspector]public string th_heavy_attack_01 = "TH_Heavy_Attack_01";
    [HideInInspector]public string th_heavy_attack_02 = "TH_Heavy_Attack_02";
    [HideInInspector]public string th_running_attack_01 = "TH_Running_Attack_01";
    [HideInInspector]public string th_jumping_attack_01 = "TH_Jumping_Attack_01";

    [HideInInspector]public string th_charge_attack_01 = "TH_Charging_Attack_Charge_01";
    [HideInInspector]public string th_charge_attack_02 = "TH_Charging_Attack_Charge_02";

    [HideInInspector]public string weapon_art = "Weapon Art";
    [HideInInspector]public int pendingCriticalDamage;
    [HideInInspector] public string lastAttack;



    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
        characterLayer = 1 << 8;
    }

    public virtual void SetBlockingAbsorptionsFromBlockingWeapon()
    {
        if (character.isUsingRightHand)
        {
            character.characterStatsManager.blockingPhysicalDamageAbsorption = character.characterEquipmentManager.m_CurrentHandRightWeapon.physicalBlockingDamageAbsorption;
            character.characterStatsManager.blockingFireDamageAbsorption = character.characterEquipmentManager.m_CurrentHandRightWeapon.fireBlockingDamageAbsorption;
            character.characterStatsManager.blockingStabilityRating = character.characterEquipmentManager.m_CurrentHandRightWeapon.m_iStability;
        }
        else if (character.isUsingLeftHand)
        {
            character.characterStatsManager.blockingPhysicalDamageAbsorption = character.characterEquipmentManager.m_CurrentHandLeftWeapon.physicalBlockingDamageAbsorption;
            character.characterStatsManager.blockingFireDamageAbsorption = character.characterEquipmentManager.m_CurrentHandLeftWeapon.fireBlockingDamageAbsorption;
            character.characterStatsManager.blockingStabilityRating = character.characterEquipmentManager.m_CurrentHandLeftWeapon.m_iStability;
        }
    }

    public virtual void DrainStaminaBasedOnAttack()
    {
        // ���⿡ ���� ���׹̳� ���� �ڵ带 �ְ� �ʹٸ� �־ ��
    }

    private void SuccessfullyCastSpell()
    {
        character.characterEquipmentManager.m_CurrentHandSpell.SuccessfullyCastSpell(character);
        character.animator.SetBool("isFiringSpell", true);
    }

    public void AttemptBackStabOrRiposte()
    {
        if (character.isInteracting)
            return;

        PlayerManager player = character as PlayerManager;
        if(player != null)
        {
            if (player.playerStatsManager.currentStamina <= 0)
                return;
        }

        RaycastHit hit;

        if(Physics.Raycast(character.criticalAttackRayCastStartPoint.transform.position, character.transform.TransformDirection(Vector3.forward),
            out hit, criticalAttackRange, characterLayer))
        {
            CharacterManager enemyCharacter = hit.transform.GetComponent<CharacterManager>();
            Vector3 directionFromChracterToEnemy = transform.position - enemyCharacter.transform.position;
            float dotValue = Vector3.Dot(directionFromChracterToEnemy, enemyCharacter.transform.forward);

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

                float criticalDamage = (character.characterEquipmentManager.m_CurrentHandRightWeapon.criticalDamagemuiltiplier *
                    (character.characterEquipmentManager.m_CurrentHandRightWeapon.m_iPhysicalDamage +
                    character.characterEquipmentManager.m_CurrentHandRightWeapon.m_iFireDamage));

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

                float criticalDamage = (character.characterEquipmentManager.m_CurrentHandRightWeapon.criticalDamagemuiltiplier *
                    (character.characterEquipmentManager.m_CurrentHandRightWeapon.m_iPhysicalDamage +
                    character.characterEquipmentManager.m_CurrentHandRightWeapon.m_iFireDamage));

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
