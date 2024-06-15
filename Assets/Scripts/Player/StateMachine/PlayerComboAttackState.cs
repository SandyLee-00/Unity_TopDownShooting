
public class PlayerComboAttackState : PlayerAttackState
{
    private bool alreadyAppliedForce;
    private bool alreadyApplyCombo;

    AttackInfoData attackInfoData;

    public PlayerComboAttackState(PlayerStateMachine playerStateMachine) : base(playerStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StartAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        alreadyApplyCombo = false;
        alreadyAppliedForce = false;

        int comboIndex = stateMachine.ComboIndex;
        attackInfoData = stateMachine.Player.Data.AttackData.GetAttackInfo(comboIndex);
        stateMachine.Player.Animator.SetInteger("Combo", comboIndex);
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.AnimationData.ComboAttackParameterHash);

        if (!alreadyApplyCombo)
        {
            stateMachine.ComboIndex = 0;
        }
    }

    private void TryComboAttack()
    {
        if (alreadyApplyCombo) return;

        if (attackInfoData.ComboStateIndex == -1) return;

        if (!stateMachine.IsAttacking) return;

        alreadyApplyCombo = true;
    }

    /// <summary>
    /// 공격할 떄 플레이어 앞으로 가기
    /// </summary>
    private void TryApplyForce()
    {
        if (alreadyAppliedForce) return;
        alreadyAppliedForce = true;

        stateMachine.Player.ForceReceiver.Reset();

        stateMachine.Player.ForceReceiver.AddForce(stateMachine.Player.transform.forward * attackInfoData.Force);
    }

    public override void Update()
    {
        base.Update();

        ForceMove();

        // 현재 진행중인 애니메이션 상태를 가져온다.
        float normalizedTime = GetNormalizedTime(stateMachine.Player.Animator, "Attack");
        if (normalizedTime < 1f)
        {
            // 댐핑 시도
            if (normalizedTime >= attackInfoData.ForceTransitionTime)
            {
                TryApplyForce();
            }

            // 콤보 시도
            if (normalizedTime >= attackInfoData.ComboTransitionTime)
            {
                TryComboAttack();
            }
        }
        else
        {
            // 콤보 이미 진행 중 -> 다음 콤보 상태로 전환
            if (alreadyApplyCombo)
            {
                stateMachine.ComboIndex = attackInfoData.ComboStateIndex;
                stateMachine.ChangeState(stateMachine.ComboAttackState);
            }
            else
            {
                stateMachine.ChangeState(stateMachine.IdleState);
            }
        }
    }
}