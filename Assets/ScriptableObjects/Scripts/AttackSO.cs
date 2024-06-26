using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 공격에 대한 데이터 
/// </summary>
[CreateAssetMenu(fileName = "DefaultAttackSO", menuName = "TopDownController/Attacks/Default", order = 0)]

public class AttackSO : ScriptableObject
{
    [Header("Attack Info")]
    public float size;
    public float delay;
    public float power;
    public float speed;
    public LayerMask target;

    [Header("Knock Back Info")]
    public bool isOnKnockBack;
    public float knockbackPower;
    public float knockbackTime;

}
