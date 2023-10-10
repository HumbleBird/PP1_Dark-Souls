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
        Debug.Log("You interactable with an Object");
    }

    public virtual void ShowInteractUI()
    {
        if(Managers.GameUI.m_InteractablePopupUI == null)
        {
            // Object
            Managers.Game.m_Interactable = this;

            // UI
            Managers.GameUI.m_InteractablePopupUI = Managers.UI.ShowPopupUI<InteractablePopupUI>();
            Managers.GameUI.m_InteractablePopupUI.m_InteractionText.text = interactableText;
        }
    }

    public virtual void CloseInteractUI()
    {
        if(Managers.GameUI.m_InteractablePopupUI != null)
        {
            Managers.Game.PlayAction(() => 
            {
                Managers.UI.ClosePopupUI(Managers.GameUI.m_InteractablePopupUI);
                Managers.GameUI.m_InteractablePopupUI = null;
                Managers.Game.m_Interactable = null;
            });
        }
    }
     
    public void OnTriggerExit(Collider other)
    {
        PlayerManager player = other.GetComponentInParent<PlayerManager>();
        if(player != null)
        {
            CloseInteractUI();
        }
    }
}
