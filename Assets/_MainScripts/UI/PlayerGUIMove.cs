using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PlayerGUIMove : MonoBehaviour
{
    protected GraphicRaycaster raycaster;
    protected EventSystem eventSystem;
    protected bool isGlobalTouch = false;   // 화면 전체 터치 유무
    public bool isTouchOnUI = false;        // 꾹 누른 상태에서 UI 진입 유무

    protected virtual void Awake()
    {
        imgTr = transform.GetChild(0).transform;
        img = imgTr.GetComponent<Image>();

        raycaster = GetComponentInParent<Canvas>().GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
    }

    protected virtual void Update()
    {
        isGlobalTouch = Input.touchCount > 0;
        isTouchOnUI = false;

        if (isGlobalTouch)
        {
            Touch touch = Input.GetTouch(0);
            PointerEventData pointerData = new(eventSystem)
            {
                position = touch.position
            };

            List<RaycastResult> results = new();
            raycaster.Raycast(pointerData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject == gameObject)
                {
                    isTouchOnUI = true;
                    break;
                }
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        RefreshTouchGUI();
    }

    protected Transform imgTr;
    protected Image img;
    protected Vector2 originSize = new(1.0f, 1.0f);
    protected Vector2 touchedSize = new(0.85f, 0.85f);
    protected Color originColor = new(0.75f, 0.75f, 0.75f, 0.75f);
    protected Color touchedColor = new(0.4f, 0.4f, 0.4f, 0.75f);

    protected virtual void RefreshTouchGUI()
    {
        if (imgTr == null || img == null) return;
        imgTr.localScale = isTouchOnUI ? touchedSize : originSize;
        img.color = isTouchOnUI ? touchedColor : originColor;
    }
}