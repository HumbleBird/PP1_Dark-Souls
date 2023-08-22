using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterEffectsManager : MonoBehaviour
{
    CharacterManager character;

    [Header("Static Effects")]
    [SerializeField] List<StaticCharacterEffect> staticCharacterEffects;

    [Header("Timed Effects")]
    public List<CharacterEffect> timedEffects;
    [SerializeField] float effectTickTimer = 0;

    [Header("Timed Effect Visual FX")]
    public List<GameObject> timedEffectParticles;

    [Header("Current Range FX")]
    public GameObject instantiatedFXModel;

    [Header("Damage FX")]
    public GameObject bloodSplatterFX;

    [Header("Weapon FX")]
    public WeaponManager rightWeaponManager;
    public WeaponManager leftWeaponManager;

    [Header("Right Weapon Buff")]
    public WeaponBuffEffect rightWeaponBuffEffect;

    [Header("Poison")]
    public Transform buildUpTransform; // �� ��ġ�� build up particle�� ��ȯ

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    protected virtual void Start()
    {
        foreach (var effect in staticCharacterEffects)
        {
            effect.AddStaticEffect(character);
        }
    }

    public virtual void ProcessEffectInstantly(CharacterEffect effect)
    {
        effect.ProcessEffect(character);
    }


    public virtual void ProcessAllTimedEffects()
    {
        effectTickTimer += Time.deltaTime;

        if(effectTickTimer >= 1)
        {
            effectTickTimer = 0;
            ProcessWeaponBuffs();

            // PROCESS ALL ACTIVE EFFECT OVER GAME TIME
            for (int i = timedEffects.Count - 1; i > -1 ; i--)
            {
                timedEffects[i].ProcessEffect(character);
            }

            // DECAYS BUILD UP EFFECT OVER GAME TIME
            ProcessBuildUpDecay();
        }
    }


    public void ProcessWeaponBuffs()
    {
        if(rightWeaponBuffEffect != null)
        {
            rightWeaponBuffEffect.ProcessEffect(character);
        }
    }

    public  void AddStaticEffect(StaticCharacterEffect effect)
    {
        // �ش� ȿ���� ����Ʈ�� �ִ��� üũ

        StaticCharacterEffect staticEffect;

        for (int i = staticCharacterEffects.Count - 1; i > -1 ; i--)
        {
            if(staticCharacterEffects[i] != null)
            {
                if(staticCharacterEffects[i].effectID == effect.effectID)
                {
                    staticEffect = staticCharacterEffects[i];

                    // ĳ���ͷκ��� actual ����Ʈ ����
                    staticEffect.RemoveStaticEffect(character);

                    // �׷� ���� Ȱ�� ȿ�� ��Ͽ��� ȿ���� �����մϴ�
                    staticCharacterEffects.Remove(staticEffect);
                }
            }
        }

        // ����Ʈ�� ����Ʈ ����Ʈ�� ���� �ֱ�
        staticCharacterEffects.Add(effect);

        // ĳ���Ϳ� actual effect�� �߰�
        effect.AddStaticEffect(character);

        // �� ���� �ִ��� üũ
        for (int i = staticCharacterEffects.Count - 1; i > -1; i--)
        {
            if (staticCharacterEffects[i] == null)
            {
                staticCharacterEffects.RemoveAt(i);
            }
        }


    }

    public  void RemoveStaticEffect(int effectID)
    {
        StaticCharacterEffect staticEffect;

        for (int i = staticCharacterEffects.Count - 1; i > -1; i--)
        {
            if (staticCharacterEffects[i] != null)
            {
                if (staticCharacterEffects[i].effectID == effectID)
                {
                    staticEffect = staticCharacterEffects[i];
                    staticEffect.RemoveStaticEffect(character);
                    staticCharacterEffects.Remove(staticEffect);
                }
            }
        }

        for (int i = staticCharacterEffects.Count - 1; i > -1; i--)
        {
            if (staticCharacterEffects[i] == null)
            {
                staticCharacterEffects.RemoveAt(i);
            }
        }
    }

    public virtual void PlayWeaponFX(bool isLeft)
    {
        if(isLeft == false)
        {
            if(rightWeaponManager != null)
            {
                rightWeaponManager.PlayWeaponTrailFX();
            }
        }
        else
        {
            if (leftWeaponManager != null)
            {
                leftWeaponManager.PlayWeaponTrailFX();
            }
        }
    }

    public virtual void PlayBloodSplatterFX(Vector3 bloodSplatterLocation)
    {
        GameObject blood = Instantiate(bloodSplatterFX, bloodSplatterLocation, Quaternion.identity);
    }

    public virtual void InterruptEffect()
    {
        // �ı� ������ effect model(drking estus, having arrow drawn ) ��
        if(instantiatedFXModel != null)
        {
            Destroy(instantiatedFXModel);
        } 
        
        // ȭ���� ������ ���� ���⿡ �ִ� loaded arrow�� �����ߴٸ�
        if(character.isHoldingArrow)
        {
            character.animator.SetBool("isHoldingArrow", false);
            Animator rangedWeaponAnimator = character.characterWeaponSlotManager.rightHandSlot.currentWeaponModel.GetComponentInChildren<Animator>();

            if(rangedWeaponAnimator != null)
            {
                rangedWeaponAnimator.SetBool("isDrawn", false);
                rangedWeaponAnimator.Play("Bow_TH_Fire_01");
            }
        }

        // ���� ����� �ִϸ��̼� ���߱�
        if(character.isAiming)
        {
            character.animator.SetBool("isAiming", false);
        }
    }

    protected virtual void ProcessBuildUpDecay()
    {
        if(character.characterStatsManager.poisonBuildup > 0)
        {
            character.characterStatsManager.poisonBuildup -= 1;
        }
    }

    public virtual void AddTimedEffectParticle(GameObject effect)
    {
        GameObject effectGameObject = Instantiate(effect, buildUpTransform);
        timedEffectParticles.Add(effectGameObject);
    }

    public virtual void RemoveTimedEffectParticle(EffectParticleType effectType)
    {
        for (int i = timedEffectParticles.Count - 1; i > -1 ; i--)
        {
            if(timedEffectParticles[i].GetComponent<EffectParticle>().effectType == effectType)
            {
                Destroy(timedEffectParticles[i]);
                timedEffectParticles.RemoveAt(i);
            }
        }
    }
}
