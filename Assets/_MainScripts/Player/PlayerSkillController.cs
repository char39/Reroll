using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    void OnEnable()
    {
        UIManager.playerGUICanvas.playerGUISkill_1.OnSkill1Touched += SetSkill1Touched;
        UIManager.playerGUICanvas.playerGUISkill_2.OnSkill2Touched += SetSkill2Touched;
        UIManager.playerGUICanvas.playerGUISkill_3.OnSkill3Touched += SetSkill3Touched;
    }

    void OnDisable()
    {
        if (UIManager.playerGUICanvas != null)
        {
            UIManager.playerGUICanvas.playerGUISkill_1.OnSkill1Touched -= SetSkill1Touched;
            UIManager.playerGUICanvas.playerGUISkill_2.OnSkill2Touched -= SetSkill2Touched;
            UIManager.playerGUICanvas.playerGUISkill_3.OnSkill3Touched -= SetSkill3Touched;
        }
    }

    public void SetSkill1Touched()      //# PlayerGUISKill_1 이벤트
    {
        transform.GetComponentInChildren<PlayerAniController>().TriggerAttack1();
    }

    public void SetSkill2Touched()      //# PlayerGUISKill_2 이벤트
    {
        transform.GetComponentInChildren<PlayerAniController>().TriggerAttack2();
    }

    public void SetSkill3Touched()      //# PlayerGUISKill_3 이벤트
    {
        transform.GetComponentInChildren<PlayerAniController>().TriggerAttack3();
    }
}
