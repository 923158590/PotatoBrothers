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
        /// (属性)过关的倒计时
        /// </summary>
        public float Time { get; }
        /// <summary>
        /// // (属性)关卡回合
        /// </summary>
        public int Round { get; }
    }

    public class GameSystem : AbstractSystem, IGameSystem
    {
        // 关卡回合
        private int mRound = 1;
        // 开始时创建敌人的个数
        private int mInitCreateEnemyCount = 1;
        // 过关的倒计时
        private float mTime = 20;
        // 生成敌人的时间间隔
        private int mCreateEnemyInterval = 4;
        // 增加敌人数量的时间间隔
        private int mAddEnemyCountInThisRoundInterval = 9;
        // 敌人对象池初始化个数
        public int poolSize = 10;
        // 敌人对象池
        private List<GameObject> pool;
        // 对象池的GameObject
        public GameObject EnemyPool;
        // 敌人预制体
        public GameObject enemyPrefab;

        public float Time { get { return mTime; } }
        public int Round { get { return mRound; } }

        // 创建敌人任务
        private DelayTask mCreateEnemyTask;
        // 创建玩家这回合获胜任务
        private DelayTask mPlayerWinThisRoundTask;
        // 创建在本回合内暂时增加敌人任务
        private DelayTask mAddEnemyCountInThisRoundTask;
        // 本局过关的倒计时
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

            // 初始化对象池
            pool = new List<GameObject>();
            // 预制体加载
            enemyPrefab = Resources.Load<GameObject>("Prefab/Enemy");
            // 搜索物体
            EnemyPool = new GameObject("EnemyPool");
            //var parent = GameObject.Find("CommonMono");
            //EnemyPool.transform.SetParent(parent.transform);
            // 设置延时事件
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
        /// 随机生成敌人在地图上
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
        /// 本回合内暂时增加敌人
        /// </summary>
        private void AddEnemyCountInThisRoundTask()
        {
            mInitCreateEnemyCount+= 2;
        }
        /// <summary>
        /// 永久增加敌人
        /// </summary>
        private void OnAddEnemyCount(NextRoundStartEvent e)
        {
            mRound += 1;
            mInitCreateEnemyCount = mRound;
        }
        /// <summary>
        /// 玩家本回合获胜
        /// </summary>
        private void PlayerWinThisRoundTask()
        {
            Debug.Log("玩家这回合获胜");
            mTimeCountDown.StopTask();
            mPlayerWinThisRoundTask.StopTask();
            mAddEnemyCountInThisRoundTask.StopTask();
            mCreateEnemyTask.StopTask();
            mTime = 15;
            mInitCreateEnemyCount = 0;
            this.SendEvent<PlayerWinThisRoundEvent>();
        }
        /// <summary>
        /// 本局的倒计时
        /// </summary>
        private void CountDown()
        {
            mTime--;
            TimeChangeInThisRoundEvent e = new TimeChangeInThisRoundEvent();
            e.time = mTime;
            this.SendEvent(e);
        }
        /// <summary>
        /// 开始下一局
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
        /// 从对象池中获取敌人
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
