using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Game
{
    public class SubmachineGun : Game2DController
    {
        private Transform target;
        private float rotationSpeed = 30f;
        private float attackRange;
        private BoxCollider2D mBoxCollider2D;
        private IGunModel mGun;
        private bool isDestory;

        // Start is called before the first frame update
        void Start()
        {
            mBoxCollider2D = GetComponent<BoxCollider2D>();
            mGun = new SubmachineGunModel();
            // ��Ϸ��ʼʱ����ǹ�Ĺ�����Χ
            attackRange = this.GetModel<IPlayerModel>().attackRange.Value;
            mBoxCollider2D.size = new Vector2(attackRange, attackRange);
            // �����ӵ�
            this.GetSystem<ITimeSystem>().AddDelayTask(mGun.AttackSpeed, Shooting, true);

            this.RegisterEvent<PlayerWinThisRoundEvent>(OnDestoryGun);
            this.RegisterEvent<GameOverEvent>(OnGameOver);
        }

        private void OnGameOver(GameOverEvent e)
        {
            isDestory = true;
        }

        private void OnDestoryGun(PlayerWinThisRoundEvent e)
        {
            isDestory = true;
        }

        // Update is called once per frame
        void Update()
        {
            AimAtEnemy();
            if (isDestory)
            {
                GameObject.Destroy(gameObject);
            }
        }

        /// <summary>
        /// ��ǹ��׼����
        /// </summary>
        private void AimAtEnemy()
        {
            if (target == null) return;
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        /// <summary>
        /// �����ӵ�
        /// </summary>
        private void Shooting()
        {
            if (target == null) return;
            try 
            { 
                Vector2 direction = (target.transform.position - transform.position).normalized;
                var go = BulletObjectPool.Instance.GetObject();
                go.tag = "SubmachineGunBullet";
                go.GetComponent<Rigidbody2D>().AddForce(direction * 2, ForceMode2D.Impulse);
                go.transform.localRotation = transform.localRotation;
                go.transform.localPosition = transform.position;
            }
            catch
            {

            }
        }


        private void OnTriggerStay2D(Collider2D collision)
        {
            // ����ҵ����˾Ͱ�target����Ϊ���� ����ΪNull
            if(collision.gameObject.CompareTag("Enemy") && collision.gameObject.activeInHierarchy)
            {
                target = collision.transform;
            }
            else
            {
                target = null;
            }
        }
    }
}
