using System;
using UnityEngine.EventSystems;

public class PlayerGUISkill_3 : PlayerGUISkill
{
    public event Action OnSkill3Touched;

    public override void OnPointerClick(PointerEventData eventData)
        => OnSkill3Touched?.Invoke();
}