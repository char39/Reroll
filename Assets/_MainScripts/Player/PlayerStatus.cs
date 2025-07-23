using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public enum PlayerClass { NONE = -1, SWORD = 0, ARCHER = 1, PRIEST = 2, WIZARD = 3 }
    public PlayerClass playerClass = PlayerClass.SWORD;

    [HideInInspector] public PlayerMoveController playerMoveController;
    [HideInInspector] public GameObject[] playerClassObjects;
    [HideInInspector] public PlayerAniController[] playerAniControllers;
    [HideInInspector] public PlayerSkillController[] playerSkillControllers;

    void OnEnable()
    {
        Initialize();
    }

    void OnDisable()
    {
        if (playerMoveController != null)
        {
            playerMoveController.OnFlipXChanged -= SetSpriteFlipX;
            playerMoveController.OnIsMoveChanged -= SetAnimatorWalkParameter;
        }
    }

    void Start()
    {
        RefreshPlayerClass();
    }

    private void Initialize()               // object, script들 할당
    {
        TryGetComponent(out playerMoveController);
        if (playerMoveController != null)
        {
            playerMoveController.OnFlipXChanged += SetSpriteFlipX;
            playerMoveController.OnIsMoveChanged += SetAnimatorWalkParameter;
        }

        playerClassObjects = new GameObject[4];
        playerAniControllers = new PlayerAniController[4];
        playerSkillControllers = new PlayerSkillController[4];

        for (int i = (int)PlayerClass.SWORD; i <= (int)PlayerClass.WIZARD; i++)
        {
            playerClassObjects[i] = transform.GetChild(i).gameObject;
            playerAniControllers[i] = playerClassObjects[i].transform.GetComponentInChildren<PlayerAniController>();
            playerSkillControllers[i] = playerClassObjects[i].transform.GetComponentInChildren<PlayerSkillController>();
        }
    }

    private void RefreshPlayerClass()       // 현재 player 클래스 object 및 sprite 갱신
    {
        for (int i = (int)PlayerClass.SWORD; i <= (int)PlayerClass.WIZARD; i++)
        {
            playerClassObjects[i].SetActive((int)playerClass == i);
        }
    }

    public void SetSpriteFlipX(bool value)
        => playerAniControllers[(int)playerClass].FlipX(value);         //# 이벤트
    public void SetAnimatorWalkParameter(bool value)
        => playerAniControllers[(int)playerClass].SetIsWalk(value);     //# 이벤트
}
