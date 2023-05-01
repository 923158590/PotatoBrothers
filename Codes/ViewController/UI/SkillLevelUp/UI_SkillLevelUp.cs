using UnityEngine;
using QFramework;
using System;
using System.Collections.Generic;

namespace Game
{
	public partial class UI_SkillLevelUp : ViewController, IController, ICanSendEvent
	{
        private List<RectTransform> mTransform;
        private List<Vector2> mPos;

        public IArchitecture GetArchitecture()
        {
			return Game.Interface;
        }

        void Start()
		{
            mTransform = new List<RectTransform>();
            mPos = new List<Vector2>();

            this.RegisterEvent<InitSkillLevelUpViewEvent>(OnInitSkillLevelUpView);

            mTransform.Add(MaxHP);
            mTransform.Add(Speed);
            mTransform.Add(AttackSpeed);
            mTransform.Add(Attack);
            mTransform.Add(Lucky);
            mTransform.Add(Recover);
            mTransform.Add(LifeSteal);

            mPos.Add(new Vector2(-275, 0));
            mPos.Add(new Vector2(-95, 0));
            mPos.Add(new Vector2(95, 0));
            mPos.Add(new Vector2(275, 0));

            MaxHP_Button.onClick.AddListener(() =>
            {
                PlayerMaxHpChangeEvent e = new PlayerMaxHpChangeEvent();
                e.value = 1;
                this.SendEvent(e);
                this.SendEvent<InitShopEvent>();
                gameObject.SetActive(false);
            });

            Speed_Button.onClick.AddListener(() =>
            {
                PlayerSpeedChangeEvent e = new PlayerSpeedChangeEvent();
                e.value = 1;
                this.SendEvent(e);
                this.SendEvent<InitShopEvent>();
                gameObject.SetActive(false);
            });

            AttackSpeed_Button.onClick.AddListener(() =>
            {
                PlayerAttackSpeedChangeEvent e = new PlayerAttackSpeedChangeEvent();
                e.value = 1;
                this.SendEvent(e);
                this.SendEvent<InitShopEvent>();
                gameObject.SetActive(false);
            });

            Attack_Button.onClick.AddListener(() =>
            {
                PlayerAttackChangeEvent e = new PlayerAttackChangeEvent();
                e.value = 1;
                this.SendEvent(e);
                this.SendEvent<InitShopEvent>();
                gameObject.SetActive(false);
            });

            Lucky_Button.onClick.AddListener(() =>
            {
                PlayerLuckyChangeEvent e = new PlayerLuckyChangeEvent();
                e.value = 1;
                this.SendEvent(e);
                this.SendEvent<InitShopEvent>();
                gameObject.SetActive(false);
            });

            Recover_Button.onClick.AddListener(() =>
            {
                PlayerRecoverChangeEvent e = new PlayerRecoverChangeEvent();
                e.value = 1;
                this.SendEvent(e);
                this.SendEvent<InitShopEvent>();
                gameObject.SetActive(false);
            });

            LifeSteal_Button.onClick.AddListener(() =>
            {
                PlayerLifeStealChangeEvent e = new PlayerLifeStealChangeEvent();
                e.value = 1;
                this.SendEvent(e);
                this.SendEvent<InitShopEvent>();
                gameObject.SetActive(false);
            });
        }

        private void OnInitSkillLevelUpView(InitSkillLevelUpViewEvent e)
        {
            int value1 = UnityEngine.Random.Range(0, 2);
            int value2 = UnityEngine.Random.Range(2, 4);
            int value3 = UnityEngine.Random.Range(4, 6);
            int value4 = UnityEngine.Random.Range(6, 7);

            mTransform[value1].localPosition = mPos[0];
            mTransform[value2].localPosition = mPos[1];
            mTransform[value3].localPosition = mPos[2];
            mTransform[value4].localPosition = mPos[3];

            mTransform[value1].gameObject.SetActive(true);
            mTransform[value2].gameObject.SetActive(true);
            mTransform[value3].gameObject.SetActive(true);
            mTransform[value4].gameObject.SetActive(true);
        }   
    }       
}           
