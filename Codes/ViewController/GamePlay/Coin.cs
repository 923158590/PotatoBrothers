using Game;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Coin : Game2DController
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                this.SendCommand<AddCoinCommand>();
                Destroy(gameObject);
            }
        }
    }
}
