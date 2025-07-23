using UnityEngine;

public class PlayerGUICanvas : MonoBehaviour
{
    void OnEnable()
    {
        UIManager.playerGUICanvas = this;
    }

    void OnDisable()
    {
        if (UIManager.playerGUICanvas == this)
            UIManager.playerGUICanvas = null;
    }

    // 아래 필드 할당은 유니티에서
    public ForceRaycastTarget forceRaycastTarget;
    public PlayerGUIMoveLeft playerGUIMoveLeft;
    public PlayerGUIMoveRight playerGUIMoveRight;
    public PlayerGUISkill_1 playerGUISkill_1;
    public PlayerGUISkill_2 playerGUISkill_2;
    public PlayerGUISkill_3 playerGUISkill_3;
    
}
