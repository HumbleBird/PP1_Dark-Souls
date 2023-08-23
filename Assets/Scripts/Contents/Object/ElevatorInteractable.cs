using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorInteractable : Interactable
{
    [Header("Interactable Collider")]
    [SerializeField] Collider interactableCollider;

    [Header("Destination")]
    [SerializeField] Vector3 destinationHigh; // 가장 최고 높이의 위치
    [SerializeField] Vector3 destinationLow; // 가장 낮은 높이의 위치
    [SerializeField] bool isTravelling = false;
    [SerializeField] bool buttonIsReleased = true;

    [Header("Animator")]
    [SerializeField] Animator elevatorAnimator;
    [SerializeField] string buttonPressAnimation = "Elevator_Button_Press_01";
    [SerializeField] List<CharacterManager> charactersOnButton;
    // 만약 멀티 중이 아니라면, 리스트에는 항상 한 캐릭만 유지임. Not just the Button Presser

    protected override void Awake()
    {
        base.Awake();

        elevatorAnimator = GetComponent<Animator>();
    }

    public void ButtonTriggerEnter(CharacterManager character)
    {
        AddCharacterToListOfCharactersStandongOnButton(character);

        if (!isTravelling && buttonIsReleased)
        {
            ActivateElevator();
        }
    }

    public void ButtonTriggerExit(CharacterManager character)
    {
        StartCoroutine(ReleaseButton(character));
    }

    private void ActivateElevator()
    {
        elevatorAnimator.SetBool("isPressed", true);
        buttonIsReleased = false;
        elevatorAnimator.Play(buttonPressAnimation);

        if(transform.position == destinationHigh)
        {
            StartCoroutine(MoveElevator(destinationLow, 1));
        }
        if (transform.position == destinationLow)
        {
            StartCoroutine(MoveElevator(destinationHigh, 1));
        }
    }

    private IEnumerator MoveElevator(Vector3 finalPosition, float duration)
    {
        isTravelling = true;

        if(duration > 0)
        {
            float startTime = Time.time;
            float endTime = startTime + duration;
            yield return null;

            while (Time.time < endTime)
            {
                transform.position = Vector3.Lerp(transform.position, finalPosition, duration * Time.deltaTime);
                Vector3 movementVelocity = Vector3.Lerp(transform.position, finalPosition, duration * Time.deltaTime);
                Vector3 characterMovementVelocity = new Vector3(0, movementVelocity.y, 0);

                foreach (var character in charactersOnButton)
                {
                    character.characterController.Move(characterMovementVelocity * Time.deltaTime);
                }

                yield return null;
            }

            transform.position = finalPosition;
            isTravelling = false;
        }
    }

    private IEnumerator ReleaseButton(CharacterManager character)
    {
        while (isTravelling)
            yield return null;

        yield return new WaitForSeconds(2);

        RemoveCharacterToListOfCharactersStandongOnButton(character);

        if(charactersOnButton.Count == 0 )
        {
            elevatorAnimator.SetBool("isPressed", false);
            buttonIsReleased = true;
        }
    }

    private void AddCharacterToListOfCharactersStandongOnButton(CharacterManager character)
    {
        if (charactersOnButton.Contains(character))
            return;

        charactersOnButton.Add(character);
    }

    private void RemoveCharacterToListOfCharactersStandongOnButton(CharacterManager character)
    {
        charactersOnButton.Remove(character);

        // 리스트 중에 빈 객체 삭제
        for (int i = charactersOnButton.Count - 1; i > -1 ; i--)
        {
            if(charactersOnButton[i] == null)
            {
                charactersOnButton.RemoveAt(i);
            }
        }
    }
}
