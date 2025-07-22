using System;

public class PlayerGUIMoveLeft : PlayerGUIMove
{
    public event Action<bool> OnLeftTouched;
    public void SetTouched(bool value) => OnLeftTouched?.Invoke(value);

    protected override void Update()
    {
        base.Update();
        SetTouched(isTouchOnUI);
    }
}
