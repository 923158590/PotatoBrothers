using QFramework;

namespace Game
{
    public interface IPlayerModel : IModel
    {
        /// <summary>
        /// 玩家血量上限
        /// </summary>
        BindableProperty<float> maxHP { get; }
        /// <summary>
        /// 玩家当前血量
        /// </summary>
        BindableProperty<float> HP { get; }
        /// <summary>
        /// 玩家经验
        /// </summary>
        BindableProperty<float> EXP { get; }
        /// <summary>
        /// 玩家升级所需经验值
        /// </summary>
        BindableProperty<float> maxEXP { get; }
        /// <summary>
        /// 玩家攻击力
        /// </summary>
        BindableProperty<float> attack { get; }
        /// <summary>
        /// 玩家攻击速度
        /// </summary>
        BindableProperty<float> attackSpeed { get; }
        /// <summary>
        /// 玩家移速
        /// </summary>
        BindableProperty<float> speed { get; }
        /// <summary>
        /// 玩家恢复速率
        /// </summary>
        BindableProperty<float> recover { get; }
        /// <summary>
        /// 玩家的幸运度
        /// </summary>
        BindableProperty<float> lucky { get; }
        /// <summary>
        /// 攻击范围
        /// </summary>
        BindableProperty<float> attackRange { get; }
        /// <summary>
        /// 玩家可以触发吸血的概率
        /// </summary>
        BindableProperty<float> lifeSteal { get; }
        /// <summary>
        /// 玩家持有的金币数
        /// </summary>
        BindableProperty<float> coin { get; }
        /// <summary>
        /// 玩家等级
        /// </summary>
        BindableProperty<float> LV { get; }

    }

    public class PlayerModel : AbstractModel, IPlayerModel
    {
        protected override void OnInit()
        {

        }


        public BindableProperty<float> maxHP { get; } = new BindableProperty<float>()
        {
            Value = 5
        };
        public BindableProperty<float> HP { get; } = new BindableProperty<float>()
        {
            Value = 5
        };
        public BindableProperty<float> EXP { get; } = new BindableProperty<float>()
        {
            Value = 0
        };
        public BindableProperty<float> maxEXP { get; } = new BindableProperty<float>()
        {
            Value = 5
        };
        public BindableProperty<float> attack { get; } = new BindableProperty<float>()
        {
            Value = 3
        };
        public BindableProperty<float> attackSpeed { get; } = new BindableProperty<float>()
        {
            Value = 0
        };
        public BindableProperty<float> speed { get; } = new BindableProperty<float>()
        {
            Value = 1
        };
        public BindableProperty<float> recover { get; } = new BindableProperty<float>()
        {
            Value = 0
        };
        public BindableProperty<float> lucky { get; } = new BindableProperty<float>()
        {
            Value = 1
        };
        public BindableProperty<float> attackRange { get; } = new BindableProperty<float>()
        {
            Value = 30
        };
        public BindableProperty<float> lifeSteal { get; } = new BindableProperty<float>()
        {
            Value = 0
        };
        public BindableProperty<float> coin { get; } = new BindableProperty<float>()
        {
            Value = 1000
        };
        public BindableProperty<float> LV { get; } = new BindableProperty<float>()
        {
            Value = 0
        };
    }
}