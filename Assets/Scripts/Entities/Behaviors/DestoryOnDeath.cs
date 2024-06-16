using UnityEngine;

/// <summary>
/// 죽었을 떄 오브젝트 없애기 
/// </summary>
public class DestoryOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDeath += OnDeath;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnDeath()
    {
        _rigidbody.velocity = Vector2.zero;
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = spriteRenderer.color;
            color.a = 0.3f;
            spriteRenderer.color = color;
        }

        foreach (Behaviour behaviour in GetComponentsInChildren<Behaviour>())
        {
            behaviour.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}