using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class PlayerCombatManager : MonoBehaviour
{
    CameraHandler cameraHandler;
    PlayerAnimatorManager playerAnimatorManager;
    PlayerEquipmentManager playerEquipmentManager;
    PlayerStatsManager playerStatsManager;
    PlayerManager playerManager;
    PlayerInventoryManager playerInventoryManager;
    InputHandler inputHandler;
    PlayerWeaponSlotManager playerWeaponSlotManager;
    PlayerEffectsManager playerEffectsManager;

    [Header("Attack Animations")]
    public string oh_light_attack_01 = "OH_Light_Attack_01";
    public string oh_light_attack_02 = "OH_Light_Attack_02";
    public string oh_heavy_attack_01 = "OH_Heavy_Attack_01";
    public string oh_heavy_attack_02 = "OH_Heavy_Attack_02";
    public string oh_running_attack_01 = "OH_Running_Attack_01";
    public string oh_jumping_attack_01 = "OH_Jumping_Attack_01";

    public string th_light_attack_01 = "TH_Light_Attack_01";
    public string th_light_attack_02 = "TH_Light_Attack_02";
    public string th_heavy_attack_01 = "TH_Heavy_Attack_01";
    public string th_heavy_attack_02 = "TH_Heavy_Attack_02";
    public string th_running_attack_01 = "TH_Running_Attack_01";
    public string th_jumping_attack_01 = "TH_Jumping_Attack_01";

    public string weapon_art = "Weapon Art";

    public string lastAttack;

    LayerMask backStabLayer = 1<< 12;
    LayerMask riposteLayer = 1<< 13;

    private void Awake()
    {
        cameraHandler = FindObjectOfType<CameraHandler>();

        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
        playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();

        playerStatsManager = GetComponent<PlayerStatsManager>();
        inputHandler = GetComponent<InputHandler>();
        playerManager = GetComponent<PlayerManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerEffectsManager = GetComponent<PlayerEffectsManager>();
    }

    private void SuccessfullyCastSpell()
    {
        playerInventoryManager.currentSpell.SuccessfullyCastSpell
            (playerAnimatorManager, playerStatsManager, cameraHandler, playerWeaponSlotManager, playerManager.isUsingLeftHand);
        playerAnimatorManager.animator.SetBool("isFiringSpell", true);
    }

    public void AttemptBackStabOrRiposte()
    {

        if (playerStatsManager.currentStamina <= 0)
            return;

        RaycastHit hit;

        // Back Stab
        if(Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position,
            transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = playerWeaponSlotManager.rightHandDamageCollider;

            if(enemyCharacterManager != null)
            {
                // 팀 ID 체크 (적 한테만 할 수 있게)
                playerManager.transform.position = enemyCharacterManager.backStabCollider.criticalDamagerStandPosition.position;

                Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - playerManager.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                playerManager.transform.rotation = targetRotation;

                int criticalDamage = playerInventoryManager.rightWeapon.criticalDamagemuiltiplier * rightWeapon.physicalDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                playerAnimatorManager.PlayerTargetAnimation("Back Stab", true);
                enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayerTargetAnimation("Back Stabbed", true);
            }
        }

        // Riposte
        else if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position,
            transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
        {
            // 팀 ID 체크 (적 한테만 할 수 있게)
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = playerWeaponSlotManager.rightHandDamageCollider;

            if(enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
            {
                playerManager.transform.position = enemyCharacterManager.riposteCollider.criticalDamagerStandPosition.position;

                Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - playerManager.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                playerManager.transform.rotation = targetRotation;

                int criticalDamage = playerInventoryManager.rightWeapon.criticalDamagemuiltiplier * rightWeapon.physicalDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                playerAnimatorManager.PlayerTargetAnimation("Riposte", true);
                enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayerTargetAnimation("Riposted", true);
            }
        }
    }
}
