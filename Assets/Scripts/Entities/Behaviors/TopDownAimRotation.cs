using UnityEngine;

/// <summary>
/// 플레이어가 마우스에 따라 조준하거나 방향 맞춰 바라보기 
/// </summary>
public class TopDownAimRotation : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer armRenderer;
    [SerializeField] 
    private Transform armPivot;
    [SerializeField]
    private SpriteRenderer characterRenderer;

    private TopDownController _controller;

    private void Awake()
    {
        _controller = GetComponent<TopDownController>();
    }

    private void Start()
    {
        _controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 direction)
    {
        RotateArm(direction);
    }

    private void RotateArm(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        characterRenderer.flipX = Mathf.Abs(rotZ) > 90f;
        armRenderer.flipY = characterRenderer.flipX;

        armPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

}