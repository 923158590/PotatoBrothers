using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Game
{   
    /// <summary>
    /// ǹ�ĵȼ�
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
        /// ǹ������
        /// </summary>
        BindableProperty<string> Name { get; }
        /// <summary>
        /// ��������
        /// </summary>
        BindableProperty<string> ChineseName { get; }

        /// <summary>
        /// ǹ�Ĺ����ٶ�
        /// </summary>
        BindableProperty<float> AttackSpeed { get; }
        /// <summary>
        /// ǹ�Ĺ�����
        /// </summary>
        BindableProperty<float> Attack { get; }
        /// <summary>
        /// ǹ�ĵȼ�
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
            Value = "��ǹ"
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
            Value = "���ǹ"
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
            Value = "��ǹ"
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
            Value = "Nullǹ"
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
