using Game;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 敌人死亡命令
    /// </summary>
    public class EnemyDieCommand : AbstractCommand
    {
        // 用于发送敌人死亡的位置
        Vector2 mPos;

        public EnemyDieCommand()
        {

        }

        public EnemyDieCommand(Vector2 pos)
        {
            mPos = pos;
        }

        protected override void OnExecute()
        {
            EnemyDieEvent e = new EnemyDieEvent();
            e.pos = mPos;
            this.SendEvent(e);
        }
    }
}
