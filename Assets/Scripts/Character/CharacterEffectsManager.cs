using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsManager : MonoBehaviour
{
    CharacterManager character;

    [Header("Static Effects")]
    [SerializeField] List<StaticCharacterEffect> staticCharacterEffects;

    [Header("Current Range FX")]
    public GameObject instantiatedFXModel;

    [Header("Damage FX")]
    public GameObject bloodSplatterFX;

    [Header("Weapon FX")]
    public WeaponFX rightWeaponFX;
    public WeaponFX leftWeaponFX;

    [Header("Poison")]
    public GameObject defaultPoisonParticleFX;
    public GameObject currentPoisonParticleFX;
    public Transform buildUpTransform; // 이 위치에 build up particle을 소환
    public bool isPoisoned;
    public float poisonBuildup = 0; // 독 수치 100에 도달한 후 플레이어를 중독시키는 시간 경과에 따른 빌드업
    public float poisonAmount = 100; // 플레이어가 무독화되기 전에 처리해야 하는 독의 양
    public float defaultPoisonAmount = 100; // 중독되기까지의 독 수치 기본 값
    public float poisonTimer = 2; // 독 피해 사이의 시간 간격
    public int poisonDamage = 1;
    float timer;

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
            if(rightWeaponFX != null)
            {
                rightWeaponFX.PlayWeaponFX();
            }
        }
        else
        {
            if (leftWeaponFX != null)
            {
                leftWeaponFX.PlayWeaponFX();
            }
        }
    }

    public virtual void PlayBloodSplatterFX(Vector3 bloodSplatterLocation)
    {
        GameObject blood = Instantiate(bloodSplatterFX, bloodSplatterLocation, Quaternion.identity);
    }

    public virtual void HandleAllBuildUpEffects()
    {
        if (character.isDead)
            return;

        HandlePoisonBuildUp();
        HandleIsPoisonedEffect();
    }

    protected virtual void HandlePoisonBuildUp()
    {
        if (isPoisoned)
            return;

        if(poisonBuildup > 0 && poisonBuildup < 100)
        {
            poisonBuildup = poisonBuildup - 1 * Time.deltaTime;
        }
        else if (poisonBuildup >= 100)
        {
            isPoisoned = true;
            poisonBuildup = 0;

            if(buildUpTransform != null)
            {
                currentPoisonParticleFX = Instantiate(defaultPoisonParticleFX, buildUpTransform.transform);
            }
            else
            {
                currentPoisonParticleFX = Instantiate(defaultPoisonParticleFX, character.transform);
            }
        }
    }

    protected virtual void HandleIsPoisonedEffect()
    {
        if(isPoisoned)
        {
            if(poisonAmount > 0 )
            {
                timer += Time.deltaTime;

                if(timer >= poisonTimer)
                {
                    character.characterStatsManager.TakePoisonDamage(poisonDamage);
                    timer = 0;
                }

                poisonAmount = poisonAmount - 1 * Time.deltaTime;
            }
            else
            {
                isPoisoned = false;
                poisonAmount = defaultPoisonAmount;
                Destroy(currentPoisonParticleFX);
            }
        }
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
}
