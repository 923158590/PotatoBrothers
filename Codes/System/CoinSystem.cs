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
        /// ��ȡ��ǰ��ҳ��н������
        /// </summary>
        /// <returns></returns>
        public float getCoins();
    }
    public class CoinSystem : AbstractSystem, ICoinSystem
    {
        // ���Ԥ����
        GameObject mCoinGO;
        // ��Ž�ҵĵط�
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

        // ���������,�����н�ҷ��ڽ������
        private void OnStartRound(NextRoundStartEvent e)
        {
            mCoins = new GameObject("Coins");
        }

        // �ݻٽ����,���ս��
        private void OnDestoryCoin(PlayerWinThisRoundEvent e)
        {
            GameObject.Destroy(mCoins);
        }

        // �������
        private void OnCreateCoin(EnemyDieEvent e)
        {
            var go = GameObject.Instantiate(mCoinGO);
            go.transform.localPosition = e.pos;
            go.transform.SetParent(mCoins.transform);
        }

        // ��ȡ���
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

        // ʹ�ý��
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