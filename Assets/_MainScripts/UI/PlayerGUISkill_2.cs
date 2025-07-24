using System;

public class PlayerGUISkill_2 : PlayerGUISkill
{
    public event Action OnSkill2Touched;

    protected override void OnClick()
    {
        base.OnClick();
        OnSkill2Touched?.Invoke();
    }
}