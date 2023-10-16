using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 0.6f;
    public string interactableText;

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public virtual void Interact(PlayerManager playermanager)
    {
        Managers.GameUI.m_GameSceneUI.m_InteractablePopupUI.ClosePopup();
    }

    // ÆË¾÷ ¶Ù¿ì±â
    public virtual void CanInteractable()
    {
        if(Managers.GameUI.m_isShowingInteratablePopup == false)
        {
            Managers.Game.m_Interactable = this;
            Managers.GameUI.m_GameSceneUI.m_InteractablePopupUI.ShowPopup();

        }
    }
}
