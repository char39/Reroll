using UnityEngine;

public abstract class EntityStatus : MonoBehaviour
{
    [HideInInspector] public GameObject[] classObjects;
    [HideInInspector] public EntityMoveController moveController;
    [HideInInspector] public EntityAniController[] aniControllers;
    [HideInInspector] public EntitySkillController[] skillControllers;
    [HideInInspector] public int classCount;
    [HideInInspector] public int entityClass;

    protected void OnEnable()
    {
        SetClassInfo();
        Initialize();
    }

    protected void Start()
    {
        RefreshEntityClass();
    }

    protected void OnDisable()
    {
        if (moveController != null)
        {
            moveController.OnFlipXChanged -= SetSpriteFlipX;
            moveController.OnMoveStateChanged -= SetAnimatorWalkParameter;
        }
    }

    protected abstract void SetClassInfo();     // Entity의 클래스 개수, 현재 클래스 할당

    protected void Initialize()                 // object, script들 할당
    {
        TryGetComponent(out moveController);
        if (moveController != null)
        {
            moveController.OnFlipXChanged += SetSpriteFlipX;
            moveController.OnMoveStateChanged += SetAnimatorWalkParameter;
        }

        classObjects = new GameObject[classCount];
        aniControllers = new EntityAniController[classCount];
        skillControllers = new EntitySkillController[classCount];

        for (int i = 0; i < classCount; i++)
        {
            classObjects[i] = transform.GetChild(i).gameObject;
            aniControllers[i] = classObjects[i].transform.GetComponentInChildren<EntityAniController>();
            skillControllers[i] = classObjects[i].transform.GetComponentInChildren<EntitySkillController>();
        }
    }

    protected void RefreshEntityClass()       // 현재 Entity 직업의 object 및 sprite 갱신
    {
        for (int i = 0; i < classCount; i++)
        {
            classObjects[i].SetActive(entityClass == i);
        }
    }

    public void SetSpriteFlipX(bool value)
        => aniControllers[entityClass].FlipX(value);         //# 이벤트

    public void SetAnimatorWalkParameter(bool value)
        => aniControllers[entityClass].SetIsWalk(value);     //# 이벤트
}