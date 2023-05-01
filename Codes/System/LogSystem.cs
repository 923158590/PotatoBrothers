using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface ILogSystem : ISystem 
    {
        string Log { get; set; }

        /// <summary>
        /// ������־��Ϣ
        /// </summary>
        public void SetLog(string message);
    }

    /// <summary>
    /// ��־ϵͳ��
    /// </summary>
    public class LogSystem : AbstractSystem, ILogSystem
    {
        private string mLog;
        public string Log 
        {
            get { return mLog; }
            set { mLog = value; } 
        }

        public IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }

        public void SetLog(string message)
        {
            mLog = message;
            Debug.Log(message);
            this.SendEvent<LogChanegEvent>();
        }

        protected override void OnInit()
        {

        }
    }
}
