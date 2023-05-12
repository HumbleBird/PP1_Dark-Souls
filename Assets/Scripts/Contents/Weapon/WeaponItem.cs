using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("Idle Animations")]
    public string right_hand_idle;
    public string left_hand_idle;
    public string th_idle;

    [Header("One Handed Attack Animatons")]
    public string oh_light_attack_01;
    public string oh_light_attack_02;
    public string oh_heavy_attack_01;
    public string oh_heavy_attack_02;

    [Header("Stamina Costs")]
    public int baseStamina;
    public float lightAttackMultiplier;
    public float heavyAttackMultiplier;
}
