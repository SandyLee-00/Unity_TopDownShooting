using System.Collections.Generic;
using UnityEngine;

public class PickupStatModifiers : PickupItem
{
    [SerializeField] private List<CharacterStat> statsModifier;
    protected override void OnPickedUp(GameObject receiver)
    {
        CharacterStatHandler statHandler = receiver.GetComponent<CharacterStatHandler>();
        foreach (CharacterStat stat in statsModifier)
        {
            statHandler.AddStatModifier(stat);
        }

        // HPBar Refresh용
        HealthSystem healthSystem = receiver.GetComponent<HealthSystem>();
        healthSystem.ChangeHealth(0);
    }
}
