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

    public PlayerGUIMoveLeft playerGUIMoveLeft;
    public PlayerGUIMoveRight playerGUIMoveRight;
}
