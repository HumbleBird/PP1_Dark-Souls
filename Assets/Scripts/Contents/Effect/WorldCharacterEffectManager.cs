using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCharacterEffectManager : MonoBehaviour
{
    public static WorldCharacterEffectManager instance;

    public PoisonBuildEffect poisonBuildUpEffect;
    public PoisonedEffect poisonedEffect;

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
