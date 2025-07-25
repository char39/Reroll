using UnityEngine;

public class PlayerAniController : EntityAniController
{
    protected override void GetMoveCtrl()
    {
        moveCtrl = transform.GetComponentInParent<PlayerMoveController>();
    }
}
