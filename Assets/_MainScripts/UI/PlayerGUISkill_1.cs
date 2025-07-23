using System;
using UnityEngine.EventSystems;

public class PlayerGUISkill_1 : PlayerGUISkill
{
    public event Action OnSkill1Touched;

    public override void OnPointerClick(PointerEventData eventData)
        => OnSkill1Touched?.Invoke();
}