using System;

public class PlayerGUISkill_1 : PlayerGUISkill
{
    public event Action OnSkill1Touched;

    protected override void OnClick()
    {
        base.OnClick();
        OnSkill1Touched?.Invoke();
    }
}