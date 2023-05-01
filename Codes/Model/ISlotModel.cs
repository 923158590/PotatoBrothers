using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public interface ISlotModel : IModel
    {

    }

    public class GunSlotModel : AbstractModel, ISlotModel
    {
        public IGunModel Gun { get; set; }

        protected override void OnInit()
        {

        }
    }
}
