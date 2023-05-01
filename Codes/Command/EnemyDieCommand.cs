using Game;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// ������������
    /// </summary>
    public class EnemyDieCommand : AbstractCommand
    {
        // ���ڷ��͵���������λ��
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
