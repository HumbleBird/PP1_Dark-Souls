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

    // �ִϸ��̼� �̺�Ʈ�� ���� ���� ������ 
    // backstab(�����) or riposte (�и� ������?)
    public int pendingCriticalDamage;
}
 