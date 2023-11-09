using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Fire Arrow Action")]
public class FireArrowAction : ItemAction
{
    public override void PerformAction(CharacterManager character)
    {
        PlayerManager player = character as PlayerManager;

        // live arrow을 생성할 위치 찾기
        ArrowInstantiationLocation arrowInstantiationLocation;
        arrowInstantiationLocation = character.characterWeaponSlotManager.rightHandSlot.GetComponentInChildren<ArrowInstantiationLocation>();

        // Fire the arrow의 애니메이션 실행
        Animator bowAnimator = character.characterWeaponSlotManager.rightHandSlot.GetComponentInChildren<Animator>();
        bowAnimator.SetBool("isDrawn", false);
        bowAnimator.Play("BowObject_TH_Fire_01");
        Destroy(character.characterEffectsManager.instantiatedFXModel);

        // reset 플레이어 holding arrow flag
        character.characterAnimatorManager.PlayTargetAnimation("Bow_TH_Fire_01", true);
        character.animator.SetBool("isHoldingArrow", false);

        GameObject liveArrow;
        // Fire the Arrow as a player character
        if (player != null)
        {
            // Stamina
            player.characterCombatManager.DrainStaminaBasedOnAttack();

            // live arrow 생성
            liveArrow = Instantiate(character.characterEquipmentManager.m_CurrentHandAmmo.liveAmmoModel, arrowInstantiationLocation.transform.position, player.cameraHandler.cameraPivotTranform.rotation);
            Rigidbody rigidBody = liveArrow.GetComponent<Rigidbody>();
            RangedProjectileDamageCollider damageCollider = liveArrow.GetComponent<RangedProjectileDamageCollider>();

            if (player.isAiming)
            {
                Ray ray = player.cameraHandler.cameraObject.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hitPoint;

                if (Physics.Raycast(ray, out hitPoint, 100.0f))
                {
                    liveArrow.transform.LookAt(hitPoint.point);
                }
                else
                {
                    liveArrow.transform.rotation = Quaternion.Euler(player.cameraHandler.cameraTransform.localEulerAngles.x, player.lockOnTransform.eulerAngles.y, 0);
                }
            }
            else
            {
                // live arrow 속도
                if (player.cameraHandler.m_trCurrentLockOnTarget != null)
                {
                    Quaternion arrowRotation = Quaternion.LookRotation(player.cameraHandler.m_trCurrentLockOnTarget.lockOnTransform.position - liveArrow.gameObject.transform.position);
                    liveArrow.transform.rotation = arrowRotation;
                }
                else
                {
                    liveArrow.transform.rotation = Quaternion.Euler(player.cameraHandler.cameraPivotTranform.eulerAngles.x, player.lockOnTransform.eulerAngles.y, 0);
                }
            }

            rigidBody.AddForce(liveArrow.transform.forward * character.characterEquipmentManager.m_CurrentHandAmmo.forwardVelocity);
            rigidBody.AddForce(liveArrow.transform.up * character.characterEquipmentManager.m_CurrentHandAmmo.upwardVelocity);
            rigidBody.useGravity = character.characterEquipmentManager.m_CurrentHandAmmo.useGravity;
            rigidBody.mass = character.characterEquipmentManager.m_CurrentHandAmmo.ammoMass;
            liveArrow.transform.parent = null;

            // damage collider set
            damageCollider.characterManager = character;
            damageCollider.ammoItem = character.characterEquipmentManager.m_CurrentHandAmmo;
            damageCollider.physicalDamage = character.characterEquipmentManager.m_CurrentHandAmmo.m_iPhysicalDamage;
            damageCollider.ammoItem.m_Shooter = character;

            if (character.isUsingRightHand == true)
                damageCollider.m_BowItem = character.characterEquipmentManager.m_CurrentHandRightWeapon;
            else if (character.isUsingLeftHand == true)
                damageCollider.m_BowItem = character.characterEquipmentManager.m_CurrentHandLeftWeapon;
        }
        // Fire the Arrow as an A.I character

        else
        {
            AICharacterManager enemy = character as AICharacterManager;

            // live arrow 생성
            liveArrow = Instantiate
                (character.characterEquipmentManager.m_CurrentHandAmmo.liveAmmoModel, 
                arrowInstantiationLocation.transform.position, 
                Quaternion.identity);
            Rigidbody rigidBody = liveArrow.GetComponent<Rigidbody>();
            RangedProjectileDamageCollider damageCollider = liveArrow.GetComponent<RangedProjectileDamageCollider>();

            // live arrow 속도
            if (enemy.currentTarget != null)
            {
                Quaternion arrowRotation = Quaternion.LookRotation
                    (enemy.currentTarget.lockOnTransform.position - liveArrow.gameObject.transform.position);
                liveArrow.transform.rotation = arrowRotation;
            }

            rigidBody.AddForce(liveArrow.transform.forward * enemy.characterEquipmentManager.m_CurrentHandAmmo.forwardVelocity);
            rigidBody.AddForce(liveArrow.transform.up * enemy.characterEquipmentManager.m_CurrentHandAmmo.upwardVelocity);
            rigidBody.useGravity = enemy.characterEquipmentManager.m_CurrentHandAmmo.useGravity;
            rigidBody.mass = enemy.characterEquipmentManager.m_CurrentHandAmmo.ammoMass;
            liveArrow.transform.parent = null;

            // damage collider set
            damageCollider.characterManager = enemy;
            damageCollider.ammoItem = enemy.characterEquipmentManager.m_CurrentHandAmmo;
            damageCollider.ammoItem.m_Shooter = character;
            damageCollider.physicalDamage = enemy.characterEquipmentManager.m_CurrentHandAmmo.m_iPhysicalDamage;
            damageCollider.teamIDNumber = enemy.characterStatsManager.teamIDNumber;

            damageCollider.m_BowItem = character.characterEquipmentManager.m_CurrentHandRightWeapon;
        }


        // Sound
        Managers.Sound.Play("Item/Weapon/Bow/Bow_Fire_01");

        Destroy(liveArrow, 5);
    }
}
