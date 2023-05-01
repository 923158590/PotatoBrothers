using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public abstract class IObjectPool : IController
    {

        public GameObject prefab;
        public int poolSize = 10;

        private List<GameObject> pool;

        protected void Awake()
        {
            pool = new List<GameObject>();
            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab) as GameObject;
                obj.SetActive(false);
                pool.Add(obj);
            }
        }

        public GameObject GetObject()
        {
            foreach (GameObject obj in pool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
            GameObject newObj = GameObject.Instantiate(prefab) as GameObject;
            pool.Add(newObj);
            return newObj;
        }

        public void ReleaseObject(GameObject obj)
        {
            obj.SetActive(false);
        }

        public IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }
    }
}