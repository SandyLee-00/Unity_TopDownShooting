using System;
using UnityEngine;


/// <summary>
/// 생성만 되었던 화살이 실제로 날라가게 하기 
/// </summary>
public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangedAttackSO attackData;
    private float currentDuration;
    private Vector2 direction;
    private bool isReady;

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    public bool fxOnDestory = true;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    public void Update()
    {
        if (!isReady) return;

        currentDuration += Time.deltaTime;
        if (currentDuration > attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }

        _rigidbody.velocity = direction * attackData.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 벽에 충돌
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * .2f;
            DestroyProjectile(destroyPosition, fxOnDestory);
        }
        // 타겟에 충돌
        else if (IsLayerMatched(attackData.target.value, collision.gameObject.layer))
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            if (healthSystem != null)
            {
                bool isAttackApplied = healthSystem.ChangeHealth(-attackData.power);

                if (isAttackApplied && attackData.isOnKnockBack)
                {
                    ApplyKnockback(collision);
                }

            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }

    private void ApplyKnockback(Collider2D collision)
    {
        TopDownMovement movement = collision.GetComponent<TopDownMovement>();
        if (movement != null)
        {
            movement.ApplyKnockback(transform, attackData.knockbackPower, attackData.knockbackTime);
        }
    }

    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }

    public void InitalizeAttack(Vector2 direction, RangedAttackSO attackData)
    {
        this.direction = direction;
        this.attackData = attackData;
        isReady = true;

        UpdateProjectileSprite();
        trailRenderer.Clear();
        currentDuration = 0;
        spriteRenderer.color = attackData.projecileColor;

        transform.right = this.direction;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // TODO : 파티클 이펙트 생성, 무기 NameTag에 맞는 fx 생성
        }

        gameObject.SetActive(false);
    }

    private void UpdateProjectileSprite()
    {
        transform.localScale = Vector3.one * attackData.size;
    }

}
