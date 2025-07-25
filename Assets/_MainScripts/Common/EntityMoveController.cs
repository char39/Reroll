using UnityEngine;
using System;

public class EntityMoveController : MonoBehaviour
{
    protected Rigidbody2D rb;

    protected virtual void OnEnable()
    {
        TryGetComponent(out rb);
        RegisterEventHandlers();
    }

    protected virtual void OnDisable()
    {
        RemoveEventHandlers();
    }

    protected virtual void FixedUpdate()
    {
        SetMove();
        SetMoveState();
    }

    protected virtual void RegisterEventHandlers()
    {
        // ex) Player GUI 터치 입력
        //? UIManager.playerGUICanvas.playerGUIMoveLeft.OnLeftTouched += SetLeftInput;
        //? UIManager.playerGUICanvas.playerGUIMoveRight.OnRightTouched += SetRightInput;
    }

    protected virtual void RemoveEventHandlers()
    {
        // ex) Player GUI 터치 입력
        //? UIManager.playerGUICanvas.playerGUIMoveLeft.OnLeftTouched -= SetLeftInput;
        //? UIManager.playerGUICanvas.playerGUIMoveRight.OnRightTouched -= SetRightInput;
    }

    protected bool leftInput = false;
    protected bool rightInput = false;
    protected float speed = 3.0f;
    protected float velocity = 0f;
    protected readonly float friction = 0.1f;
    protected int reverse;

    protected bool canMove = true;
    protected bool isMove = false;
    protected bool prevIsMove = false;

    public void SetCanMove(bool value) => canMove = value;  // 외부 호출용

    protected void SetRigidbody2DVelocity() => rb.linearVelocityX = velocity * speed;     // rb에 값 적용
    protected void ApplyFriction() => velocity += -(velocity * (friction * 1.5f));        // 점점 감속

    public void SetLeftInput(bool value)        //# 이벤트
    {
        leftInput = value;
        SetFlipDirection();
    }

    public void SetRightInput(bool value)       //# 이벤트
    {
        rightInput = value;
        SetFlipDirection();
    }

    protected void SetMove()
    {
        if (rb == null) return;

        if (!canMove)
        {
            ApplyFriction();
            SetRigidbody2DVelocity();
            return;
        }

        if (leftInput == rightInput)            // 입력이 없거나 둘 다 눌린 경우
        {
            if (Mathf.Abs(velocity) < 0.02f)
                velocity = 0f;
            else
                ApplyFriction();
        }
        else
        {
            reverse = leftInput ? -1 : 1;       // 왼쪽은 -1, 오른쪽은 1
            velocity += (reverse - velocity) * friction;
        }

        SetRigidbody2DVelocity();
        isMove = !(!canMove || (leftInput == rightInput));
    }

    public event Action<bool> OnFlipXChanged;           // 방향에 따라 sprite 반전
    protected void SetFlipDirection()                   //? 이벤트에서 호출됨
    {
        if (!canMove) return;                           // 못움직이는 경우 (터치 입력 불가한 경우) return
        if (leftInput != rightInput)
        {
            bool flipX = leftInput;
            OnFlipXChanged?.Invoke(flipX);
        }
    }

    public event Action<bool> OnMoveStateChanged;       // 움직이는지 멈춘 상태인지 확인
    protected void SetMoveState()                       //? FixedUpdate에서 갱신
    {
        if (isMove == prevIsMove) return;
        else
        {
            prevIsMove = isMove;
            OnMoveStateChanged?.Invoke(isMove);
        }
    }
}