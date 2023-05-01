using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface IPlayerCreateSystem : ISystem
    {
        public void CreatePlayer(int x, int y);
    }
    public class PlayerCreateSystem : AbstractSystem, IPlayerCreateSystem
    {
        private GameObject mPlayerPrefab;
        private GameObject mPlayerGo;

        protected override void OnInit()
        {
            mPlayerPrefab = Resources.Load<GameObject>("Prefab/Player");
            this.RegisterEvent<PlayerWinThisRoundEvent>(OnPlayerWinThisRound);
            this.RegisterEvent<NextRoundStartEvent>(OnStartNextRound);
        }

        private void OnStartNextRound(NextRoundStartEvent e)
        {
            mPlayerGo.transform.localPosition = new Vector2(10, 10);
            Player.Instance.enabled = true;
        }

        private void OnPlayerWinThisRound(PlayerWinThisRoundEvent e)
        {
            Player.Instance.enabled = false;
        }

        void IPlayerCreateSystem.CreatePlayer(int x, int y) 
        {
            mPlayerGo = GameObject.Instantiate(mPlayerPrefab);
            mPlayerGo.transform.localPosition = new Vector2(x, y);
        }
    }
}

