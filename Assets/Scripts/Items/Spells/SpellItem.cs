using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class SpellItem : Item
{
    public SpellItem(int id)
    {
        Table_Item_Spell.Info data = Managers.Table.m_Item_Spell.Get(id);

        if (data == null)
            return;

        m_iItemID = data.m_iID;
        m_ItemName = data.m_sName;
        m_ItemIcon = Managers.Resource.Load<Sprite>(data.m_sIconPath);
        m_sItemDescription = data.m_sItem_Decription;
        m_eSpellType = (E_SpellType)data.m_iSpell_Type;
        m_iCostFP = data.m_iCost_FP;
        m_iMagicUseSlot = data.m_iSlots;
        m_iAttributeRequirementIntelligence = data.m_iRequirment_Intelligence;
        m_iAttributeRequirementFaith = data.m_iRequirment_Faith;

        m_eItemType = Define.E_ItemType.Magic;

        SetFX();
        SetAnimationName();
        SetSound();
    }

    public SpellItem()
    {

    }

    [Header("Current Count")]
    public int m_iCurrentCount; // 현재 소지 가능한 수
    public int m_iMaxCount; // 최대 소지 가능한 수

    [Header("Save Count")]
    public int m_iCurrentSaveCount; // 현재 저장한 수
    public int m_iMaxSaveCount; // 최대 저장한 가능한 수

    [Header("Spell Description")]
    public string m_sItemDescription;

    [Header("Requirement Ability")] // 필요 능력치 F = 1, E = 2, D = 3, C = 4, B = 5, A = 6
    public int m_iAttributeRequirementIntelligence;
    public int m_iAttributeRequirementFaith;

    [Header("Spell Cost")]
    public int m_iCostFP;

    [Header("Cost Slots")]
    public int m_iMagicUseSlot;

    [Header("Game Models")]
    public GameObject spellWarmUpFX;
    public GameObject spellCastFX;

    public string m_sSpellAnimation;

    [Header("Spell Type")]
    public E_SpellType m_eSpellType;

    [Header("Projectile Damage")]
    public float baseDamage;

    [Header("Projectile Physics")]
    public float projectileForwardVelocity;
    public float projectileUpwardVelocity;
    public float projectileMass;
    public bool isEffectedByGravity;
    public Rigidbody rigidBody;

    public int healAmount;

    [Header("Sound")]
    public string m_sAttemptToCastSpellSound;
    public string m_sSuccessfullyCastSpellSound;

    #region AttemptToCastSpell
    public virtual void AttemptToCastSpell(CharacterManager character)
    {
        if (m_iItemID == 101)
            Heal_AttemptToCastSpell(character);
        if (m_iItemID == 102)
            FireBall_AttemptToCastSpell(character);

        // Sound
        Managers.Sound.Play(m_sAttemptToCastSpellSound);
    }

    void Heal_AttemptToCastSpell(CharacterManager character)
    {
        GameObject instantiateWarmUpSpellFX = Instantiate(spellWarmUpFX, character.characterAnimatorManager.transform);
        character.characterAnimatorManager.PlayTargetAnimation(m_sSpellAnimation, true, false, character.isUsingLeftHand);
    }

    void FireBall_AttemptToCastSpell(CharacterManager character)
    {
        if (character.isUsingLeftHand)
        {
            GameObject instantiatedWarmUpSellFX = Instantiate(spellWarmUpFX, character.characterWeaponSlotManager.leftHandSlot.transform);
            //instantiatedWarmUpSellFX.gameObject.transform.localScale = new Vector3(100, 100, 100);
            character.characterAnimatorManager.PlayTargetAnimation(m_sSpellAnimation, true, false, character.isUsingLeftHand);
        }
        else
        {
            GameObject instantiatedWarmUpSellFX = Instantiate(spellWarmUpFX, character.characterWeaponSlotManager.rightHandSlot.transform);
            //instantiatedWarmUpSellFX.gameObject.transform.localScale = new Vector3(100, 100, 100);
            character.characterAnimatorManager.PlayTargetAnimation(m_sSpellAnimation, true, false, character.isUsingLeftHand);
        }


    }

    #endregion

    #region SuccessfullyCastSpell

    public virtual void SuccessfullyCastSpell(CharacterManager character)
    {
        PlayerManager player = character as PlayerManager;

        if(player != null)
        {
            player.playerStatsManager.DeductFocusPoints(m_iCostFP);
        }

        if (m_iItemID == 101)
            Heal_SuccessfullyCastSpell(character);
        if (m_iItemID == 102)
            FireBall_SuccessfullyCastSpell(character);

        // Sound
        Managers.Sound.Play(m_sSuccessfullyCastSpellSound);
    }

    void Heal_SuccessfullyCastSpell(CharacterManager character)
    {
        GameObject instantiateSpellFX = Instantiate(spellCastFX, character.characterAnimatorManager.transform);
        character.characterStatsManager.HealCharacter(healAmount);
    }

    void FireBall_SuccessfullyCastSpell(CharacterManager character)
    {
        PlayerManager player = character as PlayerManager;

        // player
        if (player != null)
        {
            GameObject instantiatedSpellFX = null;

            if (player.isUsingLeftHand)
                instantiatedSpellFX = Instantiate(spellCastFX, player.playerWeaponSlotManager.leftHandSlot.transform.position, player.cameraHandler.cameraPivotTranform.rotation);
            else
                instantiatedSpellFX = Instantiate(spellCastFX, player.playerWeaponSlotManager.rightHandSlot.transform.position, player.cameraHandler.cameraPivotTranform.rotation);

            SpellDamageCollider spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
            spellDamageCollider.characterManager = character;
            spellDamageCollider.m_isCanCollide = true;
            rigidBody = instantiatedSpellFX.GetComponent<Rigidbody>();

            if (player.cameraHandler.m_trCurrentLockOnTarget != null)
            {
                instantiatedSpellFX.transform.LookAt(player.cameraHandler.m_trCurrentLockOnTarget.transform);
            }
            else
            {
                instantiatedSpellFX.transform.rotation = Quaternion.Euler(player.cameraHandler.cameraPivotTranform.eulerAngles.x, player.playerStatsManager.transform.eulerAngles.y, 0);
            }

            rigidBody.AddForce(instantiatedSpellFX.transform.forward * projectileForwardVelocity);
            rigidBody.AddForce(instantiatedSpellFX.transform.up * projectileUpwardVelocity);
            rigidBody.useGravity = isEffectedByGravity;
            rigidBody.mass = projectileMass;
            rigidBody.constraints = RigidbodyConstraints.None;
            instantiatedSpellFX.transform.parent = null;
        }

        // A.I
        else
        {

        }
    }

    #endregion

    void SetFX()
    {
        if(m_iItemID == 101) // HEAL
        {
            spellWarmUpFX = Managers.Resource.Load<GameObject>("Art/Models/Items/Spell/Heal_Spell_Cast_Fx");
            spellCastFX = Managers.Resource.Load<GameObject>("Art/Models/Items/Spell/Heal_Spell_Warm_Up_Fx");
        }
        else if (m_iItemID == 102) // Fire Ball
        {
            spellWarmUpFX = Managers.Resource.Load<GameObject>("Art/Models/Items/Projectile/FireBall_WARM_UP");
            spellCastFX = Managers.Resource.Load<GameObject>("Art/Models/Items/Projectile/FireBall_OBJECT");
        }

    }

    void SetAnimationName()
    {
        if (m_iItemID == 101) // HEAL
        {
            m_sSpellAnimation = "Spell_Heal";
        }
        else if (m_iItemID == 102) // Fire Ball
        {
            m_sSpellAnimation = "Spell_Throw_Start";
        }
    }

    void SetSound()
    {
        if (m_iItemID == 101) // HEAL
        {
            m_sAttemptToCastSpellSound = "Item/Magic/Spell_AttemptToCastSpell_Heal";
            m_sSuccessfullyCastSpellSound = "Item/Magic/Spell_SuccessfullyCastSpell_Heal";
        }
        else if (m_iItemID == 102) // Fire Ball
        {
            m_sAttemptToCastSpellSound = "Item/Magic/Spell_AttemptToCastSpell_Fire";
            m_sSuccessfullyCastSpellSound = "Item/Magic/Spell_SuccessfullyCastSpell_Fire";
        }
    }


}
