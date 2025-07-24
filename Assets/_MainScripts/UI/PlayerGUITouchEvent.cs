using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerGUITouchEvent : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    //? 이 클래스는 gui눌렀을때 자연스럽게 버튼이 눌리는 사용자 경험을 위해 만들어짐.

    // 눌렀다가 떼는경우: 일반적인 클릭
    // 눌렀다가 밖으로 나간경우: 캔슬
    // 밖에서 누른상태로 들어온경우: 동작x

    // 누른 상태: gui 눌러진 표현
    // 누른 상태에서 뗌(캔슬과 같은 동작): gui 원상복귀 or 튕기는 표현

    private bool isTouchDown = false;
    private bool isTouchExit = false;

    public void OnPointerDown(PointerEventData eventData)   // 우선순위 1
    {
        isTouchDown = true;
        isTouchExit = false;
        StartCoroutine(nameof(TriggerTouchDownEvent));
    }

    public void OnPointerExit(PointerEventData eventData)   // 우선순위 2
    {
        if (isTouchDown)
        {
            isTouchExit = true;
            StartCoroutine(nameof(TriggerTouchExitEvent));
        }
    }

    public void OnPointerUp(PointerEventData eventData)     // 우선순위 3
    {
        if (isTouchDown && !isTouchExit)
            StartCoroutine(nameof(TriggerTouchUpEvent));

        isTouchDown = false;
    }

    public void OnPointerClick(PointerEventData eventData)  // 우선순위 4
    {
        if (isTouchExit)
            return;
        
        TriggerTouchEvent();
    }

    public event Action OnTriggerTouchEvent;
    private void TriggerTouchEvent()
    {
        // 콜백 함수
        OnTriggerTouchEvent?.Invoke();
    }



    private bool isTouchDownAniPlay = false;
    private bool isTouchUpAniPlay = false;
    private readonly float touchEventAniTime = 0.075f;
    private readonly float originScale = 1.0f;
    private readonly float bounceScale = 1.1f;
    private readonly float touchedScale = 0.9f;

    private IEnumerator TriggerTouchDownEvent()
    {
        // 작아지게
        isTouchDownAniPlay = true;
        isTouchUpAniPlay = false;
        StartCoroutine(TouchEvent(touchedScale));
        yield return null;
    }

    private IEnumerator TriggerTouchExitEvent()
    {
        // 원래대로 돌아오기
        isTouchUpAniPlay = true;
        isTouchDownAniPlay = false;
        StartCoroutine(TouchEvent(originScale, false));
        yield return null;
    }

    private IEnumerator TriggerTouchUpEvent()
    {
        // 튕궜다가 돌아오기
        isTouchUpAniPlay = true;
        isTouchDownAniPlay = false;
        StartCoroutine(TouchEvent(bounceScale, false, true, false,
            () => { StartCoroutine(TouchEvent(originScale, false, false)); }
            ));
        yield return null;
    }

    private IEnumerator TouchEvent(float endScale, bool isDown = true, bool easeOut = true, bool isEndAniPlay = true, Action action = null)
    {
        float timer = 0.0f;
        float startScale = transform.localScale.x;
        float t;        // 0 ~ 1 시간 선형변환
        float easeInT;  // 점점 빠르게
        float easeOutT; // 점점 느리게

        while (timer < touchEventAniTime)
        {
            timer += Time.deltaTime;
            t = timer / touchEventAniTime;
            easeInT = Mathf.Pow(t, 2);
            easeOutT = 1 - Mathf.Pow(1 - t, 2);
            float easeInOutT = easeOut ? easeOutT : easeInT;
            float scale = Mathf.Lerp(startScale, endScale, easeInOutT);
            transform.localScale = new Vector2(scale, scale);

            if (isDown && isTouchUpAniPlay)
                yield break;
            else if (!isDown && isTouchDownAniPlay)
                yield break;

            yield return null;
        }

        transform.localScale = new Vector2(endScale, endScale);

        if (isEndAniPlay)
        {
            if (isDown)
                isTouchDownAniPlay = false;
            else
                isTouchUpAniPlay = false;
        }
        else
            action?.Invoke();
    }
}