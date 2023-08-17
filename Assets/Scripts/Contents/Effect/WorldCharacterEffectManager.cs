using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCharacterEffectManager : MonoBehaviour
{
    public static WorldCharacterEffectManager instance;

    [Header("POISON")]
    public PoisonBuildUpEffect poisonBuildUpEffect;
    public PoisonedEffect poisonedEffect;
    public GameObject poisonFX;
    public AudioClip poisonSFX;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
