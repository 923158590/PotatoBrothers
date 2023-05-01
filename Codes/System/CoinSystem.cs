using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface ICoinSystem : ISystem 
    {
        /// <summary>
        /// 获取当前玩家持有金币数量
        /// </summary>
        /// <returns></returns>
        public float getCoins();
    }
    public class CoinSystem : AbstractSystem, ICoinSystem
    {
        // 金币预制体
        GameObject mCoinGO;
        // 存放金币的地方
        GameObject mCoins;
        protected override void OnInit()
        {
            mCoinGO = Resources.Load<GameObject>("Prefab/Coin");
            mCoins = new GameObject("Coins");

            this.RegisterEvent<EnemyDieEvent>(OnCreateCoin);
            this.RegisterEvent<AddCoinEvent>(OnAddCoin);
            this.RegisterEvent<SubCoinEvent>(OnSubCoin);
            this.RegisterEvent<PlayerWinThisRoundEvent>(OnDestoryCoin);
            this.RegisterEvent<NextRoundStartEvent>(OnStartRound);
        }

        // 创建金币组,把所有金币放在金币组里
        private void OnStartRound(NextRoundStartEvent e)
        {
            mCoins = new GameObject("Coins");
        }

        // 摧毁金币组,回收金币
        private void OnDestoryCoin(PlayerWinThisRoundEvent e)
        {
            GameObject.Destroy(mCoins);
        }

        // 创建金币
        private void OnCreateCoin(EnemyDieEvent e)
        {
            var go = GameObject.Instantiate(mCoinGO);
            go.transform.localPosition = e.pos;
            go.transform.SetParent(mCoins.transform);
        }

        // 获取金币
        private void OnAddCoin(AddCoinEvent e)
        {

            var playerModel = this.GetModel<IPlayerModel>();
            int value = UnityEngine.Random.Range(0, 100);
            float lucky = playerModel.lucky;
            if(value < lucky)
            {
                playerModel.coin.Value += e.coin * lucky;
            }
            else
            {
                playerModel.coin.Value += e.coin;
            }
            this.SendEvent<CoinChangeEvent>();
        }

        // 使用金币
        private void OnSubCoin(SubCoinEvent e)
        {
            this.GetModel<IPlayerModel>().coin.Value -= e.coin;
            this.SendEvent<CoinChangeEvent>();
        }

        float ICoinSystem.getCoins()
        {
            return this.GetModel<IPlayerModel>().coin.Value;
        }
    }

}