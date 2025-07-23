using System;
using UnityEngine.EventSystems;

public class PlayerGUISkill_2 : PlayerGUISkill
{
    public event Action OnSkill2Touched;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        OnSkill2Touched?.Invoke();
    }
}