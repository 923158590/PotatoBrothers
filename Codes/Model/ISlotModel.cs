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
    /// 背包slot的Model 用于存放枪
    /// </summary>
    public class GunSlotModel : AbstractModel, ISlotModel
    {
        public IGunModel Gun { get; set; }

        protected override void OnInit()
        {

        }
    }
}
