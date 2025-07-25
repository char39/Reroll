using UnityEngine;

public class PlayerStatus : EntityStatus
{
    public enum PlayerClass { SWORD = 0, ARCHER = 1, PRIEST = 2, WIZARD = 3 }
    public PlayerClass playerClass = PlayerClass.SWORD;

    protected override void SetClassInfo()
    {
        classCount = 4;
        entityClass = (int)playerClass;
    }
}
