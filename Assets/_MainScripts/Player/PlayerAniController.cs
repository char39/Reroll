using UnityEngine;

public class PlayerAniController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator ani;
    private PlayerMoveController moveCtrl;

    private readonly int HashMoveSpeed = Animator.StringToHash("MoveSpeed");
    private readonly int HashAttackSpeed = Animator.StringToHash("AttackSpeed");
    private readonly int HashIdle = Animator.StringToHash("Idle");
    private readonly int HashWalk = Animator.StringToHash("Walk");
    private readonly int HashHurt = Animator.StringToHash("Hurt");
    private readonly int HashDeath = Animator.StringToHash("Death");
    private readonly int HashAttack1 = Animator.StringToHash("Attack1");
    private readonly int HashAttack2 = Animator.StringToHash("Attack2");
    private readonly int HashAttack3 = Animator.StringToHash("Attack3");

    void OnEnable()
    {
        TryGetComponent(out sr);
        TryGetComponent(out ani);
        moveCtrl = transform.GetComponentInParent<PlayerMoveController>();
    }

    public void FlipX(bool value)
    {
        if (sr == null) return;
        sr.flipX = value;
    }

    public void SetMoveSpeed(float value)       // player 이동 속도
    {
        if (ani == null) return;
        ani.SetFloat(HashMoveSpeed, value);
    }

    public void SetAttackSpeed(float value)     // player 공격 속도
    {
        if (ani == null) return;
        ani.SetFloat(HashAttackSpeed, value);
    }

    public void SetIsWalk(bool value)
    {
        if (ani == null) return;
        ani.SetBool(HashWalk, value);
    }

    public void TriggerIdle()
    {
        if (ani == null) return;
        ani.SetTrigger(HashIdle);
    }

    public void TriggerAttack1()
    {
        if (ani == null) return;
        DisableMovement();
        ani.SetTrigger(HashAttack1);
    }

    public void TriggerAttack2()
    {
        if (ani == null) return;
        DisableMovement();
        ani.SetTrigger(HashAttack2);
    }

    public void TriggerAttack3()
    {
        if (ani == null) return;
        DisableMovement();
        ani.SetTrigger(HashAttack3);
    }

    public void TriggerHurt()
    {
        if (ani == null) return;
        DisableMovement();
        ani.SetTrigger(HashHurt);
    }

    public void TriggerDeath()
    {
        if (ani == null) return;
        DisableMovement();
        ani.SetTrigger(HashDeath);
    }

    private void DisableMovement()
    {
        if (moveCtrl != null)
            moveCtrl.SetCanMove(false);
    }
}
