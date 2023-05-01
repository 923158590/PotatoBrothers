using UnityEngine;

namespace Game
{
    public class CameraController : MonoBehaviour
    {
        private Transform mPlayerTrans;

        private float xMin = -10;
        private float xMax = 10;
        private float yMin = -10;
        private float yMax = 10;

        private Vector3 mTargetPos;

        void LateUpdate()
        {
            if (!mPlayerTrans)
            {
                var playerGameObj = GameObject.FindWithTag("Player");

                if (playerGameObj)
                {
                    mPlayerTrans = playerGameObj.transform;
                }
                else
                {
                    return;
                }
            }

            var isRight = Mathf.Sign(mPlayerTrans.transform.localScale.x);

            var playerPos = mPlayerTrans.transform.position;
            //mTargetPos.x = playerPos.x + 3 * isRight;
            //mTargetPos.y = playerPos.y + 2;
            //mTargetPos.z = -10;

            mTargetPos.x = playerPos.x;
            mTargetPos.y = playerPos.y;
            mTargetPos.z = -5;

            var smoothSpeed = 5;

            var position = transform.position;

            position = Vector3.Lerp(position, mTargetPos, smoothSpeed * Time.deltaTime);

            transform.position = new Vector3(Mathf.Clamp(position.x, xMin, xMax), Mathf.Clamp(position.y, yMin, yMax),
                position.z);
            //transform.position = new Vector3(position.x, position.y, position.z);
        }
    }
}