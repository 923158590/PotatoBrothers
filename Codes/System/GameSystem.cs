using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public interface IGameSystem : ISystem
    {
        /// <summary>
        /// (����)���صĵ���ʱ
        /// </summary>
        public float Time { get; }
        /// <summary>
        /// // (����)�ؿ��غ�
        /// </summary>
        public int Round { get; }
    }

    public class GameSystem : AbstractSystem, IGameSystem
    {
        // �ؿ��غ�
        private int mRound = 1;
        // ��ʼʱ�������˵ĸ���
        private int mInitCreateEnemyCount = 1;
        // ���صĵ���ʱ
        private float mTime = 20;
        // ���ɵ��˵�ʱ����
        private int mCreateEnemyInterval = 4;
        // ���ӵ���������ʱ����
        private int mAddEnemyCountInThisRoundInterval = 9;
        // ���˶���س�ʼ������
        public int poolSize = 10;
        // ���˶����
        private List<GameObject> pool;
        // ����ص�GameObject
        public GameObject EnemyPool;
        // ����Ԥ����
        public GameObject enemyPrefab;

        public float Time { get { return mTime; } }
        public int Round { get { return mRound; } }

        // ������������
        private DelayTask mCreateEnemyTask;
        // ���������غϻ�ʤ����
        private DelayTask mPlayerWinThisRoundTask;
        // �����ڱ��غ�����ʱ���ӵ�������
        private DelayTask mAddEnemyCountInThisRoundTask;
        // ���ֹ��صĵ���ʱ
        private DelayTask mTimeCountDown;
        protected override void OnInit()
        {
            this.RegisterEvent<GameInitEndEvent>(OnGameInitEnd);
            this.RegisterEvent<GameOverEvent>(OnGameOver);
            this.RegisterEvent<NextRoundStartEvent>(OnAddEnemyCount);
            this.RegisterEvent<NextRoundStartEvent>(OnNextRoundStartEvent);
        }

        private void OnGameInitEnd(GameInitEndEvent e)
        {

            // ��ʼ�������
            pool = new List<GameObject>();
            // Ԥ�������
            enemyPrefab = Resources.Load<GameObject>("Prefab/Enemy");
            // ��������
            EnemyPool = new GameObject("EnemyPool");
            //var parent = GameObject.Find("CommonMono");
            //EnemyPool.transform.SetParent(parent.transform);
            // ������ʱ�¼�
            mPlayerWinThisRoundTask = this.GetSystem<ITimeSystem>().AddDelayTask(mTime, PlayerWinThisRoundTask, true);
            mAddEnemyCountInThisRoundTask = this.GetSystem<ITimeSystem>().AddDelayTask(mAddEnemyCountInThisRoundInterval, AddEnemyCountInThisRoundTask, true);
            mTimeCountDown = this.GetSystem<ITimeSystem>().AddDelayTask(1, CountDown, true);
            mCreateEnemyTask = this.GetSystem<ITimeSystem>().AddDelayTask(mCreateEnemyInterval, CreateEnemyInMap, true);
        }
        private void OnGameOver(GameOverEvent e)
        {
            mCreateEnemyTask.StopTask();
            mPlayerWinThisRoundTask.StopTask();
            mAddEnemyCountInThisRoundTask.StopTask();
            mTimeCountDown.StopTask();
            SceneManager.LoadScene("GameOver");

        }

        /// <summary>
        /// ������ɵ����ڵ�ͼ��
        /// </summary>
        private void CreateEnemyInMap()
        {
            for(int i=0;i<mInitCreateEnemyCount;i++)
            {
                var p = this.GetSystem<IGridNodeSystem>().FindBlockPos(20, 20);
                var go = GetEnemyInObjectPool();
                go.transform.localPosition = new Vector3(p.x, p.y, 0);
                go.GetComponent<Enemy>().Revive();
            }
        }
        /// <summary>
        /// ���غ�����ʱ���ӵ���
        /// </summary>
        private void AddEnemyCountInThisRoundTask()
        {
            mInitCreateEnemyCount+= 2;
        }
        /// <summary>
        /// �������ӵ���
        /// </summary>
        private void OnAddEnemyCount(NextRoundStartEvent e)
        {
            mRound += 1;
            mInitCreateEnemyCount = mRound;
        }
        /// <summary>
        /// ��ұ��غϻ�ʤ
        /// </summary>
        private void PlayerWinThisRoundTask()
        {
            Debug.Log("�����غϻ�ʤ");
            mTimeCountDown.StopTask();
            mPlayerWinThisRoundTask.StopTask();
            mAddEnemyCountInThisRoundTask.StopTask();
            mCreateEnemyTask.StopTask();
            mTime = 15;
            mInitCreateEnemyCount = 0;
            this.SendEvent<PlayerWinThisRoundEvent>();
        }
        /// <summary>
        /// ���ֵĵ���ʱ
        /// </summary>
        private void CountDown()
        {
            mTime--;
            TimeChangeInThisRoundEvent e = new TimeChangeInThisRoundEvent();
            e.time = mTime;
            this.SendEvent(e);
        }
        /// <summary>
        /// ��ʼ��һ��
        /// </summary>
        /// <param name="e"></param>
        private void OnNextRoundStartEvent(NextRoundStartEvent e)
        {
            this.SendEvent<CreateGunEvent>();

            mCreateEnemyTask = this.GetSystem<ITimeSystem>().AddDelayTask(mCreateEnemyInterval, CreateEnemyInMap, true);
            mPlayerWinThisRoundTask = this.GetSystem<ITimeSystem>().AddDelayTask(mTime, PlayerWinThisRoundTask, true);
            mAddEnemyCountInThisRoundTask = this.GetSystem<ITimeSystem>().AddDelayTask(mAddEnemyCountInThisRoundInterval, AddEnemyCountInThisRoundTask, true);
            mTimeCountDown = this.GetSystem<ITimeSystem>().AddDelayTask(1, CountDown, true);
        }
        /// <summary>
        /// �Ӷ�����л�ȡ����
        /// </summary>
        /// <returns></returns>
        public GameObject GetEnemyInObjectPool()
        {
            foreach (GameObject obj in pool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    obj.transform.SetParent(EnemyPool.transform);
                    return obj;
                }
            }
            GameObject newObj = GameObject.Instantiate(enemyPrefab) as GameObject;
            newObj.transform.SetParent(EnemyPool.transform);
            pool.Add(newObj);
            return newObj;
        }
    }
}
