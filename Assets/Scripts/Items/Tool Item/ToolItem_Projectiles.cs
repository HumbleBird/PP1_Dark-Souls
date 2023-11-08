using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Consumables/Projectiles Item")]
public class ToolItem_Projectiles : ToolItem
{
    public ToolItem_Projectiles(int id)
    {
        SetInfo(id);

        liveBombModel = Managers.Resource.Load<GameObject>("Items/Tool/Firebomb");
    }

    [Header("Velocity")]
    public int upwardVelocity = 5;
    public int forwardVelocity = 15;
    public int bombMass = 1;

    [Header("Live Bomb Model")]
    public GameObject liveBombModel;

    [Header("Base Damage")]
    public int baseDamage = 200;
    public int explosiveDamage = 75;

    public override void AttemptToConsumeItem(PlayerManager player)
    {
        if(m_iCurrentCount > 0 )
        {
            player.playerWeaponSlotManager.rightHandSlot.UnloadWeapon();
            player.playerAnimatorManager.PlayTargetAnimation(m_sPlayAnimationName, true);
            GameObject bombModel = Instantiate(itemModel, player.playerWeaponSlotManager.rightHandSlot.transform.position, Quaternion.identity, player.playerWeaponSlotManager.rightHandSlot.transform);
            bombModel.GetComponentInChildren<BombDamageColider>().characterManager = player;
            player.playerEffectsManager.instantiatedFXModel2 = bombModel;

            Rigidbody rigidBody = bombModel.GetComponentInChildren<Rigidbody>();
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            player.playerAnimatorManager.PlayTargetAnimation("Shrug", true);
        }
    }
}
