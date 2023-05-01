using System;
using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace Game
{
    public interface IGunSystem : ISystem
    {
        /// <summary>
        /// 存放枪的背包
        /// </summary>
        public List<GunSlotModel> GunBag { get; }
        /// <summary>
        /// 现有枪的数量
        /// </summary>
        public BindableProperty<int> GunNums { get; }
        /// <summary>
        /// 最大持有枪的数量
        /// </summary>
        public BindableProperty<int> MaxGunNums { get; }
        /// <summary>
        /// 判断枪背包有没有满
        /// </summary>
        /// <returns></returns>
        public bool IsGunsFull();
        /// <summary>
        /// 返回背包中枪的数量
        /// </summary>
        /// <returns></returns>
        public int getGunNumsInBag();
    }

    /// <summary>
    /// 枪支系统
    /// </summary>
    public class GunSystem : AbstractSystem, IGunSystem
    {
        /// <summary>
        /// 存放创建枪在游戏中的位置
        /// </summary>
        public List<Vector2> gunCreatePos = new List<Vector2>();
        public List<GunSlotModel> GunBag { get; } = new List<GunSlotModel>();
        public BindableProperty<int> GunNums { get; } = new BindableProperty<int>()
        {
            Value = 0
        };
        public BindableProperty<int> MaxGunNums { get; } = new BindableProperty<int>()
        {
            Value = 6
        };

        // 手枪预制体 冲锋枪预制体 步枪预制体 
        private GameObject mPistolGo;
        private GameObject mSubmachineGunGo;
        private GameObject mRifleGo;

        protected override void OnInit()
        {
            this.RegisterEvent<CraftGunEvent>(OnCraftGun);
            this.RegisterEvent<AddGunEvent>(OnAddGun);
            this.RegisterEvent<RemoveGunEvent>(OnRemoveGun);
            this.RegisterEvent<CreateGunEvent>(OnCreateGun);

            gunCreatePos.Add(new Vector2(-0.2f, 0.1f));
            gunCreatePos.Add(new Vector2(0.2f, 0.1f));
            gunCreatePos.Add(new Vector2(0.2f, -0.1f));
            gunCreatePos.Add(new Vector2(-0.2f, -0.1f));
            gunCreatePos.Add(new Vector2(0, -0.2f));
            gunCreatePos.Add(new Vector2(0, 0.2f));

            GunBag.Add(new GunSlotModel());
            GunBag.Add(new GunSlotModel());
            GunBag.Add(new GunSlotModel());
            GunBag.Add(new GunSlotModel());
            GunBag.Add(new GunSlotModel());
            GunBag.Add(new GunSlotModel());
            var Pistol = new PistolModel();
            GunBag[0].Gun = Pistol;


            mPistolGo = Resources.Load<GameObject>("Prefab/Pistol");
            mSubmachineGunGo = Resources.Load<GameObject>("Prefab/SubmachineGun");
            mRifleGo = Resources.Load<GameObject>("Prefab/Rifle");
        }

        // 合成武器
        private void OnCraftGun(CraftGunEvent e)
        {
            for(int i=0;i< GunBag.Count;i++)
            {
                // 如果是原来的武器就跳过
                if (i == e.index || GunBag[i].Gun == null) continue;
                // 如果两个武器的品质相同并且不是最高等级，两把武器变为一把武器，并且升级武器
                bool isRankEqual = GunBag[i].Gun.Rank == GunBag[e.index].Gun.Rank;
                bool noLegendary = GunBag[i].Gun.Rank != GunRank.Legendary;
                bool isTypeEqual = GunBag[i].Gun.GetType() == GunBag[e.index].Gun.GetType();
                if (isRankEqual && noLegendary && isTypeEqual)
                {
                    this.GetSystem<ILogSystem>().SetLog("合成武器成功!");
                    GunBag[e.index].Gun.Rank++;
                    //Guns[i] = new NullGunModel();
                    GunBag[i].Gun = null;
                    break;
                }
                else
                {
                    this.GetSystem<ILogSystem>().SetLog("不满足合成武器的条件!");
                }
            }
            this.SendEvent<UpdateShoppingViewEvent>();
        }

        // 添加武器
        private void OnAddGun(AddGunEvent e)
        {
            for(int i=0;i<6;i++)
            {
                if(GunBag[i].Gun == null)
                {
                    GunBag[i].Gun = e.gun;
                    break;
                }
            }
            this.SendEvent<UpdateShoppingViewEvent>();
        }

        // 删除武器
        private void OnRemoveGun(RemoveGunEvent e)
        {
            for (int i = 0; i < 6; i++)
            {
                if (GunBag[i].Gun == e.gun)
                {
                    GunBag[i].Gun = null;
                }
            }
            AddCoinEvent addCoinEvent = new AddCoinEvent();
            // 在原价Coin的基础上 -0.5
            addCoinEvent.coin = e.gun.Coin - 0.5f;
            this.SendEvent(addCoinEvent);
            // 发送更新商店视图事件
            this.SendEvent<UpdateShoppingViewEvent>();
            // 更新日志
            this.GetSystem<ILogSystem>().SetLog("出售成功!");
        }

        // 游戏开始时创建武器
        private void OnCreateGun(CreateGunEvent e)
        {
            Transform player = Player.Instance.transform;
            Transform parent = player.Find("Weapon");

            for (int i=0;i<GunBag.Count;i++)
            {
                if (GunBag[i].Gun == null) continue;
                if (GunBag[i].Gun.Name == "Pistol")
                {
                    var go = GameObject.Instantiate(mPistolGo);
                    go.transform.SetParent(parent);
                    go.transform.localPosition = gunCreatePos[i];
                }
                else if (GunBag[i].Gun.Name == "SubmachineGun")
                {
                    var go = GameObject.Instantiate(mSubmachineGunGo);
                    go.transform.SetParent(parent);
                    go.transform.localPosition = gunCreatePos[i];
                }
                else if (GunBag[i].Gun.Name == "Rifle")
                {
                    var go = GameObject.Instantiate(mRifleGo);
                    go.transform.SetParent(parent);
                    go.transform.localPosition = gunCreatePos[i];
                }
            }
        }

        /// <summary>
        /// 添加武器
        /// </summary>
        public void AddGun(IGunModel gun)
        {
            for (int i = 0; i < 6; i++)
            {
                if (GunBag[i].Gun == null)
                {
                    GunBag[i].Gun = gun;
                    break;
                }
            }
        }

        /// <summary>
        /// 判断枪背包的容量有没有满
        /// </summary>
        /// <returns></returns>
        bool IGunSystem.IsGunsFull()
        {
            for (int i = 0; i < 6; i++)
            {
                if (GunBag[i].Gun == null)
                {
                    return false;
                }
            }
            return true;
        }

        int IGunSystem.getGunNumsInBag()
        {
            int nums = 0;
            for (int i = 0; i < 6; i++)
            {
                if (GunBag[i].Gun != null)
                {
                    nums++;
                }
            }
            return nums;
        }
    }
}