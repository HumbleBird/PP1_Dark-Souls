using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterLocomotionManager : MonoBehaviour
{
    AICharacterManager aiCharacter;

    public CapsuleCollider characterCollider;
    public CapsuleCollider characterCollisionBlockerCollider;

    public LayerMask detectionLayer;

    // Start is called before the first frame update
    private void Awake()
    {
        aiCharacter = GetComponent<AICharacterManager>();
    }



    private void Start()
    {
        Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true);
    }

}
