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
        /// 刷新商店要花费的金币
        /// </summary>
        public BindableProperty<float> RefreshCoin { get; }
        /// <summary>
        /// 查询是否被锁住
        /// </summary>
        public List<bool> Lock { get; set; }
    }

    /// <summary>
    /// 商店系统
    /// </summary>
    public class ShopSystem : AbstractSystem, IShopSystem
    {
        // 玩家初始刷新所需要的金币
        private readonly float mInitRefreshCoin = 1;
        // 查询是否被锁住
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

        // 锁住物品
        private void OnChangeLockShopItem(LockShopItemEvent e)
        {
            mLock[e.index] = !mLock[e.index];
            if (mLock[e.index] == true)
            {
                this.GetSystem<ILogSystem>().SetLog("锁定成功!");
            }
            else
            {
                this.GetSystem<ILogSystem>().SetLog("解锁成功!");
            }
        }

        // 初始化商店
        private void OnInitShop(InitShopSuccessfulEvent e)
        {
            ClearNoLockItem();
            // 随机生成4把枪 品质随机而定
            for (int i = 0; i < 4; i++)
            {
                if (mLock[i] == true) continue;
                var Gun = RandomGun();
                Gun.Rank = RandomRank();
                if(Shop.Count != 4) Shop.Add(Gun);
                Shop[i] = Gun;
            }
            // 恢复成初始刷新所需要的金币
            RefreshCoin.Value = mInitRefreshCoin;
            // 发送初始化成功事件
            RefreshShopSuccessfulEvent refreshShopSuccessfulEvent = new RefreshShopSuccessfulEvent();
            refreshShopSuccessfulEvent.Shop = Shop;
            this.SendEvent(refreshShopSuccessfulEvent);
        }

        // 刷新商店
        private void OnRefreshShop(RefreshShopEvent e)
        {
            ClearNoLockItem();
            // 随机生成4把枪 品质随机而定
            for(int i = 0; i < 4; i++)
            {
                if (mLock[i] == true) continue;
                var Gun = RandomGun();
                Gun.Rank = RandomRank();
                Shop[i] = Gun;
            }
            // 发送金币发生变换事件
            SubCoinEvent subCoinEvent = new SubCoinEvent();
            subCoinEvent.coin = RefreshCoin.Value;
            this.SendEvent(subCoinEvent);
            // 每刷新一次就要多花1块钱
            RefreshCoin.Value++;
            // 更新日志
            this.GetSystem<ILogSystem>().SetLog("刷新成功!");
            // 发送刷新商店成功事件
            RefreshShopSuccessfulEvent refreshShopSuccessfulEvent = new RefreshShopSuccessfulEvent();
            refreshShopSuccessfulEvent.Shop = Shop;
            this.SendEvent(refreshShopSuccessfulEvent);
            this.SendEvent<UpdateShoppingViewEvent>();
        }

        // 购买商品
        private void OnShopping(ShoppingEvent e)
        {
            // 判断还能不能买枪
            var gunSystem = this.GetSystem<IGunSystem>();
            if (gunSystem.IsGunsFull())
            {
                this.GetSystem<ILogSystem>().SetLog("你持有的枪已到达上限!");
            }
            var gun = Shop[e.index];
            // 发送买枪事件
            var addGunEvent = new AddGunEvent();
            addGunEvent.gun = gun;
            this.SendEvent(addGunEvent);
            // 发送买枪要花费的金币的事件
            var subCoinEvent = new SubCoinEvent();
            subCoinEvent.coin = gun.Coin.Value;
            this.SendEvent(subCoinEvent);
            // 移除商店的枪 发送更新视图事件
            Shop[e.index] = new NullGunModel();
            mLock[e.index] = false;
            this.SendEvent<UpdateShoppingViewEvent>();
            // 更新日志
            this.GetSystem<ILogSystem>().SetLog("购买成功!");
        }
        /// <summary>
        /// 清理没有被锁住的物品
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
        /// 随机返回枪的稀有程度
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
        /// 随机返回枪的类型
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