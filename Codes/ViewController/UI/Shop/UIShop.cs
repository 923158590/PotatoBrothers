using UnityEngine;
using QFramework;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace Game
{
    public partial class UIShop : ViewController, IController
    {
        private Sprite mPistolSprite;
        private Sprite mSubmachineGunSprite;
        private Sprite mRifleSprite;
        private Sprite mNullSprite;
        private int mCurSelectGunIndex;

        private List<Image> mSprites;
        private List<Text> mAttackTexts;
        private List<Text> mAttackSpeedTexts;
        private List<Text> mNameTexts;
        private List<Text> mShopButtonTexts;
        private List<RectTransform> mItem;
        /// <summary>
        /// �̵걳����ɫ
        /// </summary>
        private List<Image> mItemBackGround;
        private List<Image> mGunSprites;
        /// <summary>
        /// ǹе������ɫ
        /// </summary>
        private List<Image> mGunBackGround;
        private List<Button> mLockButton;

        public IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }

        void Start()
        {
            mPistolSprite = Resources.Load<Sprite>("Sprite/Weapon/Pistol");
            mSubmachineGunSprite = Resources.Load<Sprite>("Sprite/Weapon/SubmachineGun");
            mRifleSprite = Resources.Load<Sprite>("Sprite/Weapon/Rifle");
            mNullSprite = Resources.Load<Sprite>("Sprite/Weapon/Null");

            this.RegisterEvent<UpdateShoppingViewEvent>(OnUpdateShoppingView);
            this.RegisterEvent<InitShopSuccessfulEvent>(OnInitShopView);
            this.RegisterEvent<LogChanegEvent>(OnInitLogView);
            
            Init();

            // ˢ�°�ť���
            RefreshButton.onClick.AddListener(() =>
            {
                for(int i = 0; i < mItem.Count; i++)
                {
                    mItem[i].gameObject.SetActive(true);
                }
                // �����̵�ˢ������
                this.SendCommand<ShopRefreshCommand>();
            });

            // ��ʼ��Ϸ��ť���
            StartNextRoundButton.onClick.AddListener(() =>
            {
                this.SendCommand<StartNextRoundCommand>();
            });

            // ���1����ť
            Item1_ShopButton.onClick.AddListener(() =>
            {
                if (this.GetSystem<IGunSystem>().IsGunsFull())
                {
                    this.GetSystem<ILogSystem>().SetLog("�������е�ǹе�ѵ�������!");
                    return;
                }
                ShoppingCommand shoppingCommand = new ShoppingCommand(0);
                this.SendCommand(shoppingCommand);
            });

            // ���2����ť
            Item2_ShopButton.onClick.AddListener(() =>
            {
                if (this.GetSystem<IGunSystem>().IsGunsFull())
                {
                    this.GetSystem<ILogSystem>().SetLog("�������е�ǹе�ѵ�������!");
                    return;
                }
                ShoppingCommand shoppingCommand = new ShoppingCommand(1);
                this.SendCommand(shoppingCommand);
            });

            // ���3����ť
            Item3_ShopButton.onClick.AddListener(() =>
            {
                if (this.GetSystem<IGunSystem>().IsGunsFull())
                {
                    this.GetSystem<ILogSystem>().SetLog("�������е�ǹе�ѵ�������!");
                    return;
                }
                ShoppingCommand shoppingCommand = new ShoppingCommand(2);
                this.SendCommand(shoppingCommand);
            });

            // ���4����ť
            Item4_ShopButton.onClick.AddListener(() =>
            {
                if (this.GetSystem<IGunSystem>().IsGunsFull())
                {
                    this.GetSystem<ILogSystem>().SetLog("�������е�ǹе�ѵ�������!");
                    return;
                }
                ShoppingCommand shoppingCommand = new ShoppingCommand(3);
                this.SendCommand(shoppingCommand);
            });

            // ��Ʒ1��ס ��ֹˢ��
            Item1_LockButton.onClick.AddListener(() =>
            {
                // UI��ɫ�任
                ButtonColorChange(Item1_LockButton, Color.black, Color.white);

                // ������ס�̵���¼�
                LockShopItemCommand lockShopItemCommand = new LockShopItemCommand(0);
                this.SendCommand(lockShopItemCommand);
            });

            // ��Ʒ2��ס ��ֹˢ��
            Item2_LockButton.onClick.AddListener(() =>
            {
                // UI��ɫ�任
                ButtonColorChange(Item2_LockButton, Color.black, Color.white);
                // ������ס�̵���¼�
                LockShopItemCommand lockShopItemCommand = new LockShopItemCommand(1);
                this.SendCommand(lockShopItemCommand);
            });

            // ��Ʒ3��ס ��ֹˢ��
            Item3_LockButton.onClick.AddListener(() =>
            {
                // UI��ɫ�任
                ButtonColorChange(Item3_LockButton, Color.black, Color.white);
                // ������ס�̵���¼�
                LockShopItemCommand lockShopItemCommand = new LockShopItemCommand(2);
                this.SendCommand(lockShopItemCommand);
            });

            // ��Ʒ4��ס ��ֹˢ��
            Item4_LockButton.onClick.AddListener(() =>
            {
                // UI��ɫ�任
                ButtonColorChange(Item4_LockButton, Color.black, Color.white);
                // ������ס�̵���¼�
                LockShopItemCommand lockShopItemCommand = new LockShopItemCommand(3);
                this.SendCommand(lockShopItemCommand);
            });

            Weapon1_Button.onClick.AddListener(() =>
            {
                mCurSelectGunIndex = 0;

                SelectGunInformation.localPosition = new Vector2(28, -45);

                if (this.GetSystem<IGunSystem>().GunBag[mCurSelectGunIndex].Gun == null) return;
                var gun = this.GetSystem<IGunSystem>().GunBag[0].Gun;
                if (gun.ChineseName == "Nullǹ") return;
                UpdateSelectedGunInformation(gun);

                SelectGunInformationBackGround.gameObject.SetActive(true);
                SelectGunInformation.gameObject.SetActive(true);
            });

            Weapon2_Button.onClick.AddListener(() =>
            {
                mCurSelectGunIndex = 1;

                SelectGunInformation.localPosition = new Vector2(75, -45);

                if (this.GetSystem<IGunSystem>().GunBag[mCurSelectGunIndex].Gun == null) return;
                var gun = this.GetSystem<IGunSystem>().GunBag[1].Gun;
                if (gun.ChineseName == "Nullǹ") return;
                UpdateSelectedGunInformation(gun);

                SelectGunInformationBackGround.gameObject.SetActive(true);
                SelectGunInformation.gameObject.SetActive(true);
            });

            Weapon3_Button.onClick.AddListener(() =>
            {
                mCurSelectGunIndex = 2;

                SelectGunInformation.transform.localPosition = new Vector2(125, -45);

                if (this.GetSystem<IGunSystem>().GunBag[mCurSelectGunIndex].Gun == null) return;
                var gun = this.GetSystem<IGunSystem>().GunBag[2].Gun;
                if (gun.ChineseName == "Nullǹ") return;
                UpdateSelectedGunInformation(gun);

                SelectGunInformationBackGround.gameObject.SetActive(true);
                SelectGunInformation.gameObject.SetActive(true);
            });

            Weapon4_Button.onClick.AddListener(() =>
            {
                mCurSelectGunIndex = 3;

                SelectGunInformation.transform.localPosition = new Vector2(28, -93);

                if (this.GetSystem<IGunSystem>().GunBag[mCurSelectGunIndex].Gun == null) return;
                var gun = this.GetSystem<IGunSystem>().GunBag[3].Gun;
                if (gun.ChineseName == "Nullǹ") return;
                UpdateSelectedGunInformation(gun);

                SelectGunInformationBackGround.gameObject.SetActive(true);
                SelectGunInformation.gameObject.SetActive(true);
            });

            Weapon5_Button.onClick.AddListener(() =>
            {
                mCurSelectGunIndex = 4;

                SelectGunInformation.transform.localPosition = new Vector2(75, -93);

                if (this.GetSystem<IGunSystem>().GunBag[mCurSelectGunIndex].Gun == null) return;
                var gun = this.GetSystem<IGunSystem>().GunBag[4].Gun;
                if (gun.ChineseName == "Nullǹ") return;
                UpdateSelectedGunInformation(gun);

                SelectGunInformationBackGround.gameObject.SetActive(true);
                SelectGunInformation.gameObject.SetActive(true);
            });

            Weapon6_Button.onClick.AddListener(() =>
            {
                mCurSelectGunIndex = 5;

                SelectGunInformation.transform.localPosition = new Vector2(125, -93);

                if (this.GetSystem<IGunSystem>().GunBag[mCurSelectGunIndex].Gun == null) return;
                var gun = this.GetSystem<IGunSystem>().GunBag[5].Gun;
                if (gun.ChineseName == "Nullǹ") return;
                UpdateSelectedGunInformation(gun);

                SelectGunInformationBackGround.gameObject.SetActive(true);
                SelectGunInformation.gameObject.SetActive(true);
            });

            SelectGunInformation_Cancel.onClick.AddListener(() =>
            {
                SelectGunInformationBackGround.gameObject.SetActive(false);
                SelectGunInformation.gameObject.SetActive(false);
            });

            SelectGunInformation_Craft.onClick.AddListener(() =>
            {
                GunCraftCommand gunCraftCommand = new GunCraftCommand(mCurSelectGunIndex);
                SelectGunInformationBackGround.gameObject.SetActive(false);
                SelectGunInformation.gameObject.SetActive(false);
                this.SendCommand(gunCraftCommand);
            });

            SelectGunInformation_Sale.onClick.AddListener(() => 
            {
                GunSaleCommand gunSaleCommand = new GunSaleCommand(this.GetSystem<IGunSystem>().GunBag[mCurSelectGunIndex].Gun);
                this.SendCommand(gunSaleCommand);
                //UpdateShopItemBackGround(mCurSelectGunIndex, GunRank.Normal, mGunBackGround);
                mGunSprites[mCurSelectGunIndex].sprite = mNullSprite;
                SelectGunInformationBackGround.gameObject.SetActive(false);
                SelectGunInformation.gameObject.SetActive(false);
            });
        }

        private void OnInitLogView(LogChanegEvent e)
        {
            ShopInformationText.text = this.GetSystem<ILogSystem>().Log;
        }

        // ��ʼ��
        public void Init()
        {
            mSprites = new List<Image>();
            mAttackTexts = new List<Text>();
            mAttackSpeedTexts = new List<Text>();
            mNameTexts = new List<Text>();
            mShopButtonTexts = new List<Text>();
            mItem = new List<RectTransform>();
            mItemBackGround = new List<Image>();
            mGunSprites = new List<Image>();
            mGunBackGround = new List<Image>();
            mLockButton = new List<Button>();

            mSprites.Add(Item1_Sprite);
            mSprites.Add(Item2_Sprite);
            mSprites.Add(Item3_Sprite);
            mSprites.Add(Item4_Sprite);

            mAttackTexts.Add(Item1_AttackText);
            mAttackTexts.Add(Item2_AttackText);
            mAttackTexts.Add(Item3_AttackText);
            mAttackTexts.Add(Item4_AttackText);

            mAttackSpeedTexts.Add(Item1_AttackSpeedText);
            mAttackSpeedTexts.Add(Item2_AttackSpeedText);
            mAttackSpeedTexts.Add(Item3_AttackSpeedText);
            mAttackSpeedTexts.Add(Item4_AttackSpeedText);

            mNameTexts.Add(Item1_NameText);
            mNameTexts.Add(Item2_NameText);
            mNameTexts.Add(Item3_NameText);
            mNameTexts.Add(Item4_NameText);

            mShopButtonTexts.Add(Item1_ShopButtonText);
            mShopButtonTexts.Add(Item2_ShopButtonText);
            mShopButtonTexts.Add(Item3_ShopButtonText);
            mShopButtonTexts.Add(Item4_ShopButtonText);

            mItem.Add(Item1);
            mItem.Add(Item2);
            mItem.Add(Item3);
            mItem.Add(Item4);

            mItemBackGround.Add(Item1_BackGround);
            mItemBackGround.Add(Item2_BackGround);
            mItemBackGround.Add(Item3_BackGround);
            mItemBackGround.Add(Item4_BackGround);

            mGunSprites.Add(Weapon1_Sprite);
            mGunSprites.Add(Weapon2_Sprite);
            mGunSprites.Add(Weapon3_Sprite);
            mGunSprites.Add(Weapon4_Sprite);
            mGunSprites.Add(Weapon5_Sprite);
            mGunSprites.Add(Weapon6_Sprite);

            mGunBackGround.Add(Weapon1);
            mGunBackGround.Add(Weapon2);
            mGunBackGround.Add(Weapon3);
            mGunBackGround.Add(Weapon4);
            mGunBackGround.Add(Weapon5);
            mGunBackGround.Add(Weapon6);

            mLockButton.Add(Item1_LockButton);
            mLockButton.Add(Item2_LockButton);
            mLockButton.Add(Item3_LockButton);
            mLockButton.Add(Item4_LockButton);

            UpdateShoppingView();
        }


        // ��ʼ���̵����ͼ
        private void OnInitShopView(InitShopSuccessfulEvent e)
        {
            // ��ʼ�����н����
            var playerModel = this.GetModel<IPlayerModel>();
            CoinText.text = "Coin:" + playerModel.coin;

            // ��ʼ�����״̬��ͼ
            var plyerModel = this.GetModel<IPlayerModel>();
            PlayerStatus_HPText.text = "HP:" + plyerModel.maxHP;
            PlayerStatus_AttackText.text = "������:" + plyerModel.attack;
            PlayerStatus_AttackSpeedText.text = "�����ٶ�:" + plyerModel.attackSpeed;
            PlayerStatus_SpeedText.text = "�ƶ��ٶ�:" + plyerModel.speed;
            PlayerStatus_RecoverText.text = "�ָ��ٶ�:" + plyerModel.recover;
            PlayerStatus_LuckyText.text = "���˶�:" + plyerModel.lucky;
            PlayerStatus_LifeStealText.text = "��Ѫ:" + plyerModel.lifeSteal;

            // ��ʼ���ؿ�״̬��ͼ
            var gameSystem = this.GetSystem<IGameSystem>();
            RoundText.text = "�̵�(��" + gameSystem.Round + "��)";

            // ��ʼ��ˢ�½����ͼ
            var shopSystem = this.GetSystem<IShopSystem>();
            RefreshButtonText.text = "ˢ�� -" + shopSystem.RefreshCoin + "Coin";

            // ��ʼ������ǹе��ͼ
            var gunSystem = this.GetSystem<IGunSystem>();

            Gun_Title.text = "����(" + gunSystem.getGunNumsInBag() + "/6)";

            for(int i = 0; i < gunSystem.MaxGunNums; i++)
            {
                mGunSprites[i].gameObject.SetActive(true);

                if (gunSystem.GunBag[i].Gun == null)
                {
                    mGunSprites[i].sprite = mNullSprite;
                    continue;
                }

                var gun = gunSystem.GunBag[i].Gun;
                if (gun.Name == "Pistol")
                {
                    mGunSprites[i].sprite = mPistolSprite;
                }
                else if (gun.Name == "SubmachineGun")
                {
                    mGunSprites[i].sprite = mSubmachineGunSprite;
                }
                else if (gun.Name == "Rifle")
                {
                    mGunSprites[i].sprite = mRifleSprite;
                }
                UpdateShopItemBackGround(i, gun.Rank, mGunBackGround);
            }


            // ��ʼ���̵���Ʒ
            for (int i = 0; i < 4; i++)
            {
                mItem[i].gameObject.SetActive(true);
            }
        }

        // �����̵���ͼ
        private void OnUpdateShoppingView(UpdateShoppingViewEvent e)
        {
            UpdateShoppingView();
        }

        private void UpdateShoppingView()
        {
            // ��ʼ�����н����
            var playerModel = this.GetModel<IPlayerModel>();
            CoinText.text = "Coin:" + playerModel.coin;

            // ��ʼ�����״̬��ͼ
            var plyerModel = this.GetModel<IPlayerModel>();
            PlayerStatus_HPText.text = "HP:" + plyerModel.maxHP;
            PlayerStatus_AttackText.text = "������:" + plyerModel.attack;
            PlayerStatus_AttackSpeedText.text = "�����ٶ�:" + plyerModel.attackSpeed;
            PlayerStatus_SpeedText.text = "�ƶ��ٶ�:" + plyerModel.speed;
            PlayerStatus_RecoverText.text = "�ָ��ٶ�:" + plyerModel.recover;
            PlayerStatus_LuckyText.text = "���˶�:" + plyerModel.lucky;
            PlayerStatus_LifeStealText.text = "��Ѫ:" + plyerModel.lifeSteal;

            // ��ʼ���ؿ�״̬��ͼ
            var gameSystem = this.GetSystem<IGameSystem>();
            RoundText.text = "�̵�(��" + gameSystem.Round + "��)";

            // ��ʼ��ˢ�½����ͼ
            var shopSystem = this.GetSystem<IShopSystem>();
            RefreshButtonText.text = "ˢ�� -" + shopSystem.RefreshCoin + "Coin";

            // ��ʼ������ǹе��ͼ
            var gunSystem = this.GetSystem<IGunSystem>();
            var guns = gunSystem.GunBag;

            Gun_Title.text = "����(" + gunSystem.getGunNumsInBag() + "/6)";

            for (int i = 0; i < gunSystem.MaxGunNums; i++)
            {
                mGunSprites[i].gameObject.SetActive(true);

                if (gunSystem.GunBag[i].Gun == null)
                {
                    mGunSprites[i].sprite = mNullSprite;
                    UpdateShopItemBackGround(i, GunRank.Normal, mGunBackGround);
                    continue;
                }

                var gun = guns[i].Gun;

                if (gun.Name == "Pistol")
                {
                    mGunSprites[i].sprite = mPistolSprite;
                }
                else if (gun.Name == "SubmachineGun")
                {
                    mGunSprites[i].sprite = mSubmachineGunSprite;
                }
                else if (gun.Name == "Rifle")
                {
                    mGunSprites[i].sprite = mRifleSprite;
                }
                // ���³���ǹе������ɫ
                UpdateShopItemBackGround(i, gun.Rank, mGunBackGround);
            }

            // ˢ���̵���Ʒ��ͼ
            List<IGunModel> Guns = shopSystem.Shop;

            for (int i = 0; i < Guns.Count; i++)
            {
                var gun = Guns[i];
                if (Guns[i].Name == "Pistol")
                {
                    mSprites[i].sprite = mPistolSprite;
                    mAttackTexts[i].text = "������:" + gun.Attack;
                    mAttackSpeedTexts[i].text = "�����ٶ�:" + gun.AttackSpeed;
                    mNameTexts[i].text = "��ǹ";
                    mShopButtonTexts[i].text = gun.Coin + " Coin";
                }
                else if (Guns[i].Name == "SubmachineGun")
                {
                    mSprites[i].sprite = mSubmachineGunSprite;
                    mAttackTexts[i].text = "������:" + gun.Attack;
                    mAttackSpeedTexts[i].text = "�����ٶ�:" + gun.AttackSpeed;
                    mNameTexts[i].text = "���ǹ";
                    mShopButtonTexts[i].text = gun.Coin + " Coin";
                }
                else if (Guns[i].Name == "Rifle")
                {
                    mSprites[i].sprite = mRifleSprite;
                    mAttackTexts[i].text = "������:" + gun.Attack;
                    mAttackSpeedTexts[i].text = "�����ٶ�:" + gun.AttackSpeed;
                    mNameTexts[i].text = "��ǹ";
                    mShopButtonTexts[i].text = gun.Coin + " Coin";
                }
                else
                {
                    mItem[i].gameObject.SetActive(false);
                }

                // ���°�ť��ɫ
                if (shopSystem.Lock[i] == false)
                {
                    SetButtonColorBlack(mLockButton[i]);
                }
                // ������Ʒ������ɫ
                UpdateShopItemBackGround(i, gun.Rank, mItemBackGround);

            }
        }

        // ����ǹ�ı�����ɫ ��ͬRank��ǹ�в�ͬ��ɫ
        private void UpdateShopItemBackGround(int index, GunRank rank, List<Image> Images)
        {
            if(rank == GunRank.Normal)
            {
                Images[index].color = new Color(1, 1, 1, 0.05f);
            }
            else if (rank == GunRank.Rare)
            {
                Images[index].color = new Color(0, 0, 1, 0.3f);
            }
            else if (rank == GunRank.Epic)
            {
                Images[index].color = new Color(0.5f, 0, 0.5f, 0.4f);
            }
            else if (rank == GunRank.Legendary)
            {
                Images[index].color = new Color(1, 0.5f, 0, 0.35f);
            }
            else
            {
                Images[index].color = new Color(1, 1, 1, 0.05f);
            }
        }

        // ʵ��������ɫ���л�
        public void ButtonColorChange(Button button, Color from, Color to)
        {
            if (button.image.color == from)
                button.image.color = to;
            else
                button.image.color = from;

            if (button.transform.GetChild(0).gameObject.GetComponent<Text>().color == from)
                button.transform.GetChild(0).gameObject.GetComponent<Text>().color = to;
            else
                button.transform.GetChild(0).gameObject.GetComponent<Text>().color = from;
        }

        // �Ѱ�ť��ɫ����Ϊ��ɫ
        private void SetButtonColorBlack(Button button)
        {
            button.image.color = Color.black;
            button.transform.GetChild(0).gameObject.GetComponent<Text>().color = Color.white;
        }

        // ������ѡ��ǹ����Ϣ
        private void UpdateSelectedGunInformation(IGunModel gun)
        {
            // ��������
            SelectGunInformation_NameText.text = gun.ChineseName;

            // ����Ʒ����ɫ
            var rank = gun.Rank;
            if (rank == GunRank.Normal)
            {
                SelectGunInformation_SpriteBackGround.color = new Color(1, 1, 1, 0.05f);
            }
            else if (rank == GunRank.Rare)
            {
                SelectGunInformation_SpriteBackGround.color = new Color(0, 0, 1, 0.3f);
            }
            else if (rank == GunRank.Epic)
            {
                SelectGunInformation_SpriteBackGround.color = new Color(0.5f, 0, 0.5f, 0.4f);
            }
            else if (rank == GunRank.Legendary)
            {
                SelectGunInformation_SpriteBackGround.color = new Color(1, 0.5f, 0, 0.35f);
            }

            // ����ͼ��
            if (gun.ChineseName == "��ǹ")
            {
                SelectGunInformation_Sprite.sprite = mPistolSprite;
            }
            else if (gun.ChineseName == "���ǹ")
            {
                SelectGunInformation_Sprite.sprite = mSubmachineGunSprite;
            }
            else if (gun.ChineseName == "��ǹ")
            {
                SelectGunInformation_Sprite.sprite = mRifleSprite;
            }

            // ���¹�����
            SelectGunInformation_AttackText.text = "������" + gun.Attack;
            // ���¹����ٶ�
            SelectGunInformation_AttackSpeedText.text = "�����ٶ�" + gun.AttackSpeed;
        }
    }
}
