using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Game
{   
    /// <summary>
    /// 枪的等级
    /// </summary>
    public enum GunRank
    {
        Normal,
        Rare,
        Epic,
        Legendary
    }

    public interface IGunModel : IModel
    {
        /// <summary>
        /// 枪的名字
        /// </summary>
        BindableProperty<string> Name { get; }
        /// <summary>
        /// 中文名字
        /// </summary>
        BindableProperty<string> ChineseName { get; }

        /// <summary>
        /// 枪的攻击速度
        /// </summary>
        BindableProperty<float> AttackSpeed { get; }
        /// <summary>
        /// 枪的攻击力
        /// </summary>
        BindableProperty<float> Attack { get; }
        /// <summary>
        /// 枪的等级
        /// </summary>
        public GunRank Rank { get; set; }
        BindableProperty<float> Coin { get; }
    }

    public class PistolModel : AbstractModel, IGunModel
    {
        public BindableProperty<string> Name { get; } = new BindableProperty<string>()
        {
            Value = "Pistol"
        };
        public BindableProperty<string> ChineseName { get; } = new BindableProperty<string>()
        {
            Value = "手枪"
        };
        public BindableProperty<float> AttackSpeed { get; } = new BindableProperty<float>()
        {
            Value = 1
        };
        public BindableProperty<float> Attack { get; } = new BindableProperty<float>()
        {
            Value = 3
        };

        public GunRank Rank { get; set; } = GunRank.Normal;

        public BindableProperty<float> Coin { get; } = new BindableProperty<float>()
        {
            Value = 1
        };

        protected override void OnInit()
        {

        }
    }

    public class SubmachineGunModel : AbstractModel, IGunModel
    {
        public BindableProperty<string> Name { get; } = new BindableProperty<string>()
        {
            Value = "SubmachineGun"
        };
        public BindableProperty<string> ChineseName { get; } = new BindableProperty<string>()
        {
            Value = "冲锋枪"
        };
        public BindableProperty<float> AttackSpeed { get; } = new BindableProperty<float>()
        {
            Value = 0.1f
        };
        public BindableProperty<float> Attack { get; } = new BindableProperty<float>()
        {
            Value = 2f
        };
        public GunRank Rank { get; set; } = GunRank.Normal;
        public BindableProperty<float> Coin { get; } = new BindableProperty<float>()
        {
            Value = 2
        };

        protected override void OnInit()
        {

        }
    }

    public class RifleModel : AbstractModel, IGunModel
    {
        public BindableProperty<string> Name { get; } = new BindableProperty<string>()
        {
            Value = "Rifle"
        };
        public BindableProperty<string> ChineseName { get; } = new BindableProperty<string>()
        {
            Value = "步枪"
        };
        public BindableProperty<float> AttackSpeed { get; } = new BindableProperty<float>()
        {
            Value = 0.45f
        };
        public BindableProperty<float> Attack { get; } = new BindableProperty<float>()
        {
            Value = 4f
        };
        public GunRank Rank { get; set; } = GunRank.Normal;
        public BindableProperty<float> Coin { get; } = new BindableProperty<float>()
        {
            Value = 3
        };

        protected override void OnInit()
        {

        }
    }

    public class NullGunModel : AbstractModel, IGunModel
    {
        public BindableProperty<string> Name { get; } = new BindableProperty<string>()
        {
            Value = "Null"
        };
        public BindableProperty<string> ChineseName { get; } = new BindableProperty<string>()
        {
            Value = "Null枪"
        };
        public BindableProperty<float> AttackSpeed => throw new System.NotImplementedException();

        public BindableProperty<float> Attack => throw new System.NotImplementedException();

        public GunRank Rank { get { return GunRank.Normal; } set => throw new System.NotImplementedException(); }

        public BindableProperty<float> Coin => throw new System.NotImplementedException();

        protected override void OnInit()
        {

        }
    }
}
