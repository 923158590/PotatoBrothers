using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class Enemy : Game2DController
{
    Rigidbody2D mRigibody;
    Animator mAnimator;

    private float mHP;
    private float mPlayerHurted;
    private float mRifleBulletHurted;
    private float mSubmachineGunBulletHurted;
    private float mPistolBulletHurted;

    void Start()
    {
        mRigibody = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        this.RegisterEvent<PlayerWinThisRoundEvent>(OnEnemyDestory);
        mHP = this.GetModel<IEnemyModel>().HP;
        mPlayerHurted = this.GetModel<IPlayerModel>().attack;
        mPistolBulletHurted = this.GetModel<IGunModel>().Attack;
        mSubmachineGunBulletHurted = this.GetModel<IGunModel>().Attack;
        mRifleBulletHurted = this.GetModel<IGunModel>().Attack;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        // ���Ѫ��С�ڵ���0����������
        if (mHP <= 0)
        {
            var e = new EnemyDieCommand(transform.localPosition);
            this.SendCommand(e);
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ׷�����
    /// </summary>
    private void FollowPlayer()
    {
        if (Vector2.Distance(Player.Instance.transform.position, transform.position) < 0.3f) return;
        // ��ȡEnemy���ƶ��ٶ�
        var speed = this.GetModel<IEnemyModel>().speed;
        // ������˳�����ҵķ�������
        Vector2 direction = (Player.Instance.transform.position - transform.position).normalized;
        // ���ö���״̬
        mAnimator.SetFloat("x", direction.x);
        mAnimator.SetFloat("y", direction.y);
        // ׷�����
        mRigibody.velocity = direction * speed;
    }

    /// <summary>
    /// �ݻٵ���
    /// </summary>
    /// <param name="e"></param>
    private void OnEnemyDestory(PlayerWinThisRoundEvent e)
    {
        mHP = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("RifleBullet"))
        {
            mHP -= mPlayerHurted + mRifleBulletHurted;
            // ��Ѫ
            var playerModel = this.GetModel<IPlayerModel>();
            int value = UnityEngine.Random.Range(0, 100);
            float lifeSteal = playerModel.lifeSteal;
            if (value < lifeSteal && playerModel.HP + 1 <= playerModel.maxHP)
            {
                playerModel.HP.Value += 1;
            }
        }
        else if (collision.gameObject.CompareTag("PistolBullet"))
        {
            mHP -= mPlayerHurted + mPistolBulletHurted;
            // ��Ѫ
            var playerModel = this.GetModel<IPlayerModel>();
            int value = UnityEngine.Random.Range(0, 100);
            float lifeSteal = playerModel.lifeSteal;
            if (value < lifeSteal && playerModel.HP + 1 <= playerModel.maxHP)
            {
                playerModel.HP.Value += 1;
            }
        }
        else if(collision.gameObject.CompareTag("SubmachineGunBullet"))
        {
            mHP -= mPlayerHurted + mSubmachineGunBulletHurted;
            // ��Ѫ
            var playerModel = this.GetModel<IPlayerModel>();
            int value = UnityEngine.Random.Range(0, 100);
            float lifeSteal = playerModel.lifeSteal;
            if (value < lifeSteal && playerModel.HP + 1 <= playerModel.maxHP)
            {
                playerModel.HP.Value += 1;
            }
        }
    }
    /// <summary>
    /// �������
    /// </summary>
    public void Revive()
    {
        mHP = this.GetModel<IEnemyModel>().HP;
    }
}
