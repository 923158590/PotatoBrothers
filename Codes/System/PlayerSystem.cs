using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IPlayerSystem : ISystem { }
    public class PlayerSystem : AbstractSystem, IPlayerSystem
    {
        protected override void OnInit()
        {
            this.RegisterEvent<PlayerMaxHpChangeEvent>(OnPlayerMaxHPChange);
            this.RegisterEvent<PlayerAttackChangeEvent>(OnPlayerAttackChange);
            this.RegisterEvent<PlayerAttackSpeedChangeEvent>(OnPlayerAttackSpeedChange);
            this.RegisterEvent<PlayerSpeedChangeEvent>(OnPlayerSpeedChange);
            this.RegisterEvent<PlayerRecoverChangeEvent>(OnPlayerRecoverChange);
            this.RegisterEvent<PlayerLuckyChangeEvent>(OnPlayerLuckyChange);
            this.RegisterEvent<PlayerLifeStealChangeEvent>(OnPlayerLifeStealChange);

            this.GetSystem<ITimeSystem>().AddDelayTask(10, RecoverTask, true);

        }

        private void OnPlayerLifeStealChange(PlayerLifeStealChangeEvent obj)
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.lifeSteal.Value += obj.value;
        }

        private void OnPlayerLuckyChange(PlayerLuckyChangeEvent obj)
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.lucky.Value += obj.value;
        }

        private void OnPlayerRecoverChange(PlayerRecoverChangeEvent obj)
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.recover.Value += obj.value;
        }

        private void OnPlayerSpeedChange(PlayerSpeedChangeEvent obj)
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.speed.Value += obj.value;
        }

        private void OnPlayerAttackSpeedChange(PlayerAttackSpeedChangeEvent obj)
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.attackSpeed.Value += obj.value;
        }

        private void OnPlayerAttackChange(PlayerAttackChangeEvent obj)
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.attack.Value += obj.value;
        }

        private void OnPlayerMaxHPChange(PlayerMaxHpChangeEvent obj)
        {
            var playerModel = this.GetModel<IPlayerModel>();
            playerModel.maxHP.Value += obj.value;
            playerModel.HP.Value += obj.value;
            this.SendEvent<PlayerHPChangeEvent>();
        }

        private void RecoverTask()
        {
            var playerModel = this.GetModel<IPlayerModel>();
            if(playerModel.maxHP < playerModel.HP + playerModel.recover)
            {
                playerModel.HP.Value = playerModel.maxHP.Value;
                this.SendEvent<PlayerHPChangeEvent>();
                return;
            }
            playerModel.HP.Value += playerModel.recover.Value;
            this.SendEvent<PlayerHPChangeEvent>();
        }
    }
}
