using System;

public class PlayerGUIMoveRight : PlayerGUIMove
{
    public event Action<bool> OnRightTouched;
    public void SetTouched(bool value) => OnRightTouched?.Invoke(value);    //# 이벤트

    protected override void Update()
    {
        base.Update();
        SetTouched(isTouchOnUI);
    }
}
