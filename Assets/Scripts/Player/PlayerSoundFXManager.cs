using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundFXManager : CharacterSoundFXManager
{
    PlayerManager player;

    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();
    }

    protected override void SetMoveStateSoundName(ref string moveStateName)
    {
        base.SetMoveStateSoundName(ref moveStateName);


        // 움직임 정도에 따라
        if (player.inputHandler.moveAmount > 0.5f)
        {
            moveStateName = "Run";
        }
        else if (player.inputHandler.moveAmount <= 0.5f)
        {
            moveStateName = "Walk";
        }
    }
}
