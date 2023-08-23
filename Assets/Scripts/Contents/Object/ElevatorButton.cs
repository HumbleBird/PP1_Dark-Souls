using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
    [SerializeField]  ElevatorInteractable elevatorInteractable;

    private void Awake()
    {
        elevatorInteractable = GetComponentInParent<ElevatorInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CharacterManager character = other.GetComponentInParent<CharacterManager>();

        if(character != null)
        {
            elevatorInteractable.ButtonTriggerEnter(character);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterManager character = other.GetComponentInParent<CharacterManager>();

        if (character != null)
        {
            elevatorInteractable.ButtonTriggerExit(character);

        }
    }
}
