using Game;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// ªÒ»°Ω±“√¸¡Ó
    /// </summary>
    public class AddCoinCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            AddCoinEvent e = new AddCoinEvent();
            e.coin = 1;
            this.SendEvent(e);
        }
    }
}
