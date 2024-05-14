using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어가 각 액션에 대해 무엇을 실행할지 결정 
/// </summary>
public class PlayerInputController : TopDownController
{
    private Camera _camera;

    protected override void Awake() 
    {
        base.Awake();
        _camera = Camera.main;
    }

    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>().normalized;
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;

        CallLookEvent(newAim);
    }

    public void OnFire(InputValue value)
    {
        IsAttacking = value.isPressed;
    }
}
