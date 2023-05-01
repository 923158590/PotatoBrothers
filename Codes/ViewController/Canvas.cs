using UnityEngine;
using QFramework;
using UnityEngine.UI;
using System;

namespace Game
{
	public partial class Canvas : ViewController, IController
    {
		void Start()
		{
			this.RegisterEvent<PlayerHPChangeEvent>(OnPlayerHPChange);
			this.RegisterEvent<ExpChangeEvent>(OnPlayerExpChange);
            this.RegisterEvent<TimeChangeInThisRoundEvent>(OnTimeChange);
            this.RegisterEvent<CoinChangeEvent>(OnCoinChange);
        }

        private void OnCoinChange(CoinChangeEvent e)
        {
            CoinText.text = "Coin:" + this.GetModel<IPlayerModel>().coin;
        }

        private void OnTimeChange(TimeChangeInThisRoundEvent e)
        {
            TimeText.text = "Time:" + e.time;
        }

        private void OnPlayerHPChange(PlayerHPChangeEvent e)
		{
            HPText.text = "HP:" + this.GetModel<IPlayerModel>().HP + "/" + this.GetModel<IPlayerModel>().maxHP.Value;
		}

        private void OnPlayerExpChange(ExpChangeEvent e)
        {
            EXPText.text = "EXP:" + this.GetModel<IPlayerModel>().EXP + "/" + this.GetModel<IPlayerModel>().maxEXP;
            LVText.text = "LV:" + this.GetModel<IPlayerModel>().LV;
        }

        public IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }
    }
}
