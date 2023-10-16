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
        PlayerManager player = Managers.Object.m_MyPlayer;
        player.playerStatsManager.AddSouls(aiCharacter.aiCharacterStatsManager.soulsAwardedOnDeath);
    }

    public void InstantiateBossParticleFX()
    {
        BossFXTransform bossFXTransform = GetComponentInChildren<BossFXTransform>();
        GameObject phaseFX = Instantiate(aiCharacter.aiCharacterBossManager.particleFX, bossFXTransform.transform);
    }

    public void PlayWeaponTrailFX()
    {
        aiCharacter.aiCharacterEffectsManager.PlayWeaponFX();
    }

    public override void OnAnimatorMove()
    {
        //if (character.isInteracting == false)
        //    return;

        Vector3 velocity = character.animator.deltaPosition;
        character.characterController.Move(velocity);

        if(aiCharacter.isRotatingWithRootMotion)
        {
            character.transform.rotation *= character.animator.deltaRotation;

        }
    }
}
