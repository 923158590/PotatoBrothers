using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyModel : IModel
{
    /// <summary>
    /// 敌人当前血量
    /// </summary>
    BindableProperty<float> HP { get; }
    /// <summary>
    /// 敌人攻击力
    /// </summary>
    BindableProperty<float> attack { get; }
    /// <summary>
    /// 敌人移动速度
    /// </summary>
    BindableProperty<float> speed { get; }
}

public class EnemyModel : AbstractModel, IEnemyModel
{
    protected override void OnInit()
    {

    }

    public BindableProperty<float> HP { get; } = new BindableProperty<float>()
    {
        Value = 10
    };
    public BindableProperty<float> attack { get; } = new BindableProperty<float>()
    {
        Value = 3
    };
    public BindableProperty<float> speed { get; } = new BindableProperty<float>()
    {
        Value = 0.8f
    };
}
