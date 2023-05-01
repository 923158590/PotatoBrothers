using QFramework;
using UnityEngine;

namespace Game
{
    public class BulletGo : Game2DController, IPoolable, IPoolType
    {
        private GameObject mBulletPrefab;
        private GameObject mBulletGo;
        private bool mIsRecycled;

        public bool IsRecycled 
        {
            get { return mIsRecycled; }
            set { mIsRecycled = value; }
        }

        public GameObject Bullet
        {
            get { return mBulletGo; }
            set { mBulletGo = value; }  
        }

        private void Awake()
        {
            mBulletPrefab = Resources.Load<GameObject>("Prefab/Bullet");
            mBulletGo = GameObject.Instantiate(mBulletPrefab);
        }

        public static BulletGo Allocate()
        {
            return SafeObjectPool<BulletGo>.Instance.Allocate();
        }

        public void Recycle2Cache()
        {
            SafeObjectPool<BulletGo>.Instance.Recycle(this);
        }

        public void OnRecycled()
        {

        }
    }
}