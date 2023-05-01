using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IEnemyCreateSystem : ISystem
    {
        public void CreateEnemy(int x, int y);
    }

        /// <summary>
        /// 创建敌人系统（已废弃）
        /// </summary>
    public class EnemyCreateSystem : AbstractSystem, IEnemyCreateSystem
    {
        private GameObject mEnemyGo;
        protected override void OnInit()
        {
            mEnemyGo = Resources.Load<GameObject>("Prefab/Enemy");
        }

        void IEnemyCreateSystem.CreateEnemy(int x, int y) 
        {
            GameObject enemyGo = GameObject.Instantiate(mEnemyGo);
            enemyGo.transform.localPosition = new Vector2(x, y);
        }
    }
}

