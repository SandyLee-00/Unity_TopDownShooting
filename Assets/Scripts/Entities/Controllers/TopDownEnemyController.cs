using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownEnemyController : TopDownController
{
    protected GameObject ClosestTarget { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }

    protected virtual void Start()
    {
        ClosestTarget = GameManager.Instance.Player;
    }

    protected virtual void FixedUpdate()
    {
    }

    protected float DistanceToTarget()
    {
        return Vector2.Distance(transform.position, ClosestTarget.transform.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (ClosestTarget.transform.position - transform.position).normalized;
    }

}
