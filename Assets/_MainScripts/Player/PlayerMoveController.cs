using UnityEngine;
using System;

public class PlayerMoveController : MonoBehaviour
{
    public event Action<bool> OnFlipXChanged;
    internal Rigidbody2D rb;

    public bool isLeftTouched = false;
    public bool isRightTouched = false;
    public float speed = 3.0f;

    internal float velocity = 0f;
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
        if (rb == null) return;

        if (isLeftTouched == isRightTouched)        // 터치 입력이 없거나 둘 다 눌린 경우
        {
            if (Mathf.Abs(velocity) < 0.02f)
                velocity = 0f;
            else
                velocity += -(velocity * (friction * 1.5f));
        }
        else
        {
            reverse = isLeftTouched ? -1 : 1;       // 왼쪽은 -1, 오른쪽은 1
            velocity += (reverse - velocity) * friction;
        }

        rb.linearVelocityX = velocity * speed;      // 값 적용
    }

    public void SetLeftTouched(bool value)
    {
        isLeftTouched = value;
        CheckFlip();
    }

    public void SetRightTouched(bool value)
    {
        isRightTouched = value;
        CheckFlip();
    }

    private void CheckFlip()        // 터치 입력 방향에 따라 player sprite 반전
    {
        if (isLeftTouched != isRightTouched)
        {
            bool flipX = isLeftTouched;
            OnFlipXChanged?.Invoke(flipX);
        }
    }
}
