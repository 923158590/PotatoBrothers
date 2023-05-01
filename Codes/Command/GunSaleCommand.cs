using Game;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// ≥ˆ €«π÷ß√¸¡Ó
    /// </summary>
    public class GunSaleCommand : AbstractCommand
    {
        private IGunModel mGun;

        public GunSaleCommand(IGunModel mGun)
        {
            this.mGun = mGun;
        }

        protected override void OnExecute()
        {
            RemoveGunEvent e = new RemoveGunEvent();
            e.gun = mGun;
            this.SendEvent(e);
        }
    }
}
