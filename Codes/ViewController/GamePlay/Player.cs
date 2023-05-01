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
        // �ƶ���Χ������
        private Rect mMovementBounds;
        
        private static Player _instance;

        public static Player Instance
        {
            get
            {
                // ����������󲻴��ڣ��򴴽�һ���µĵ�������
                if (_instance == null)
                {
                    // �ڳ����в��ҵ�������
                    _instance = FindObjectOfType<Player>();

                    // ��������в����ڵ��������򴴽�һ���µĵ�������
                    if (_instance == null)
                    {
                        // ����һ���µĿ���Ϸ���󣬲���� Singleton ���
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

            // ��ʼ���ƶ���Χ
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

            // ��ȡ��Ҽ�������״̬
            var horizontalMovement = Input.GetAxis("Horizontal");
            var verticalMovement = Input.GetAxis("Vertical");
            // ���ö���״̬
            mAnimator.SetFloat("x", horizontalMovement);
            mAnimator.SetFloat("y", verticalMovement);
        }

        private void FixedUpdate()
        {
            // ��ȡ��Ҽ�������״̬
            var horizontalMovement = Input.GetAxis("Horizontal");
            var verticalMovement = Input.GetAxis("Vertical");
            // ��ȡ����ƶ��ٶ�
            var speed = this.GetModel<IPlayerModel>().speed;
            // ����ƶ�
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
        /// ����Ƿ�����
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
        /// ��ȡ����ƶ��ķ���
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
        /// �ж�����Ƿ񳬳������ƶ��ķ�Χ
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
