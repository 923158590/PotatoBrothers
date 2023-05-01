using QFramework;

namespace Game
{
    /// <summary>
    /// 开始下一回合命令
    /// </summary>
    public class StartNextRoundCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<NextRoundStartEvent>();
        }
    }
}