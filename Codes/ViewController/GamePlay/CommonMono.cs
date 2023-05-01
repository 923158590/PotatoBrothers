using UnityEngine;
using System;

namespace Game
{
    public class CommonMono : MonoBehaviour
    {
        private static Action mUpdateAction;
        public static void AddUpdateAction(Action fun) => mUpdateAction += fun;
        public static void RemoveUpdateAction(Action fun) => mUpdateAction -= fun;

        private void Update()
        {
            mUpdateAction?.Invoke();
        }
    }
}