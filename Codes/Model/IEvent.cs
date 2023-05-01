using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    /// <summary>
    /// �����ڵ��¼�
    /// <param name="pos">������λ��</param>
    /// <param name="x">�ڵ������</param>
    /// </summary>
    public struct CreateNodeEvent { public Vector2 pos; public Node.E_Type type; }
    /// <summary>
    /// ��������¼�
    /// </summary>
    public struct CreatePlayerEvent { public Vector2 pos; }
    /// <summary>
    /// ��Ϸ��ʼ������¼�
    /// </summary>
    public struct GameInitEndEvent { }
    /// <summary>
    /// �������غϻ�ʤ�¼�
    /// </summary>
    public struct PlayerWinThisRoundEvent { }
    /// <summary>
    /// ��������¼�
    /// </summary>
    public struct PlayerHurtEvent { public float HP; public float maxHP; }
    /// <summary>
    /// ��һ�ȡ����ֵ�¼�
    /// </summary>
    public struct ExpChangeEvent { public float EXP; public float maxEXP; }
    /// <summary>
    /// ��һ�ȡѪ���¼�
    /// </summary>
    public struct PlayerHPChangeEvent { public float HP; }
    /// <summary>
    /// ���������¼�
    /// </summary>
    public struct EnemyDieEvent { public Vector2 pos; }
    /// <summary>
    /// ����޵��¼�
    /// </summary>
    public struct PlayerInvincibleEvent { }
    /// <summary>
    /// ��������¼�
    /// </summary>
    public struct PlayerLevelUpEvent { }
    /// <summary>
    /// ��Ϸʧ���¼�
    /// </summary>
    public struct GameOverEvent { }
    /// <summary>
    /// ��ȡ����¼�
    /// </summary>
    public struct AddCoinEvent { public float coin; }
    /// <summary>
    /// ʹ�ý���¼�
    /// </summary>
    public struct SubCoinEvent { public float coin; }
    /// <summary>
    /// �����任����¼�
    /// </summary>
    public struct CoinChangeEvent { }
    /// <summary>
    /// ���غϵ�ʱ�䷢���ı�
    /// </summary>
    public struct TimeChangeInThisRoundEvent { public float time; }
    /// <summary>
    /// ��Ϸ��ʼ�¼�
    /// </summary>
    public struct GameStartEvent { }
    /// <summary>
    /// �ϳ������¼�
    /// </summary>
    /// <param name="index">����������</param>
    /// <param name="rank">������Ʒ��</param>
    public struct CraftGunEvent { public int index; }
    /// <summary>
    /// �ϳ������ɹ��¼�
    /// </summary>
    public struct CraftGunSuccessEvent { public int index; public GunRank rank; }
    /// <summary>
    /// ��������¼�
    /// </summary>
    public struct AddGunEvent { public IGunModel gun; }
    /// <summary>
    /// ɾ�������¼�
    /// </summary>
    public struct RemoveGunEvent { public IGunModel gun; }
    /// <summary>
    /// ����ǹ֧������ǹ֧�¼�
    /// </summary>
    public struct ShoppingAndUpdateGunEvent { }
    /// <summary>
    /// ��Ϸ��ʼʱ���������¼�
    /// </summary>
    public struct CreateGunEvent { }
    /// <summary>
    /// �����¼�
    /// </summary>
    public struct ShoppingEvent { public int index; }
    /// <summary>
    /// ˢ���̵��¼�
    /// </summary>
    public struct RefreshShopEvent { }
    /// <summary>
    /// ˢ���̵�ɹ��¼�
    /// </summary>
    public struct RefreshShopSuccessfulEvent { public List<IGunModel> Shop; }
    /// <summary>
    /// ��ʼ���̵��¼�
    /// </summary>
    public struct InitShopEvent { }
    /// <summary>
    /// ��ʼ���̵�ɹ��¼�
    /// </summary>
    public struct InitShopSuccessfulEvent { }
    /// <summary>
    /// ������Ʒ�������Ϣ
    /// </summary>
    public struct UpdateShoppingViewEvent { }
    /// <summary>
    /// ��ʼ��һ���¼�
    /// </summary>
    public struct NextRoundStartEvent { }
    /// <summary>
    /// ��ס��Ʒ�����
    /// </summary>
    public struct LockShopItemEvent { public int index; public bool isLock; }
    /// <summary>
    /// ������Ѫ�����ͱ䶯�¼�
    /// </summary>
    public struct PlayerMaxHpChangeEvent { public float value; }
    /// <summary>
    /// ��ҹ����������䶯�¼�
    /// </summary>
    public struct PlayerAttackChangeEvent { public float value; }
    /// <summary>
    /// ��ҹ����ٶȷ����䶯�¼�
    /// </summary>
    public struct PlayerAttackSpeedChangeEvent { public float value; }
    /// <summary>
    /// ����ƶ��ٶȱ任�¼�
    /// </summary>
    public struct PlayerSpeedChangeEvent { public float value; }
    /// <summary>
    /// ��һָ��ٶȷ����仯�¼�
    /// </summary>
    public struct PlayerRecoverChangeEvent { public float value; }
    /// <summary>
    /// ������˶ȷ����任�¼�
    /// </summary>
    public struct PlayerLuckyChangeEvent { public float value; }
    /// <summary>
    /// �����Ѫ���ʷ����仯�¼�
    /// </summary>
    public struct PlayerLifeStealChangeEvent { public float value; }
    /// <summary>
    /// ��ʼ���������������¼�
    /// </summary>
    public struct InitSkillLevelUpViewEvent { }
    /// <summary>
    /// ��־��Ϣ�仯�¼�
    /// </summary>
    public struct LogChanegEvent { }
    /// <summary>
    /// ��ǹ��ǹ�¼�
    /// </summary>
    public struct PistolShootingEvent { public Vector2 pos; public Vector2 direction; public Quaternion rotation; }
    /// <summary>
    /// ���ǹ��ǹ�¼�
    /// </summary>
    public struct SubmachineGunShootingEvent { public Vector2 pos; public Vector2 direction; public Quaternion rotation; }
    /// <summary>
    /// ��ǹ��ǹ�¼�
    /// </summary>
    public struct RifleShootingEvent { public Vector2 pos; public Vector2 direction; public Quaternion rotation; }
}
