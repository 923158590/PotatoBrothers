using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface ISlotModel : IModel
    {

    }

    /// <summary>
    /// ����slot��Model ���ڴ��ǹ
    /// </summary>
    public class GunSlotModel : AbstractModel, ISlotModel
    {
        public IGunModel Gun { get; set; }

        protected override void OnInit()
        {

        }
    }
}
