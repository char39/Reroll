using UnityEngine;

public class PlayerAnimationSingleLoopEndBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var moveController = animator.GetComponentInParent<PlayerMoveController>();
        if (moveController != null)
        {
            moveController.SetCanMove(true);
        }

        if (UIManager.playerGUICanvas != null)
        {
            UIManager.playerGUICanvas.forceRaycastTarget.SetRaycastTarget(false);
        }
    }
}
