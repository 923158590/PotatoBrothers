using QFramework;
using UnityEngine;

namespace Game
{
    public class InitGameCommand : AbstractCommand
    {
        private readonly int mapW, mapH;

        public InitGameCommand(int w, int h)
        {
            mapW = w;
            mapH = h;
        }
        /// <summary>
        /// 游戏初始化命令
        /// </summary>
        protected override void OnExecute()
        {
            //CenterCamera(mapW, mapH);
            var map = this.GetSystem<IGridNodeSystem>();
            map.CreateGrid(mapW, mapH);
            var p = map.FindBlockPos(mapW, mapH);
            this.GetSystem<IPlayerCreateSystem>().CreatePlayer(10, 10);
            this.GetSystem<IEnemyCreateSystem>().CreateEnemy(p.x, p.y);
            //this.SendEvent(new CreateFoodEvent() { pos = map.FindBlockPos(mapW, mapH) });
            this.SendEvent<CreateGunEvent>();
            this.SendEvent<GameStartEvent>();
            this.SendEvent<GameInitEndEvent>();
        }
        /// <summary>
        /// 居中摄像机
        /// </summary>
        private void CenterCamera(int w, int h)
        {
            Camera.main.transform.localPosition = new Vector3((w - 1) * 0.5f, (h - 1) * 0.5f, -10f);
            Camera.main.orthographicSize = w > h ? w * 0.5f : h * 0.5f;
        }
    }
}