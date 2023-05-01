using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface ILevelSystem : ISystem { }

    /// <summary>
    /// 等级系统
    /// </summary>
    public class LevelSystem : AbstractSystem, ILevelSystem
    {
        protected override void OnInit()
        {
            this.RegisterEvent<AddCoinEvent>(OnEXPAdd);
            this.RegisterEvent<PlayerLevelUpEvent>(OnPlayerLevelUp);
        }

        /// <summary>
        /// 等级升级
        /// </summary>
        /// <param name="e"></param>
        private void OnPlayerLevelUp(PlayerLevelUpEvent e)
        {
            // 算出超过升级经验值的剩余经验
            var exp = this.GetModel<IPlayerModel>().EXP.Value;
            var maxExp = this.GetModel<IPlayerModel>().maxEXP.Value;
            float tempExp = exp - maxExp;

            if(tempExp >= 0) 
            {
                this.GetModel<IPlayerModel>().EXP.Value = tempExp;
                this.GetModel<IPlayerModel>().maxEXP.Value++;
                this.GetModel<IPlayerModel>().LV.Value++;
                this.SendEvent<ExpChangeEvent>();
            }
            else
            {
                Debug.Log("LevelSystem出错!不可能发生负经验的事情!");
            }
        }

        /// <summary>
        /// 获取经验
        /// </summary>
        /// <param name="e"></param>
        private void OnEXPAdd(AddCoinEvent e)
        {
            this.GetModel<IPlayerModel>().EXP.Value += 2;
            this.SendEvent<ExpChangeEvent>();
        }
    }

}