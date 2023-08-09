using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCharacterEffect : ScriptableObject
{
    public int effectID;

    // Static Effect 는 일반적으로 아이템을 장착할 때 추가, 제거는 해당 아이템을 장착 해제시 됨

    public virtual void AddStaticEffect(CharacterManager character)
    {

    }
    public virtual void RemoveStaticEffect(CharacterManager character)
    {

    }
}
