using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public enum PlayerClass { NONE, SWORD, ARCHER, PRIEST, WIZARD }
    public PlayerClass playerClass = PlayerClass.SWORD;

    void OnEnable()
    {
        GameManager.player = gameObject;
    }

    void OnDisable()
    {
        if (GameManager.player == gameObject)
            GameManager.player = null;
    }
}
