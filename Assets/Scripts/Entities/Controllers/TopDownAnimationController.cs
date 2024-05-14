using UnityEngine;

/// <summary>
/// 애니메이션
/// </summary>
public class TopDownAnimationController : AnimationController
{
    private static readonly int isWalking = Animator.StringToHash("isWalking");
    private static readonly int isHit = Animator.StringToHash("isHit");
    private static readonly int attack = Animator.StringToHash("attack");

    private static readonly float magnitudeThreshold = 0.5f;

    private HealthSystem healthSystem;

    protected override void Awake()
    {
        base.Awake();
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Walking;

        if (healthSystem != null)
        {
            healthSystem.OnDamage += Hit;
            healthSystem.OnInvincibilityEnd += InvinciblilityEnd;
        }
    }

    private void Walking(Vector2 direction)
    {
        if (direction.magnitude > magnitudeThreshold)
        {
            animator.SetBool(isWalking, true);
        }
        else
        {
            animator.SetBool(isWalking, false);
        }
    }

    private void Attacking(AttackSO attackSO)
    {
        animator.SetTrigger(attack);
    }

    private void Hit()
    {
        animator.SetBool(isHit, true);
    }

    private void InvinciblilityEnd()
    {
        animator.SetBool(isHit, false);
    }
}
