using QFramework;
using UnityEngine;

namespace Game
{
    public class PistolBulletGo : Game2DController, IPoolable, IPoolType
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
            mBulletPrefab = Resources.Load<GameObject>("Prefab/PistolBullet");
            mBulletGo = GameObject.Instantiate(mBulletPrefab);
        }

        public static PistolBulletGo Allocate()
        {
            return SafeObjectPool<PistolBulletGo>.Instance.Allocate();
        }

        public void Recycle2Cache()
        {
            SafeObjectPool<PistolBulletGo>.Instance.Recycle(this);
        }

        public void OnRecycled()
        {

        }
    }
}