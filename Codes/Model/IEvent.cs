using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    /// <summary>
    /// 创建节点事件
    /// <param name="pos">创建的位置</param>
    /// <param name="x">节点的类型</param>
    /// </summary>
    public struct CreateNodeEvent { public Vector2 pos; public Node.E_Type type; }
    /// <summary>
    /// 创建玩家事件
    /// </summary>
    public struct CreatePlayerEvent { public Vector2 pos; }
    /// <summary>
    /// 游戏初始化完成事件
    /// </summary>
    public struct GameInitEndEvent { }
    /// <summary>
    /// 玩家在这回合获胜事件
    /// </summary>
    public struct PlayerWinThisRoundEvent { }
    /// <summary>
    /// 玩家受伤事件
    /// </summary>
    public struct PlayerHurtEvent { public float HP; public float maxHP; }
    /// <summary>
    /// 玩家获取经验值事件
    /// </summary>
    public struct ExpChangeEvent { public float EXP; public float maxEXP; }
    /// <summary>
    /// 玩家获取血量事件
    /// </summary>
    public struct PlayerHPChangeEvent { public float HP; }
    /// <summary>
    /// 敌人死亡事件
    /// </summary>
    public struct EnemyDieEvent { public Vector2 pos; }
    /// <summary>
    /// 玩家无敌事件
    /// </summary>
    public struct PlayerInvincibleEvent { }
    /// <summary>
    /// 玩家升级事件
    /// </summary>
    public struct PlayerLevelUpEvent { }
    /// <summary>
    /// 游戏失败事件
    /// </summary>
    public struct GameOverEvent { }
    /// <summary>
    /// 获取金币事件
    /// </summary>
    public struct AddCoinEvent { public float coin; }
    /// <summary>
    /// 使用金币事件
    /// </summary>
    public struct SubCoinEvent { public float coin; }
    /// <summary>
    /// 发生变换金币事件
    /// </summary>
    public struct CoinChangeEvent { }
    /// <summary>
    /// 本回合的时间发生改变
    /// </summary>
    public struct TimeChangeInThisRoundEvent { public float time; }
    /// <summary>
    /// 游戏开始事件
    /// </summary>
    public struct GameStartEvent { }
    /// <summary>
    /// 合成武器事件
    /// </summary>
    /// <param name="index">武器的索引</param>
    /// <param name="rank">武器的品质</param>
    public struct CraftGunEvent { public int index; }
    /// <summary>
    /// 合成武器成功事件
    /// </summary>
    public struct CraftGunSuccessEvent { public int index; public GunRank rank; }
    /// <summary>
    /// 添加武器事件
    /// </summary>
    public struct AddGunEvent { public IGunModel gun; }
    /// <summary>
    /// 删除武器事件
    /// </summary>
    public struct RemoveGunEvent { public IGunModel gun; }
    /// <summary>
    /// 购买枪支并升级枪支事件
    /// </summary>
    public struct ShoppingAndUpdateGunEvent { }
    /// <summary>
    /// 游戏开始时创建武器事件
    /// </summary>
    public struct CreateGunEvent { }
    /// <summary>
    /// 购物事件
    /// </summary>
    public struct ShoppingEvent { public int index; }
    /// <summary>
    /// 刷新商店事件
    /// </summary>
    public struct RefreshShopEvent { }
    /// <summary>
    /// 刷新商店成功事件
    /// </summary>
    public struct RefreshShopSuccessfulEvent { public List<IGunModel> Shop; }
    /// <summary>
    /// 初始化商店事件
    /// </summary>
    public struct InitShopEvent { }
    /// <summary>
    /// 初始化商店成功事件
    /// </summary>
    public struct InitShopSuccessfulEvent { }
    /// <summary>
    /// 更新商品界面的信息
    /// </summary>
    public struct UpdateShoppingViewEvent { }
    /// <summary>
    /// 开始下一关事件
    /// </summary>
    public struct NextRoundStartEvent { }
    /// <summary>
    /// 锁住商品的物件
    /// </summary>
    public struct LockShopItemEvent { public int index; public bool isLock; }
    /// <summary>
    /// 玩家最大血量发送变动事件
    /// </summary>
    public struct PlayerMaxHpChangeEvent { public float value; }
    /// <summary>
    /// 玩家攻击力发生变动事件
    /// </summary>
    public struct PlayerAttackChangeEvent { public float value; }
    /// <summary>
    /// 玩家攻击速度发生变动事件
    /// </summary>
    public struct PlayerAttackSpeedChangeEvent { public float value; }
    /// <summary>
    /// 玩家移动速度变换事件
    /// </summary>
    public struct PlayerSpeedChangeEvent { public float value; }
    /// <summary>
    /// 玩家恢复速度发生变化事件
    /// </summary>
    public struct PlayerRecoverChangeEvent { public float value; }
    /// <summary>
    /// 玩家幸运度发生变换事件
    /// </summary>
    public struct PlayerLuckyChangeEvent { public float value; }
    /// <summary>
    /// 玩家吸血概率发生变化事件
    /// </summary>
    public struct PlayerLifeStealChangeEvent { public float value; }
    /// <summary>
    /// 初始化技能升级界面事件
    /// </summary>
    public struct InitSkillLevelUpViewEvent { }
    /// <summary>
    /// 日志信息变化事件
    /// </summary>
    public struct LogChanegEvent { }
    /// <summary>
    /// 手枪开枪事件
    /// </summary>
    public struct PistolShootingEvent { public Vector2 pos; public Vector2 direction; public Quaternion rotation; }
    /// <summary>
    /// 冲锋枪开枪事件
    /// </summary>
    public struct SubmachineGunShootingEvent { public Vector2 pos; public Vector2 direction; public Quaternion rotation; }
    /// <summary>
    /// 步枪开枪事件
    /// </summary>
    public struct RifleShootingEvent { public Vector2 pos; public Vector2 direction; public Quaternion rotation; }
}
