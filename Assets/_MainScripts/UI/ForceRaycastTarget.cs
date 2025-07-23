using UnityEngine;
using UnityEngine.UI;

public class ForceRaycastTarget : MonoBehaviour
{
    private Image img;

    void Awake()
    {
        TryGetComponent(out img);
    }

    public void SetRaycastTarget(bool value)
    {
        if (img == null) return;
        img.raycastTarget = value;
    }
}