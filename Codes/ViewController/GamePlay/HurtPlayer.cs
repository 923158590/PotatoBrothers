using Game;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class HurtPlayer : Game2DController
    {
        // �˺�ֵ
        private float hurt;
        // �Ƿ���Թ���
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
                // ������ܹ���ֱ�ӷ���
                if (!isAttack) return;
                Debug.Log("���-" + hurt + "HP");
                this.SendCommand(new HurtPlayerCommand(hurt));
            }
        }

        /// <summary>
        /// ������޵�ʱ���˲��ܹ���
        /// </summary>
        /// <param name="e"></param>
        private void OnPlayerInvincible(PlayerInvincibleEvent e)
        {
            Debug.Log("�޵�ʱ�䣬�޷�����");
            // ��ֹ����
            isAttack = false;
            // 1���Ӻ�ָ�����
            this.GetSystem<ITimeSystem>().AddDelayTask(2f, StartAttack, false);
        }

        /// <summary>
        /// �ָ�����
        /// </summary>
        void StartAttack()
        {
            Debug.Log("�޵�ʱ����������˿�ʼ����");
            isAttack = true;
        }
    }
}
