using QFramework;
using UnityEngine;

namespace Game
{
    public abstract class Game2DController : MonoBehaviour,IController
    {
        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return Game.Interface;
        }
    }
}