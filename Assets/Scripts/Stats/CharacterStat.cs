using UnityEngine;

/// <summary>
///  Add 먼저하고, Multiple하고, 마지막에 Override
///  Override가 마지막 -> 공격력을 고정해야하는 특정 로직이나 기본 공격력 적용에 활용
/// </summary>
public enum StatsChangeType
{
    Add = 0,
    Muliple,
    Override
}

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class CharacterStat
{
    public StatsChangeType statsChangeType;
    [Range(1, 100)] public int maxHeath;
    [Range(1f, 20f)] public int speed;
    public AttackSO attackSO;
}