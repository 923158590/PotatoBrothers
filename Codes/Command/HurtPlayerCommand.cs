using QFramework;
using UnityEngine.SceneManagement;

namespace Game
{
    /// <summary>
    /// �����������
    /// </summary>
    public class HurtPlayerCommand : AbstractCommand
    {
        private readonly float mHurt;

        public HurtPlayerCommand(float hurt = 1)
        {
            mHurt = hurt;
        }

        protected override void OnExecute()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.HP.Value -= mHurt;
            // ������������¼�������޵��¼�
            var e = new PlayerHPChangeEvent();
            var e2 = new PlayerInvincibleEvent();
            this.SendEvent(e);
            this.SendEvent(e2);
            // ������Ѫ������0��������Ϸ
            if (playerModel.HP.Value <= 0)
            {
                this.SendEvent<GameOverEvent>();
            }
        }
    }
}