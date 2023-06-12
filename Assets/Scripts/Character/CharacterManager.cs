using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Header("Lock On Tranform")]
    public Transform lockOnTransform;

    [Header("Combat Colliders")]
    public CriticalDamageCollider backStabCollider;
    public CriticalDamageCollider riposteCollider;

    [Header("Combat Flaged")]
    public bool canBeRiposted;
    public bool canBeParryied;
    public bool isParrying;
    public bool isBlocking;
    
    [Header("Spells")]
    public bool isFiringSpell;

    // 애니메이션 이벤트에 맞춰 들어가는 데미지 
    // backstab(뒤잡기) or riposte (패링 데미지?)
    public int pendingCriticalDamage;
}
 