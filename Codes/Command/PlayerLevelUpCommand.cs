using QFramework;

namespace Game
{
    /// <summary>
    /// 玩家升级命令
    /// </summary>
    public class PlayerLevelUpCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            // 增加生命值 发送血量变化事件
            this.GetModel<IPlayerModel>().HP.Value++;
            this.GetModel<IPlayerModel>().maxHP.Value++;
            this.SendEvent<PlayerHPChangeEvent>();
            // 发送玩家升级事件
            this.SendEvent<PlayerLevelUpEvent>();
        }
    }
}