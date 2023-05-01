using Game;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockShopItemCommand : AbstractCommand
{
    private int mIndex;

    public LockShopItemCommand()
    {

    }

    public LockShopItemCommand(int index)
    {
        mIndex = index;
    }

    protected override void OnExecute()
    {
        LockShopItemEvent e = new LockShopItemEvent();
        e.index = mIndex;
        this.SendEvent(e);
    }
}
