using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterAnimationManager : CharacterAnimatorManager
{
    AICharacterManager aiCharacter;

    protected override  void Awake()
    {
        base.Awake();
        aiCharacter = GetComponent<AICharacterManager>();
    }

    public void AwardSoulsOnDeath()
    {

        PlayerStatsManager playerstatus = FindObjectOfType<PlayerStatsManager>();
        SoulCountBar soulCountBar = FindObjectOfType<SoulCountBar>();

        if (playerstatus != null)
        {
            playerstatus.AddSouls(character.characterStatsManager.soulsAwardedOnDeath);


            if (soulCountBar != null)
            {
                soulCountBar.SetSoulCountText(playerstatus.currentSoulCount);
            }
        }

    }

    public void InstantiateBossParticleFX()
    {
        BossFXTransform bossFXTransform = GetComponentInChildren<BossFXTransform>();
        GameObject phaseFX = Instantiate(aiCharacter.aiCharacterBossManager.particleFX, bossFXTransform.transform);
    }

    public void PlayWeaponTrailFX()
    {
        aiCharacter.aiCharacterEffectsManager.PlayWeaponFX(false);
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        aiCharacter.aiCharacterRigidbody.drag = 0;
        Vector3 deltaPosition = aiCharacter.animator.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        aiCharacter.aiCharacterRigidbody.velocity = velocity;

        if(aiCharacter.isRotatingWithRootMotion)
        {
            aiCharacter.transform.rotation *= aiCharacter.animator.deltaRotation;
        }
    }
}
