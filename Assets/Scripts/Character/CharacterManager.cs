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

    // �ִϸ��̼� �̺�Ʈ�� ���� ���� ������ 
    // backstab(�����) or riposte (�и� ������?)
    public int pendingCriticalDamage;
}
 