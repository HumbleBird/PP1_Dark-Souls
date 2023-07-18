using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Actions/Fire Arrow Action")]
public class FireArrowAction : ItemAction
{
    public override void PerformAction(PlayerManager player)
    {
        // live arrow을 생성할 위치 찾기
        ArrowInstantiationLocation arrowInstantiationLocation;
        arrowInstantiationLocation = player.playerWeaponSlotManager.rightHandSlot.GetComponentInChildren<ArrowInstantiationLocation>();

        // Fire the arrow의 애니메이션 실행
        Animator bowAnimator = player.playerWeaponSlotManager.rightHandSlot.GetComponentInChildren<Animator>();
        bowAnimator.SetBool("isDrawn", false);
        bowAnimator.Play("BowObject_TH_Fire_01");
        Destroy(player.playerEffectsManager.currentRangeFX);

        // reset 플레이어 holding arrow flag
        player.playerAnimatorManager.PlayerTargetAnimation("Bow_TH_Fire_01", true);
        player.animator.SetBool("isHoldingArrow", false);

        // live arrow 생성
        GameObject liveArrow = Instantiate(player.playerInventoryManager.currentAmmo.liveAmmoModel, arrowInstantiationLocation.transform.position, player.cameraHandler.cameraPivotTranform.rotation);
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

        rigidBody.AddForce(liveArrow.transform.forward * player.playerInventoryManager.currentAmmo.forwardVelocity);
        rigidBody.AddForce(liveArrow.transform.up * player.playerInventoryManager.currentAmmo.upwardVelocity);
        rigidBody.useGravity = player.playerInventoryManager.currentAmmo.useGravity;
        rigidBody.mass = player.playerInventoryManager.currentAmmo.ammoMass;
        liveArrow.transform.parent = null;

        // damage collider set
        damageCollider.characterManager = player;
        damageCollider.ammoItem = player.playerInventoryManager.currentAmmo;
        damageCollider.physicalDamage = player.playerInventoryManager.currentAmmo.physicalDamage;
    }
}
