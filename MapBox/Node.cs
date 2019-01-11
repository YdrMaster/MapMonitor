using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MapBox
{
    /// <summary>
    /// 地图或移动节点
    /// </summary>
    public class Node
    {
        /// <summary>
        /// ID
        /// </summary>
        public readonly int Id = 0;

        /// <summary>
        /// tag
        /// </summary>
        public int Tag = -1;

        /// <summary>
        /// X坐标
        /// </summary>
        public int X = 0;

        /// <summary>
        /// Y坐标
        /// </summary>
        public int Y = 0;

        /// <summary>
        /// 航向角
        /// </summary>
        public float Yaw = float.NaN;

        /// <summary>
        /// 坐标
        /// </summary>
        public Point Coordinate
        {
            get
            {
                return new Point(X, Y);
            }
        }

        // 构造
        public Node() { }
        public Node(Node node)
        {
            Id = node.Id;
            Tag = node.Tag;
            X = node.X;
            Y = node.Y;
            Yaw = node.Yaw;
        }
        public Node(int id, int tag, int x, int y)
        {
            Id = id;
            Tag = tag;
            X = x;
            Y = y;
        }
        public Node(int id, int tag, int x, int y, float yaw)
        {
            Id = id;
            Tag = tag;
            X = x;
            Y = y;
            Yaw = yaw;
        }
        
        /// <summary>
        /// 距离
        /// </summary>
        public int distance(Node node)
        {
            return (int)(Math.Sqrt((node.X - X) * (node.X - X) + 
                (node.Y - Y) * (node.Y - Y)) + 0.5);
        }
    }
}
