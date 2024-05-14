using UnityEngine;

/// <summary>
///  Add �����ϰ�, Multiple�ϰ�, �������� Override
///  Override�� ������ -> ���ݷ��� �����ؾ��ϴ� Ư�� �����̳� �⺻ ���ݷ� ���뿡 Ȱ��
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