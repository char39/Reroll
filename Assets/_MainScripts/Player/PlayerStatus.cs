using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public enum PlayerClass { NONE = -1, SWORD = 0, ARCHER = 1, PRIEST = 2, WIZARD = 3 }
    public PlayerClass playerClass = PlayerClass.SWORD;

    public enum PlayerState { IDLE, WALK, HURT, DEATH, ATTACK1, ATTACK2, ATTACK3 }
    public PlayerState playerState = PlayerState.IDLE;

    public GameObject[] playerClassObjects;
    public PlayerAniController[] playerAniControllers;

    void OnEnable()
    {
        GameManager.player = gameObject;

        for (int i = (int)PlayerClass.SWORD; i <= (int)PlayerClass.WIZARD; i++)     // object, script들 할당
        {
            playerClassObjects[i] = transform.GetChild(i).gameObject;
            playerAniControllers[i] = playerClassObjects[i].transform.GetComponentInChildren<PlayerAniController>();
        }
    }

    void OnDisable()
    {
        if (GameManager.player == gameObject)
            GameManager.player = null;

        if (TryGetComponent<PlayerMoveController>(out var moveController))      // player sprite 반전 이벤트
            moveController.OnFlipXChanged -= SetSpriteFlipX;
    }

    void Start()
    {
        RefreshPlayerClass();

        if (TryGetComponent<PlayerMoveController>(out var moveController))      // player sprite 반전 이벤트
            moveController.OnFlipXChanged += SetSpriteFlipX;
    }

    private void RefreshPlayerClass()       // 현재 player 클래스 object 및 sprite 갱신
    {
        for (int i = (int)PlayerClass.SWORD; i <= (int)PlayerClass.WIZARD; i++)
        {
            playerClassObjects[i].SetActive((int)playerClass == i);
        }
    }

    public void SetSpriteFlipX(bool value)
    {
        playerAniControllers[(int)playerClass].FlipX(value);
    }
}
