using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface ILevelSystem : ISystem { }

    /// <summary>
    /// �ȼ�ϵͳ
    /// </summary>
    public class LevelSystem : AbstractSystem, ILevelSystem
    {
        protected override void OnInit()
        {
            this.RegisterEvent<AddCoinEvent>(OnEXPAdd);
            this.RegisterEvent<PlayerLevelUpEvent>(OnPlayerLevelUp);
        }

        /// <summary>
        /// �ȼ�����
        /// </summary>
        /// <param name="e"></param>
        private void OnPlayerLevelUp(PlayerLevelUpEvent e)
        {
            // ���������������ֵ��ʣ�ྭ��
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
                Debug.Log("LevelSystem����!�����ܷ��������������!");
            }
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="e"></param>
        private void OnEXPAdd(AddCoinEvent e)
        {
            this.GetModel<IPlayerModel>().EXP.Value += 2;
            this.SendEvent<ExpChangeEvent>();
        }
    }

}