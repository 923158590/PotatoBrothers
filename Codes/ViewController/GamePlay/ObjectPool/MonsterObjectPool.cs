using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MonsterObjectPool : MonoBehaviour, IController
    {
        public GameObject prefab;
        public int poolSize = 10;
        public GameObject bulletPool;

        private List<GameObject> pool;

        private static MonsterObjectPool instance;

        public static MonsterObjectPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<MonsterObjectPool>();
                }
                return instance;
            }
        }

        protected void Awake()
        {
            bulletPool = new GameObject("MonsterPool");
            bulletPool.transform.SetParent(transform, false);
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }

            prefab = Resources.Load<GameObject>("Prefab/Enemy");
            pool = new List<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab) as GameObject;
                obj.transform.SetParent(bulletPool.transform);
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
                    StartCoroutine(Release(obj));
                    obj.transform.SetParent(bulletPool.transform);
                    return obj;
                }
            }
            GameObject newObj = GameObject.Instantiate(prefab) as GameObject;
            newObj.transform.SetParent(bulletPool.transform);
            StartCoroutine(Release(newObj));
            pool.Add(newObj);
            return newObj;
        }

        public void ReleaseObject(GameObject obj)
        {
            obj.SetActive(false);
            Debug.Log("对象池还有" + pool.Count +"个子弹");
        }

        public IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }

        public IEnumerator Release(GameObject obj)
        {
            yield return new WaitForSeconds(3);
            obj.SetActive(false);
            Debug.Log("对象池还有" + pool.Count +"个子弹");
        }
    }

}