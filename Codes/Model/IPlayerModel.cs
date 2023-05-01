using QFramework;

namespace Game
{
    public interface IPlayerModel : IModel
    {
        /// <summary>
        /// ���Ѫ������
        /// </summary>
        BindableProperty<float> maxHP { get; }
        /// <summary>
        /// ��ҵ�ǰѪ��
        /// </summary>
        BindableProperty<float> HP { get; }
        /// <summary>
        /// ��Ҿ���
        /// </summary>
        BindableProperty<float> EXP { get; }
        /// <summary>
        /// ����������辭��ֵ
        /// </summary>
        BindableProperty<float> maxEXP { get; }
        /// <summary>
        /// ��ҹ�����
        /// </summary>
        BindableProperty<float> attack { get; }
        /// <summary>
        /// ��ҹ����ٶ�
        /// </summary>
        BindableProperty<float> attackSpeed { get; }
        /// <summary>
        /// �������
        /// </summary>
        BindableProperty<float> speed { get; }
        /// <summary>
        /// ��һָ�����
        /// </summary>
        BindableProperty<float> recover { get; }
        /// <summary>
        /// ��ҵ����˶�
        /// </summary>
        BindableProperty<float> lucky { get; }
        /// <summary>
        /// ������Χ
        /// </summary>
        BindableProperty<float> attackRange { get; }
        /// <summary>
        /// ��ҿ��Դ�����Ѫ�ĸ���
        /// </summary>
        BindableProperty<float> lifeSteal { get; }
        /// <summary>
        /// ��ҳ��еĽ����
        /// </summary>
        BindableProperty<float> coin { get; }
        /// <summary>
        /// ��ҵȼ�
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