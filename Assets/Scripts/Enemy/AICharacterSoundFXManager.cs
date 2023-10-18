using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterSoundFXManager : CharacterSoundFXManager
{
    AICharacterManager aiCharacter;

    protected override void Awake()
    {
        base.Awake();

        aiCharacter = GetComponent<AICharacterManager>();
    }

    protected override void SetMoveStateSoundName(ref string moveStateName)
    {
        base.SetMoveStateSoundName(ref moveStateName);

        // ������ ������ ����
        if (aiCharacter.aiCharacterLocomotionManager.moveAmount > 0.5f)
        {
            moveStateName = "Run";
        }
        else if (aiCharacter.aiCharacterLocomotionManager.moveAmount <= 0.5f)
        {
            moveStateName = "Walk";
        }
    }
}
