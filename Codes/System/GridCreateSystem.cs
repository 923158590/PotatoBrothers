using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IGridCreateSystem : ISystem { }
    public class GridCreateSystem : AbstractSystem, IGridCreateSystem
    {
        private Sprite mGroundSprite;
        private Sprite mNullSprite;
        private List<SpriteRenderer> mRenderList;
        private int mGroundIndex;

        private Transform mRootTrans;

        protected override void OnInit()
        {
            mGroundSprite = Resources.Load<Sprite>("Sprite/Ground/TX Tileset Grass");
            mNullSprite = Resources.Load<Sprite>("Sprite/Weapon/Null");
            mRenderList = new List<SpriteRenderer>();

            mRootTrans = new GameObject("MapRoot").transform;

            this.RegisterEvent<CreateNodeEvent>(OnCreateGrid);
            this.RegisterEvent<GameInitEndEvent>(OnInitGridSystem);
        }

        private void OnInitGridSystem(GameInitEndEvent e)
        {
            mRootTrans = new GameObject("MapRoot").transform;
        }

        //private void OnCreateFood(CreateFoodEvent e) => mFoodTrans.localPosition = new Vector2(e.pos.x, e.pos.y);

        private void OnCreateGrid(CreateNodeEvent e)
        {
            if (e.type == Node.E_Type.Ground)
            {
                mRenderList.Add(new GameObject("Ground").AddComponent<SpriteRenderer>());
            }
            else
            {
                mRenderList.Add(new GameObject("Wall").AddComponent<SpriteRenderer>());
            }
            //if (mGroundIndex == mRenderList.Count) mRenderList.Add(new GameObject("Ground").AddComponent<SpriteRenderer>());
            mRenderList[mGroundIndex].sprite = e.type == Node.E_Type.Wall ? mGroundSprite : mGroundSprite;
            if (e.type == Node.E_Type.Wall) mRenderList[mGroundIndex].gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            if (e.type == Node.E_Type.Wall) mRenderList[mGroundIndex].gameObject.tag = "Wall";

            mRenderList[mGroundIndex].transform.localPosition = e.pos;
            mRenderList[mGroundIndex].sortingOrder = -1;
            mRenderList[mGroundIndex].transform.SetParent(mRootTrans);
            mGroundIndex++;
        }
    }
}

