using Game;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunCraftCommand : AbstractCommand
{
    private int mIndex;
    public GunCraftCommand(int index) { mIndex = index; }

    protected override void OnExecute()
    {
        CraftGunEvent e = new CraftGunEvent();
        e.index= mIndex;
        this.SendEvent(e);
    }
}
