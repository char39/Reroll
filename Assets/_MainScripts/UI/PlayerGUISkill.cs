using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PlayerGUISkill : MonoBehaviour, IPointerClickHandler
{
    public abstract void OnPointerClick(PointerEventData eventData);
}
