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
    public Transform buildUpTransform; // 이 위치에 build up particle을 소환

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
        // 해당 효과가 리스트에 있는지 체크

        StaticCharacterEffect staticEffect;

        for (int i = staticCharacterEffects.Count - 1; i > -1 ; i--)
        {
            if(staticCharacterEffects[i] != null)
            {
                if(staticCharacterEffects[i].effectID == effect.effectID)
                {
                    staticEffect = staticCharacterEffects[i];

                    // 캐릭터로부터 actual 이펙트 삭제
                    staticEffect.RemoveStaticEffect(character);

                    // 그런 다음 활성 효과 목록에서 효과를 제거합니다
                    staticCharacterEffects.Remove(staticEffect);
                }
            }
        }

        // 이펙트를 이펙트 리스트에 집어 넣기
        staticCharacterEffects.Add(effect);

        // 캐릭터에 actual effect를 추가
        effect.AddStaticEffect(character);

        // 빈 것이 있는지 체크
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
        // 파괴 가능한 effect model(drking estus, having arrow drawn ) 등
        if(instantiatedFXModel != null)
        {
            Destroy(instantiatedFXModel);
        } 
        
        // 화살을 날리고 현재 무기에 있는 loaded arrow를 삭제했다면
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

        // 현재 땡기는 애니메이션 멈추기
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
