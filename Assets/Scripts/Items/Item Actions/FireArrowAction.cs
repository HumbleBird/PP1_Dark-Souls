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

        // Fire the Arrow as a player character
        if(player != null)
        {
            // live arrow 생성
            GameObject liveArrow = Instantiate(character.characterInventoryManager.currentAmmo.liveAmmoModel, arrowInstantiationLocation.transform.position, player.cameraHandler.cameraPivotTranform.rotation);
            Rigidbody rigidBody = liveArrow.GetComponentInChildren<Rigidbody>();
            RangedProjectileDamageCollider damageCollider = liveArrow.GetComponentInChildren<RangedProjectileDamageCollider>();

            if (player.isAiming)
            {
                Ray ray = player.cameraHandler.cameraObject.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hitPoint;

                if (Physics.Raycast(ray, out hitPoint, 100.0f))
                {
                    liveArrow.transform.LookAt(hitPoint.point);
                    Debug.Log(hitPoint.transform.name);
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

            rigidBody.AddForce(liveArrow.transform.forward * character.characterInventoryManager.currentAmmo.forwardVelocity);
            rigidBody.AddForce(liveArrow.transform.up * character.characterInventoryManager.currentAmmo.upwardVelocity);
            rigidBody.useGravity = character.characterInventoryManager.currentAmmo.useGravity;
            rigidBody.mass = character.characterInventoryManager.currentAmmo.ammoMass;
            liveArrow.transform.parent = null;

            // damage collider set
            damageCollider.characterManager = character;
            damageCollider.ammoItem = character.characterInventoryManager.currentAmmo;
            damageCollider.physicalDamage = character.characterInventoryManager.currentAmmo.physicalDamage;
        }
        // Fire the Arrow as an A.I character

        else
        {
            EnemyManager enemy = character as EnemyManager;

            // live arrow 생성
            GameObject liveArrow = Instantiate
                (character.characterInventoryManager.currentAmmo.liveAmmoModel, 
                arrowInstantiationLocation.transform.position, 
                Quaternion.identity);
            Rigidbody rigidBody = liveArrow.GetComponentInChildren<Rigidbody>();
            RangedProjectileDamageCollider damageCollider = liveArrow.GetComponentInChildren<RangedProjectileDamageCollider>();

            // live arrow 속도
            if (enemy.currentTarget != null)
            {
                Quaternion arrowRotation = Quaternion.LookRotation
                    (enemy.currentTarget.lockOnTransform.position - liveArrow.gameObject.transform.position);
                liveArrow.transform.rotation = arrowRotation;
            }

            rigidBody.AddForce(liveArrow.transform.forward * enemy.characterInventoryManager.currentAmmo.forwardVelocity);
            rigidBody.AddForce(liveArrow.transform.up * enemy.characterInventoryManager.currentAmmo.upwardVelocity);
            rigidBody.useGravity = enemy.characterInventoryManager.currentAmmo.useGravity;
            rigidBody.mass = enemy.characterInventoryManager.currentAmmo.ammoMass;
            liveArrow.transform.parent = null;

            // damage collider set
            damageCollider.characterManager = enemy;
            damageCollider.ammoItem = enemy.characterInventoryManager.currentAmmo;
            damageCollider.physicalDamage = enemy.characterInventoryManager.currentAmmo.physicalDamage;
            damageCollider.teamIDNumber = enemy.characterStatsManager.teamIDNumber;
        }

        
    }
}
