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

    public override void AwardSoulsOnDeath()
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

    public override void OnAnimatorMove()
    {
        Vector3 velocity = character.animator.deltaPosition;
        character.characterController.Move(velocity);

        if(aiCharacter.isRotatingWithRootMotion)
        {
            character.transform.rotation *= character.animator.deltaRotation;

        }
    }
}
