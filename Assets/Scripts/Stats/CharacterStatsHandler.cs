using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �⺻ ����, �߰� ���� ����ؼ� ���� ���� ��� ����
/// </summary>
public class CharacterStatHandler : MonoBehaviour
{
    [SerializeField] private CharacterStat baseStat;
    public CharacterStat CurrentStat { get; private set; }
    public List<CharacterStat> statModifiers = new List<CharacterStat>();

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        AttackSO attackSO = null;
        if (baseStat.attackSO != null)
        {
            attackSO = Instantiate(baseStat.attackSO);
        }

        CurrentStat = new CharacterStat { attackSO = attackSO };

        // TODO : �ɷ�ġ ��ȭ ���

        CurrentStat.statsChangeType = baseStat.statsChangeType;
        CurrentStat.maxHeath = baseStat.maxHeath;
        CurrentStat.speed = baseStat.speed;
    }
}