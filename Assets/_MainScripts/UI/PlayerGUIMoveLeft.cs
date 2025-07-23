using System;

public class PlayerGUIMoveLeft : PlayerGUIMove
{
    public event Action<bool> OnLeftTouched;
    public void SetTouched(bool value) => OnLeftTouched?.Invoke(value);     //# 이벤트

    protected override void Update()
    {
        base.Update();
        SetTouched(isTouchOnUI);
    }
}
