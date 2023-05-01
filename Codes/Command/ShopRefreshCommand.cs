using QFramework;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 商店刷新命令
    /// </summary>
    public class ShopRefreshCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            // 判断是否全部商品物品都被锁定,如果4个都被锁定就不能刷新
            var shopSystem = this.GetSystem<IShopSystem>();
            int LockNums = 0;
            for (int i=0;i< shopSystem.Lock.Count;i++)
            {
                if (shopSystem.Lock[i] == true) LockNums++;
            }
            bool isAllLock = LockNums == 4 ? true : false;
            if (isAllLock)
            {
                this.GetSystem<ILogSystem>().SetLog("四个物品都被锁定，不能刷新!");
                return;
            }
            // 判断是否有足够的钱刷新
            var coinSystem = this.GetSystem<ICoinSystem>();
            if (coinSystem.getCoins() < shopSystem.RefreshCoin.Value)
            {
                UnityEngine.Debug.Log("不够金币刷新!");
                return;
            }
            // 发送刷新事件和更新视图事件
            this.SendEvent<RefreshShopEvent>();
        }
    }
}