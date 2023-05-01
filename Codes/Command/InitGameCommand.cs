using QFramework;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 初始化游戏命令
    /// </summary>
    public class InitGameCommand : AbstractCommand
    {
        private readonly int mapW, mapH;

        // 初始化地图的大小
        public InitGameCommand(int w, int h)
        {
            mapW = w;
            mapH = h;
        }

        protected override void OnExecute()
        {
            // 创建地图
            var map = this.GetSystem<IGridNodeSystem>();
            map.CreateGrid(mapW, mapH);
            var p = map.FindBlockPos(mapW, mapH);
            // 创建主角
            this.GetSystem<IPlayerCreateSystem>().CreatePlayer(10, 10);
            // 创建敌人
            this.GetSystem<IEnemyCreateSystem>().CreateEnemy(p.x, p.y);
            // 发送初始化游戏命令
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