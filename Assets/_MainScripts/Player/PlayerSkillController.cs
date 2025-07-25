using UnityEngine;

public class PlayerSkillController : EntitySkillController
{
    protected override void OnEnable()
    {
        UIManager.playerGUICanvas.playerGUISkill_1.OnSkill1Touched += SkillEvent1;
        UIManager.playerGUICanvas.playerGUISkill_2.OnSkill2Touched += SkillEvent2;
        UIManager.playerGUICanvas.playerGUISkill_3.OnSkill3Touched += SkillEvent3;
    }

    protected override void OnDisable()
    {
        if (UIManager.playerGUICanvas != null)
        {
            UIManager.playerGUICanvas.playerGUISkill_1.OnSkill1Touched -= SkillEvent1;
            UIManager.playerGUICanvas.playerGUISkill_2.OnSkill2Touched -= SkillEvent2;
            UIManager.playerGUICanvas.playerGUISkill_3.OnSkill3Touched -= SkillEvent3;
        }
    }

    public override void SkillEvent1()
    {
        transform.GetComponentInChildren<PlayerAniController>().TriggerAttack1();
    }

    public override void SkillEvent2()
    {
        transform.GetComponentInChildren<PlayerAniController>().TriggerAttack2();
    }

    public override void SkillEvent3()
    {
        transform.GetComponentInChildren<PlayerAniController>().TriggerAttack3();
    }
}
