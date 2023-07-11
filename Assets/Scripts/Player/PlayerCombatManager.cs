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

    public void HandleHoldRBAction()
    {
        if(playerManager.isTwoHandingWeapon)
        {
            PerformRBRangedAction();
        }
        else
        {

        }
    }

    #region Input Actions
    public void HandleRBAction()
    {
            PerformMagicAction(playerInventoryManager.rightWeapon, false);
    }


    public void HandleLBAction()
    {

        if(playerManager.isTwoHandingWeapon)
        {
            if(playerInventoryManager.rightWeapon.weaponType == WeaponType.Bow)
            {
                PerformLBAimingAction();
            }
        }
        else
        {
            if (playerInventoryManager.leftWeapon.weaponType == WeaponType.Shield ||
                playerInventoryManager.leftWeapon.weaponType == WeaponType.StraightSwords)
            {

            }
            else if (playerInventoryManager.leftWeapon.weaponType == WeaponType.FaithCaster ||
                playerInventoryManager.leftWeapon.weaponType == WeaponType.PyroCaster)
            {
                PerformMagicAction(playerInventoryManager.leftWeapon, true);
                playerAnimatorManager.animator.SetBool("isUsingLeftHand", true);
            }
        }
    }

    public void HandleLTAction()
    {
        if(playerInventoryManager.leftWeapon.weaponType == WeaponType.Shield || playerInventoryManager.rightWeapon.weaponType == WeaponType.Unarmed)
        {
            PerformLTWeaponArt(inputHandler.twoHandFlag);
        }
        else if (playerInventoryManager.leftWeapon.weaponType == WeaponType.StraightSwords)
        {

        }
    }

    private void DrawArrowAction()
    {
        playerAnimatorManager.animator.SetBool("isHoldingArrow", true);
        playerAnimatorManager.PlayerTargetAnimation("Bow_TH_Draw_01", true);
        GameObject loadedArrow = Instantiate(playerInventoryManager.currentAmmo.loadedItemModel, playerWeaponSlotManager.rightHandSlot.transform);

        //Animate the bow
        Animator bowAnimator = playerWeaponSlotManager.rightHandSlot.GetComponentInChildren<Animator>();
        bowAnimator.SetBool("isDrawn", true);
        bowAnimator.Play("BowObject_TH_Draw_01");

        playerEffectsManager.currentRangeFX = loadedArrow;
    }

    public void FireArrowAction()
    {
        // live arrow을 생성할 위치 찾기
        ArrowInstantiationLocation arrowInstantiationLocation;
        arrowInstantiationLocation = playerWeaponSlotManager.rightHandSlot.GetComponentInChildren<ArrowInstantiationLocation>();

        // Fire the arrow의 애니메이션 실행
        Animator bowAnimator = playerWeaponSlotManager.rightHandSlot.GetComponentInChildren<Animator>();
        bowAnimator.SetBool("isDrawn", false);
        bowAnimator.Play("BowObject_TH_Fire_01");
        Destroy(playerEffectsManager.currentRangeFX);

        // reset 플레이어 holding arrow flag
        playerAnimatorManager.PlayerTargetAnimation("Bow_TH_Fire_01", true);
        playerAnimatorManager.animator.SetBool("isHoldingArrow", false);

        // live arrow 생성
        GameObject liveArrow = Instantiate(playerInventoryManager.currentAmmo.liveAmmoModel, arrowInstantiationLocation.transform.position, cameraHandler.cameraPivotTranform.rotation);
        Rigidbody rigidBody = liveArrow.GetComponentInChildren<Rigidbody>();
        RangedProjectileDamageCollider damageCollider = liveArrow.GetComponentInChildren<RangedProjectileDamageCollider>();

        if(playerManager.isAiming)
        {
            Ray ray = cameraHandler.cameraObject.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hitPoint;

            if(Physics.Raycast(ray, out hitPoint, 100.0f))
            {
                liveArrow.transform.LookAt(hitPoint.point);
                Debug.Log(hitPoint.transform.name);
            }
            else
            {
                liveArrow.transform.rotation = Quaternion.Euler(cameraHandler.cameraTransform.localEulerAngles.x, playerManager.lockOnTransform.eulerAngles.y, 0);
            }
        }
        else
        {
            // live arrow 속도
            if (cameraHandler.m_trCurrentLockOnTarget != null)
            {
                Quaternion arrowRotation = Quaternion.LookRotation(cameraHandler.m_trCurrentLockOnTarget.lockOnTransform.position - liveArrow.gameObject.transform.position);
                liveArrow.transform.rotation = arrowRotation;
            }
            else
            {
                liveArrow.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTranform.eulerAngles.x, playerManager.lockOnTransform.eulerAngles.y, 0);
            }
        }






        rigidBody.AddForce(liveArrow.transform.forward * playerInventoryManager.currentAmmo.forwardVelocity);
        rigidBody.AddForce(liveArrow.transform.up * playerInventoryManager.currentAmmo.upwardVelocity);
        rigidBody.useGravity = playerInventoryManager.currentAmmo.useGravity;
        rigidBody.mass = playerInventoryManager.currentAmmo.ammoMass;
        liveArrow.transform.parent = null;

        // damage collider set
        damageCollider.characterManager = playerManager;
        damageCollider.ammoItem = playerInventoryManager.currentAmmo;
        damageCollider.physicalDamage = playerInventoryManager.currentAmmo.physicalDamage;
    }


    #endregion

    #region Attack Actions


    private void PerformRBRangedAction()
    {
        if (playerStatsManager.currentStamina <= 0)
            return;

        playerAnimatorManager.EraseHandIKForWeapon();
        playerAnimatorManager.animator.SetBool("isUsingRightHand", true);

        if(!playerManager.isHoldingArrow)
        {
            if(playerInventoryManager.currentAmmo != null)
            {
                DrawArrowAction();
            }
            else
            {
                playerAnimatorManager.PlayerTargetAnimation("Shrug", true);
            }
        }
    }


    private void PerformLBAimingAction()
    {
        if (playerManager.isAiming)
            return;

        inputHandler.uiManager.crossHair.SetActive(true);
        playerManager.isAiming = true;
    }

    private void PerformMagicAction(WeaponItem weapon, bool isLeftHaned)
    {
        if (playerManager.isInteracting)
            return;

        if (weapon.weaponType == WeaponType.FaithCaster)
        {
            if(playerInventoryManager.currentSpell != null && playerInventoryManager.currentSpell.isFaithSpell)
            {
                // CHECK FOR FP
                if (playerStatsManager.currentFocusPoints >= playerInventoryManager.currentSpell.focusPointCost)
                {
                    playerInventoryManager.currentSpell.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, playerWeaponSlotManager, isLeftHaned);
                }
                else
                    playerAnimatorManager.PlayerTargetAnimation("Shrug", true);
            }
        }
        else if (weapon.weaponType == WeaponType.PyroCaster)
        {
            if (playerInventoryManager.currentSpell != null && playerInventoryManager.currentSpell.isPyroSpell)
            {
                // CHECK FOR FP
                if (playerStatsManager.currentFocusPoints >= playerInventoryManager.currentSpell.focusPointCost)
                {
                    playerInventoryManager.currentSpell.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, playerWeaponSlotManager, isLeftHaned);
                }
                else
                    playerAnimatorManager.PlayerTargetAnimation("Shrug", true);
            }
        }
    }

    private void PerformLTWeaponArt(bool isTwoHanding)
    {
        if (playerManager.isInteracting)
            return;

        if (isTwoHanding)
        {

        }
        else
        {
            playerAnimatorManager.PlayerTargetAnimation(weapon_art, true);


        }
    }

    private void SuccessfullyCastSpell()
    {
        playerInventoryManager.currentSpell.SuccessfullyCastSpell
            (playerAnimatorManager, playerStatsManager, cameraHandler, playerWeaponSlotManager, playerManager.isUsingLeftHand);
        playerAnimatorManager.animator.SetBool("isFiringSpell", true);
    }

    #endregion


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
