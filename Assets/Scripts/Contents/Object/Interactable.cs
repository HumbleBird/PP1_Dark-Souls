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
        playermanager.inputHandler.vertical = 0;
        playermanager.inputHandler.horizontal = 0;
        //Managers.GameUI.m_InteractableAnnouncementPopupUI = null;
        Managers.GameUI.ClosePopupUI();
    }

    // ÆË¾÷ ¶Ù¿ì±â
    public virtual void CanInteractable()
    {
        if(Managers.GameUI.m_InteractableAnnouncementPopupUI == null)
        {
            Managers.Game.m_Interactable = this;
            Managers.GameUI.m_InteractableAnnouncementPopupUI = Managers.GameUI.ShowPopupUI<InteractableAnnouncementPopupUI>(false);
            Managers.GameUI.m_InteractableAnnouncementPopupUI.m_InteractionText.text = interactableText;
        }
    }
}
