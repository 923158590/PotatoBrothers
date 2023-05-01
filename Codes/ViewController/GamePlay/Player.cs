using QFramework;
using System;
using UnityEngine;

namespace Game
{
    public class Player : Game2DController
    {
        private Rigidbody2D mRigidbody2D;
        private Animator mAnimator;
        private Transform mWeapon;
        // 移动范围的限制
        private Rect mMovementBounds;
        
        private static Player _instance;

        public static Player Instance
        {
            get
            {
                // 如果单例对象不存在，则创建一个新的单例对象
                if (_instance == null)
                {
                    // 在场景中查找单例对象
                    _instance = FindObjectOfType<Player>();

                    // 如果场景中不存在单例对象，则创建一个新的单例对象
                    if (_instance == null)
                    {
                        // 创建一个新的空游戏对象，并添加 Singleton 组件
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<Player>();
                        singletonObject.name = typeof(Player).ToString() + " (Singleton)";
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
            mAnimator = GetComponent<Animator>();
            mWeapon = transform.Find("Weapon");

            // 初始化移动范围
            mMovementBounds = new Rect(-0.5f, -0.5f, 20.5f, 20.5f);

            this.RegisterEvent<PlayerWinThisRoundEvent>(OnPlayerWinThisRoundEvent);

        }

        private void OnPlayerWinThisRoundEvent(PlayerWinThisRoundEvent e)
        {
            mRigidbody2D.velocity = Vector2.zero;
        }

        private void Update()
        {
            CheckIsLevelUp();
            BoundPlayerMove();

            // 获取玩家键盘输入状态
            var horizontalMovement = Input.GetAxis("Horizontal");
            var verticalMovement = Input.GetAxis("Vertical");
            // 设置动画状态
            mAnimator.SetFloat("x", horizontalMovement);
            mAnimator.SetFloat("y", verticalMovement);
        }

        private void FixedUpdate()
        {
            // 获取玩家键盘输入状态
            var horizontalMovement = Input.GetAxis("Horizontal");
            var verticalMovement = Input.GetAxis("Vertical");
            // 获取玩家移动速度
            var speed = this.GetModel<IPlayerModel>().speed;
            // 玩家移动
            mRigidbody2D.velocity = new Vector2(horizontalMovement * speed, verticalMovement * speed);

            if (horizontalMovement > 0 && transform.localScale.x < 0 ||
            horizontalMovement < 0 && transform.localScale.x > 0)
            {
                var localScale = transform.localScale;
                localScale.x = -localScale.x;
                transform.localScale = localScale;

                var weaponLocalScale = mWeapon.localScale;
                weaponLocalScale.x = -weaponLocalScale.x;
                mWeapon.localScale = weaponLocalScale;
            }
        }

        /// <summary>
        /// 检查是否升级
        /// </summary>
        private void CheckIsLevelUp()
        {
            var exp = this.GetModel<IPlayerModel>().EXP.Value;
            var maxExp = this.GetModel<IPlayerModel>().maxEXP.Value;
            if (exp >= maxExp) this.SendCommand<PlayerLevelUpCommand>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Wall"))
            {
                mRigidbody2D.velocity = Vector2.zero;
            }
        }

        /// <summary>
        /// 获取玩家移动的方向
        /// </summary>
        /// <returns></returns>
        private Vector2 GetPlayerInput()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            // create a new vector from the input values and normalize it
            Vector2 inputVector = new Vector2(horizontal, vertical).normalized;

            return inputVector;
        }
        /// <summary>
        /// 判断玩家是否超出可以移动的范围
        /// </summary>
        private void BoundPlayerMove()
        {
            // get player input and calculate movement vector
            float speed = this.GetModel<IPlayerModel>().speed;
            Vector2 movement = GetPlayerInput() * speed * Time.deltaTime;

            // calculate new position
            Vector2 newPosition = (Vector2)transform.position + movement;

            // check if new position is inside movement bounds
            if (mMovementBounds.Contains(newPosition))
            {
                // set player position to new position
                transform.position = newPosition;
            }
            else
            {
                // if player is outside the movement bounds, limit its position to the bounds
                transform.position = new Vector2(
                    Mathf.Clamp(newPosition.x, mMovementBounds.xMin, mMovementBounds.xMax),
                    Mathf.Clamp(newPosition.y, mMovementBounds.yMin, mMovementBounds.yMax)
                );
            }
        }
    }
}
