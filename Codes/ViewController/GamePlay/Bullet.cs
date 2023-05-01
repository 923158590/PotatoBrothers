using QFramework;
using UnityEngine;

namespace Game
{
    public class Bullet : Game2DController
    {
        private Rigidbody2D mRigidbody2D;

        public bool IsRecycled { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            //var isRight = Mathf.Sign(transform.lossyScale.x);
            
            //mRigidbody2D.velocity = Vector2.right * 10 * isRight; 
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                ReleaseObject();
            }
        }

        private void ReleaseObject()
        {
            gameObject.SetActive(false);
        }
    }
}