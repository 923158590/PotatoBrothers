using Game;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 购买商店物品命令
    /// </summary>
    public class ShoppingCommand : AbstractCommand
    {
        private int mIndex;

        public ShoppingCommand()
        {

        }

        public ShoppingCommand(int index)
        {
            mIndex = index;
        }

        protected override void OnExecute()
        {
            // 判断是否有足够的金币购买物品
            var coinSystem = this.GetSystem<ICoinSystem>();
            var shopSystem = this.GetSystem<IShopSystem>();
            if (coinSystem.getCoins() < shopSystem.Shop[mIndex].Coin.Value)
            {
                Debug.Log("不够钱买枪!");
                return;
            }

            ShoppingEvent e = new ShoppingEvent();
            e.index = mIndex;
            this.SendEvent(e);
        }
    }
}
