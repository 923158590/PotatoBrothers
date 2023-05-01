using QFramework;

namespace Game
{
    public class StartNextRoundCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<NextRoundStartEvent>();
        }
    }
}