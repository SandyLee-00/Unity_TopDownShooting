using UnityEngine;

/// <summary>
///  Add �����ϰ�, Multiple�ϰ�, �������� Override
///  Override�� ������ -> ���ݷ��� �����ؾ��ϴ� Ư�� �����̳� �⺻ ���ݷ� ���뿡 Ȱ��
/// </summary>
public enum StatsChangeType
{
    Add = 0,
    Multiple,
    Override
}

/// <summary>
/// 
/// </summary>
[System.Serializable]
public class CharacterStat
{
    public StatsChangeType statsChangeType;
    [Range(0, 100)] public int maxHealth;
    [Range(0f, 20f)] public float speed;
    public AttackSO attackSO;
}