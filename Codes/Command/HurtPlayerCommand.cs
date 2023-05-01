using QFramework;
using UnityEngine.SceneManagement;

namespace Game
{
    /// <summary>
    /// 玩家受伤命令
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
            // 发送玩家受伤事件和玩家无敌事件
            var e = new PlayerHPChangeEvent();
            var e2 = new PlayerInvincibleEvent();
            this.SendEvent(e);
            this.SendEvent(e2);
            // 如果玩家血量少于0，结束游戏
            if (playerModel.HP.Value <= 0)
            {
                this.SendEvent<GameOverEvent>();
            }
        }
    }
}