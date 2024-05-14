using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 기본 스텟, 추가 스텟 계산해서 최종 스탯 계산 로직
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

        // TODO : 능력치 강화 기능

        CurrentStat.statsChangeType = baseStat.statsChangeType;
        CurrentStat.maxHeath = baseStat.maxHeath;
        CurrentStat.speed = baseStat.speed;
    }
}