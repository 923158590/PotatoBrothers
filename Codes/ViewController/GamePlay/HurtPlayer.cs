using Game;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class HurtPlayer : Game2DController
    {
        // 伤害值
        private float hurt;
        // 是否可以攻击
        private bool isAttack;

        // Start is called before the first frame update
        void Start()
        {
            isAttack = true;
            hurt = this.GetModel<IEnemyModel>().attack;
            this.RegisterEvent<PlayerInvincibleEvent>(OnPlayerInvincible);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                // 如果不能攻击直接返回
                if (!isAttack) return;
                Debug.Log("玩家-" + hurt + "HP");
                this.SendCommand(new HurtPlayerCommand(hurt));
            }
        }

        /// <summary>
        /// 当玩家无敌时敌人不能攻击
        /// </summary>
        /// <param name="e"></param>
        private void OnPlayerInvincible(PlayerInvincibleEvent e)
        {
            Debug.Log("无敌时间，无法攻击");
            // 禁止攻击
            isAttack = false;
            // 1秒钟后恢复攻击
            this.GetSystem<ITimeSystem>().AddDelayTask(2f, StartAttack, false);
        }

        /// <summary>
        /// 恢复攻击
        /// </summary>
        void StartAttack()
        {
            Debug.Log("无敌时间结束，敌人开始攻击");
            isAttack = true;
        }
    }
}
