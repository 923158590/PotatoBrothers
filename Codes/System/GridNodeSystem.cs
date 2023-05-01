using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Node
    { 
        public enum E_Type { Ground ,Wall}
        public E_Type type;
    }

    public interface IGridNodeSystem : ISystem 
    {
        void CreateGrid(int w, int h);
        Vector2Int FindBlockPos(int w, int h);
    }

    public class GridNodeSystem : AbstractSystem, IGridNodeSystem
    {
        private Node[,] mNodes;
        protected override void OnInit()
        {

        }

        void IGridNodeSystem.CreateGrid(int w, int h)
        {
            if (mNodes == null || mNodes.GetLength(0) < w || mNodes.GetLength(1) < h) mNodes = new Node[w + 1, h + 1];

            var e = new CreateNodeEvent();

            for (int row = 0; row < w + 1; row++)
            {
                for (int col = 0; col < h + 1; col++)
                {
                    if (mNodes[row, col] == null) mNodes[row, col] = new Node();
                    mNodes[row, col].type = row == w || col == h || row == 0 || col == 0 ? Node.E_Type.Wall : Node.E_Type.Ground;
                    e.type = mNodes[row, col].type;
                    e.pos = new Vector2(row, col);
                    this.SendEvent(e);
                }
            }

            //if (mNodes == null || mNodes.GetLength(0) < w || mNodes.GetLength(1) < h) mNodes = new Node[w + 1, h + 1];

            //var e = new CreateNodeEvent();

            //for (int row = 0; row < w + 1; row++)
            //{
            //    for (int col = 0; col < h + 1; col++)
            //    {
            //        if (mNodes[row, col] == null) mNodes[row, col] = new Node();
            //        mNodes[row, col].type = row == w || col == h || row == 0 || col == 0 ? Node.E_Type.Ground : Node.E_Type.Wall;
            //        e.type = mNodes[row, col].type;
            //        e.pos = new Vector2(row, col);
            //        this.SendEvent(e);
            //    }
            //}
        }


        /// <summary>
        /// 随机返回一个位置节点
        /// </summary>
        /// <param name="w">地图的宽度</param>
        /// <param name="h">地图的高度</param>
        /// <returns></returns>
        Vector2Int IGridNodeSystem.FindBlockPos(int w, int h)
        {
            Node node;
            int x, y;
            do
            {
                x = UnityEngine.Random.Range(1, w - 1);
                y = UnityEngine.Random.Range(1, h - 1);
                node = mNodes[x, y];
            }
            while (node.type != Node.E_Type.Ground);
            return new Vector2Int(x, y);
        }
    }
}

