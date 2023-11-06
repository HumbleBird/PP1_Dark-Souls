using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : CharacterAnimatorManager
{
    PlayerManager player;

    int vertical;
    int horizontal;


    protected override void Awake()
    {
        base.Awake();

        player = GetComponent<PlayerManager>();

        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimatorValues(float verticalAmount, float horizontalAmount, bool isSprinting)
    {
        #region Vertical

        float v = 0;
        if (verticalAmount > 0 && verticalAmount < 0.55f)
        {
            v = 0.5f;
        }
        else if (verticalAmount > 0.55f)
        {
            v = 1;
        }
        else if (verticalAmount < 0 && verticalAmount > -0.55f)
        {
            v = -0.5f;
        }
        else if (verticalAmount < -0.55f)
        {
            v = -1;
        }
        else
        {
            v = 0;
        }

        #endregion
        #region Horizontal

        float h = 0;
        if (horizontalAmount > 0 && horizontalAmount < 0.55f)
        {
            h = 0.5f;
        }
        else if (horizontalAmount > 0.55f)
        {
            h = 1;
        }
        else if (horizontalAmount < 0 && horizontalAmount > -0.55f)
        {
            h = -0.5f;
        }
        else if (horizontalAmount < -0.55f)
        {
            h = -1;
        }
        else
        {
            h = 0;
        }

        #endregion

        if (isSprinting)
        {
            v = 2;
            h = horizontalAmount;
        }

        player.animator.SetFloat("Vertical", v, 0.1f, Time.deltaTime);
        player.animator.SetFloat("Horizontal", h, 0.1f, Time.deltaTime);
    }

    public void SucessFullyUseCurrentConsumable()
    {
        if (character.characterEquipmentManager.m_CurrentHandConsumable != null)
        {
            character.characterEquipmentManager.m_CurrentHandConsumable.SucessToConsumeItem(player);
        }
    }

    public override void RollCameraShake()
    {
        Managers.Camera.CameraShake(1);
    }

    public override void OnAnimatorMove()
    {
        if (character.isInteracting == false)
            return;

        Vector3 velocity = character.animator.deltaPosition;

        if (character.characterController.enabled == true)
        {
            character.characterController.Move(velocity);
            character.transform.rotation *= character.animator.deltaRotation;
        }
        else
        {
            transform.position += velocity;
        }
    }
}
