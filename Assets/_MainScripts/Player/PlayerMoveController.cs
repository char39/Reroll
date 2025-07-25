using UnityEngine;
using System;

public class PlayerMoveController : EntityMoveController
{
    protected override void RegisterEventHandlers()
    {
        UIManager.playerGUICanvas.playerGUIMoveLeft.OnLeftTouched += SetLeftInput;
        UIManager.playerGUICanvas.playerGUIMoveRight.OnRightTouched += SetRightInput;
    }

    protected override void RemoveEventHandlers()
    {
        if (UIManager.playerGUICanvas != null)
        {
            UIManager.playerGUICanvas.playerGUIMoveLeft.OnLeftTouched -= SetLeftInput;
            UIManager.playerGUICanvas.playerGUIMoveRight.OnRightTouched -= SetRightInput;
        }
    }
}
