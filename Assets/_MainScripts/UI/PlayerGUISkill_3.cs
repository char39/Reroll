using System;

public class PlayerGUISkill_3 : PlayerGUISkill
{
    public event Action OnSkill3Touched;

    protected override void OnClick()
    {
        base.OnClick();
        OnSkill3Touched?.Invoke();
    }
}