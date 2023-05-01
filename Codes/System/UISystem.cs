using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IUISystem : ISystem { }
    /// <summary>
    /// UIϵͳ
    /// </summary>
    public class UISystem : AbstractSystem, IUISystem
    {
        private GameObject mShopUIPrefab;
        private GameObject mCanvasPrefab;
        private GameObject mSkillLevelUpPrefab;
        private GameObject ShopUI;
        private GameObject SkillLevelUpUI;
        private GameObject CanvasUI;
        protected override void OnInit()
        {
            this.RegisterEvent<NextRoundStartEvent>(OnCloseShopView);
            this.RegisterEvent<InitShopEvent>(OnInitShopView);
            this.RegisterEvent<PlayerWinThisRoundEvent>(OnInitSkillLevelUpView);
            this.RegisterEvent<GameInitEndEvent>(OnInitUISystem);
        }

        // ��ʼ��UISystem
        private void OnInitUISystem(GameInitEndEvent obj)
        {
            mShopUIPrefab = Resources.Load<GameObject>("Prefab/UI/UI_Shop");
            mCanvasPrefab = Resources.Load<GameObject>("Prefab/UI/Canvas");
            mSkillLevelUpPrefab = Resources.Load<GameObject>("Prefab/UI/UI_SkillLevelUp");

            ShopUI = GameObject.Instantiate(mShopUIPrefab);
            CanvasUI = GameObject.Instantiate(mCanvasPrefab);
            SkillLevelUpUI = GameObject.Instantiate(mSkillLevelUpPrefab);

            ShopUI.SetActive(false);
            SkillLevelUpUI.SetActive(false);
            //UnityEngine.Object.DontDestroyOnLoad(ShopUI);
            //UnityEngine.Object.DontDestroyOnLoad(SkillLevelUpUI);
        }

        // ��ʼ��������ͼ
        private void OnInitSkillLevelUpView(PlayerWinThisRoundEvent e)
        {
            SkillLevelUpUI.SetActive(true);
            this.SendEvent<InitSkillLevelUpViewEvent>();
        }

        // ��ʼ���̵���ͼ
        private void OnInitShopView(InitShopEvent e)
        {
            if (ShopUI == null)
                ShopUI = GameObject.Instantiate(mShopUIPrefab);
            else
                ShopUI.gameObject.SetActive(true);

            this.SendEvent<InitShopSuccessfulEvent>();
            this.SendEvent<UpdateShoppingViewEvent>();
        }

        // �ر��̵���ͼ
        private void OnCloseShopView(NextRoundStartEvent e)
        {
            if (ShopUI != null)
                ShopUI.gameObject.SetActive(false);
            else
                return;
        }
    }
}

