using UnityEngine;
using System;

public class PlayerMoveController : MonoBehaviour
{
    private Rigidbody2D rb;

    [HideInInspector] public bool isLeftTouched = false;
    [HideInInspector] public bool isRightTouched = false;
    [HideInInspector] public float speed = 3.0f;
    [HideInInspector] public float velocity = 0f;
    private const float friction = 0.1f;
    private int reverse;

    void Start()
    {
        TryGetComponent(out rb);

        UIManager.playerGUICanvas.playerGUIMoveLeft.OnLeftTouched += SetLeftTouched;
        UIManager.playerGUICanvas.playerGUIMoveRight.OnRightTouched += SetRightTouched;
    }

    void OnDisable()
    {
        if (UIManager.playerGUICanvas != null)
        {
            UIManager.playerGUICanvas.playerGUIMoveLeft.OnLeftTouched -= SetLeftTouched;
            UIManager.playerGUICanvas.playerGUIMoveRight.OnRightTouched -= SetRightTouched;
        }
    }

    void FixedUpdate()
    {
        SetPlayerMove();
        CheckIsMoveValue();
    }



    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool isMove = false;
    private bool prevIsMove = false;

    /// <summary> 매개변수가 없을 경우 움직일 수 있는 값을 반대로 변환 </summary>
    public void SetCanMove() => canMove = !canMove;
    public void SetCanMove(bool value) => canMove = value;

    private void SetRigidbody2DVelocity() => rb.linearVelocityX = velocity * speed;     // rb에 값 적용
    private void ApplyFriction() => velocity += -(velocity * (friction * 1.5f));        // 점점 감속

    private void SetPlayerMove()
    {
        if (rb == null) return;

        if (!canMove)
        {
            ApplyFriction();
            SetRigidbody2DVelocity();
            return;
        }

        if (isLeftTouched == isRightTouched)        // 터치 입력이 없거나 둘 다 눌린 경우
        {
            if (Mathf.Abs(velocity) < 0.02f)
                velocity = 0f;
            else
                ApplyFriction();
        }
        else
        {
            reverse = isLeftTouched ? -1 : 1;       // 왼쪽은 -1, 오른쪽은 1
            velocity += (reverse - velocity) * friction;
        }

        SetRigidbody2DVelocity();
        isMove = !(!canMove || (isLeftTouched == isRightTouched));
    }

    public event Action<bool> OnIsMoveChanged;
    private void CheckIsMoveValue()
    {
        if (isMove == prevIsMove) return;
        else
        {
            prevIsMove = isMove;
            OnIsMoveChanged?.Invoke(isMove);
        }
    }



    public void SetLeftTouched(bool value)      //# PlayerGUIMoveLeft 이벤트
    {
        isLeftTouched = value;
        CheckFlip();
    }

    public void SetRightTouched(bool value)     //# PlayerGUIMoveRight 이벤트
    {
        isRightTouched = value;
        CheckFlip();
    }

    public event Action<bool> OnFlipXChanged;
    private void CheckFlip()        // 터치 입력 방향에 따라 player sprite 반전
    {
        if (!canMove) return;       // 못움직이는 경우 (터치 입력 불가한 경우) return
        if (isLeftTouched != isRightTouched)
        {
            bool flipX = isLeftTouched;
            OnFlipXChanged?.Invoke(flipX);
        }
    }
}
