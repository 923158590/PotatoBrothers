using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public interface IShopSystem : ISystem 
    {
        public List<IGunModel> Shop { get; set; }
        /// <summary>
        /// ˢ���̵�Ҫ���ѵĽ��
        /// </summary>
        public BindableProperty<float> RefreshCoin { get; }
        /// <summary>
        /// ��ѯ�Ƿ���ס
        /// </summary>
        public List<bool> Lock { get; set; }
    }

    /// <summary>
    /// �̵�ϵͳ
    /// </summary>
    public class ShopSystem : AbstractSystem, IShopSystem
    {
        // ��ҳ�ʼˢ������Ҫ�Ľ��
        private readonly float mInitRefreshCoin = 1;
        // ��ѯ�Ƿ���ס
        private List<bool> mLock = new List<bool>();

        public List<IGunModel> Shop { get; set; } = new List<IGunModel>();
        public BindableProperty<float> RefreshCoin { get; } = new BindableProperty<float>()
        {
            Value = 1
        };
        public List<bool> Lock 
        {
            get { return mLock; }
            set { mLock = value; }
        }

        protected override void OnInit()
        {
            this.RegisterEvent<ShoppingEvent>(OnShopping);
            this.RegisterEvent<RefreshShopEvent>(OnRefreshShop);
            this.RegisterEvent<InitShopSuccessfulEvent>(OnInitShop);
            this.RegisterEvent<LockShopItemEvent>(OnChangeLockShopItem);

            bool[] trueArray = Enumerable.Repeat(false, 4).ToArray();
            mLock.AddRange(trueArray);
        }

        // ��ס��Ʒ
        private void OnChangeLockShopItem(LockShopItemEvent e)
        {
            mLock[e.index] = !mLock[e.index];
            if (mLock[e.index] == true)
            {
                this.GetSystem<ILogSystem>().SetLog("�����ɹ�!");
            }
            else
            {
                this.GetSystem<ILogSystem>().SetLog("�����ɹ�!");
            }
        }

        // ��ʼ���̵�
        private void OnInitShop(InitShopSuccessfulEvent e)
        {
            ClearNoLockItem();
            // �������4��ǹ Ʒ���������
            for (int i = 0; i < 4; i++)
            {
                if (mLock[i] == true) continue;
                var Gun = RandomGun();
                Gun.Rank = RandomRank();
                if(Shop.Count != 4) Shop.Add(Gun);
                Shop[i] = Gun;
            }
            // �ָ��ɳ�ʼˢ������Ҫ�Ľ��
            RefreshCoin.Value = mInitRefreshCoin;
            // ���ͳ�ʼ���ɹ��¼�
            RefreshShopSuccessfulEvent refreshShopSuccessfulEvent = new RefreshShopSuccessfulEvent();
            refreshShopSuccessfulEvent.Shop = Shop;
            this.SendEvent(refreshShopSuccessfulEvent);
        }

        // ˢ���̵�
        private void OnRefreshShop(RefreshShopEvent e)
        {
            ClearNoLockItem();
            // �������4��ǹ Ʒ���������
            for(int i = 0; i < 4; i++)
            {
                if (mLock[i] == true) continue;
                var Gun = RandomGun();
                Gun.Rank = RandomRank();
                Shop[i] = Gun;
            }
            // ���ͽ�ҷ����任�¼�
            SubCoinEvent subCoinEvent = new SubCoinEvent();
            subCoinEvent.coin = RefreshCoin.Value;
            this.SendEvent(subCoinEvent);
            // ÿˢ��һ�ξ�Ҫ�໨1��Ǯ
            RefreshCoin.Value++;
            // ������־
            this.GetSystem<ILogSystem>().SetLog("ˢ�³ɹ�!");
            // ����ˢ���̵�ɹ��¼�
            RefreshShopSuccessfulEvent refreshShopSuccessfulEvent = new RefreshShopSuccessfulEvent();
            refreshShopSuccessfulEvent.Shop = Shop;
            this.SendEvent(refreshShopSuccessfulEvent);
            this.SendEvent<UpdateShoppingViewEvent>();
        }

        // ������Ʒ
        private void OnShopping(ShoppingEvent e)
        {
            // �жϻ��ܲ�����ǹ
            var gunSystem = this.GetSystem<IGunSystem>();
            if (gunSystem.IsGunsFull())
            {
                this.GetSystem<ILogSystem>().SetLog("����е�ǹ�ѵ�������!");
            }
            var gun = Shop[e.index];
            // ������ǹ�¼�
            var addGunEvent = new AddGunEvent();
            addGunEvent.gun = gun;
            this.SendEvent(addGunEvent);
            // ������ǹҪ���ѵĽ�ҵ��¼�
            var subCoinEvent = new SubCoinEvent();
            subCoinEvent.coin = gun.Coin.Value;
            this.SendEvent(subCoinEvent);
            // �Ƴ��̵��ǹ ���͸�����ͼ�¼�
            Shop[e.index] = new NullGunModel();
            mLock[e.index] = false;
            this.SendEvent<UpdateShoppingViewEvent>();
            // ������־
            this.GetSystem<ILogSystem>().SetLog("����ɹ�!");
        }
        /// <summary>
        /// ����û�б���ס����Ʒ
        /// </summary>
        public void ClearNoLockItem() 
        {
            var nullGunModel = new NullGunModel();
            if (Shop.Count == 0) return;
            for(int i = Shop.Count - 1; i >= 0;i--)
            {
                if (mLock[i] != true) Shop[i] = nullGunModel;
            }
        }
        /// <summary>
        /// �������ǹ��ϡ�г̶�
        /// </summary>
        /// <returns></returns>
        public GunRank RandomRank()
        {
            int probability = UnityEngine.Random.Range(0, 100);
            if (probability <= 10) return GunRank.Legendary;
            else if (probability > 10 && probability <= 30) return GunRank.Epic;
            else if (probability > 30 && probability <= 60) return GunRank.Rare;
            else return GunRank.Normal;
        }
        /// <summary>
        /// �������ǹ������
        /// </summary>
        /// <returns></returns>
        public IGunModel RandomGun()
        {
            var Pistol = new PistolModel();
            var SubmachineGun = new SubmachineGunModel();
            var Rifle = new RifleModel();

            int probability = UnityEngine.Random.Range(0, 3);
            if (probability == 0) return Pistol;
            else if (probability == 1) return SubmachineGun;
            else return Rifle;
        }
    }

}