using System;

public class PlayerGUIMoveRight : PlayerGUIMove
{
    public event Action<bool> OnRightTouched;
    public void SetTouched(bool value) => OnRightTouched?.Invoke(value);

    protected override void Update()
    {
        base.Update();
        SetTouched(isTouchOnUI);
    }
}
