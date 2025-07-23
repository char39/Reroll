using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerGUISkill : MonoBehaviour, IPointerClickHandler
{
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (UIManager.playerGUICanvas != null)
            UIManager.playerGUICanvas.forceRaycastTarget.SetRaycastTarget(true);    // 스킬 사용 도중 다른 스킬 사용을 못하게 하기 위함
        SetRaycastTarget(false);

        timer = coolDownTime;
        StartCoroutine(nameof(CoolDownTimer));
    }

    private Image buttonImg;
    public Image coolDownImg;   // 유니티에서 직접 할당
    public Image skillImg;      // ''

    public float coolDownTime = 2.0f;
    private float timer;

    void Awake()
    {
        TryGetComponent(out buttonImg);
    }

    public void SetRaycastTarget(bool value)
    {
        if (buttonImg == null) return;
        buttonImg.raycastTarget = value;
    }

    private IEnumerator CoolDownTimer()
    {
        float fillAmount = 1.0f;

        while (timer > 0.0f)
        {
            timer -= Time.deltaTime;
            fillAmount = timer / coolDownTime;
            RefreshCoolDownGUI(fillAmount);
            yield return null;
        }

        timer = 0.0f;
        RefreshCoolDownGUI(fillAmount);
        SetRaycastTarget(true);
    }

    private void RefreshCoolDownGUI(float value)
    {
        coolDownImg.fillAmount = value;
    }
}
