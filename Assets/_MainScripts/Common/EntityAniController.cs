using UnityEngine;

public abstract class EntityAniController : MonoBehaviour
{
    protected readonly int HashMoveSpeed = Animator.StringToHash("MoveSpeed");
    protected readonly int HashAttackSpeed = Animator.StringToHash("AttackSpeed");
    protected readonly int HashIdle = Animator.StringToHash("Idle");
    protected readonly int HashWalk = Animator.StringToHash("Walk");
    protected readonly int HashHurt = Animator.StringToHash("Hurt");
    protected readonly int HashDeath = Animator.StringToHash("Death");
    protected readonly int HashAttack1 = Animator.StringToHash("Attack1");
    protected readonly int HashAttack2 = Animator.StringToHash("Attack2");
    protected readonly int HashAttack3 = Animator.StringToHash("Attack3");

    protected SpriteRenderer sr;
    protected Animator ani;
    protected EntityMoveController moveCtrl;

    protected virtual void OnEnable()
    {
        TryGetComponent(out sr);
        TryGetComponent(out ani);
        GetMoveCtrl();
    }

    protected abstract void GetMoveCtrl();              // 각 엔티티당 moveCtrl 할당

    public virtual void FlipX(bool value)
    {
        if (sr == null) return;
        sr.flipX = value;
    }

    public virtual void SetMoveSpeed(float value)       // 이동 속도
    {
        if (ani == null) return;
        ani.SetFloat(HashMoveSpeed, value);
    }

    public virtual void SetAttackSpeed(float value)     // 공격 속도
    {
        if (ani == null) return;
        ani.SetFloat(HashAttackSpeed, value);
    }

    public virtual void SetIsWalk(bool value)           // 움직임 유무
    {
        if (ani == null) return;
        ani.SetBool(HashWalk, value);
    }

    public virtual void TriggerIdle()
    {
        if (ani == null) return;
        ani.SetTrigger(HashIdle);
    }

    public virtual void TriggerAttack1()
    {
        if (ani == null) return;
        DisableMovement();
        ani.SetTrigger(HashAttack1);
    }

    public virtual void TriggerAttack2()
    {
        if (ani == null) return;
        DisableMovement();
        ani.SetTrigger(HashAttack2);
    }

    public virtual void TriggerAttack3()
    {
        if (ani == null) return;
        DisableMovement();
        ani.SetTrigger(HashAttack3);
    }

    public virtual void TriggerHurt()
    {
        if (ani == null) return;
        DisableMovement();
        ani.SetTrigger(HashHurt);
    }

    public virtual void TriggerDeath()
    {
        if (ani == null) return;
        DisableMovement();
        ani.SetTrigger(HashDeath);
    }

    protected virtual void DisableMovement()
    {
        if (moveCtrl != null)
            moveCtrl.SetCanMove(false);
    }
}