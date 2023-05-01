using Game;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// �����̵���Ʒ����
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
            // �ж��Ƿ����㹻�Ľ�ҹ�����Ʒ
            var coinSystem = this.GetSystem<ICoinSystem>();
            var shopSystem = this.GetSystem<IShopSystem>();
            if (coinSystem.getCoins() < shopSystem.Shop[mIndex].Coin.Value)
            {
                Debug.Log("����Ǯ��ǹ!");
                return;
            }

            ShoppingEvent e = new ShoppingEvent();
            e.index = mIndex;
            this.SendEvent(e);
        }
    }
}
