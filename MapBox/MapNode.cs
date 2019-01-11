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
    public class MapNode : Node
    {
        /// <summary>
        /// 是否与定位点连线
        /// </summary>
        public bool IsLink
        {
            get
            {
                return (Tag & 2) == 2;
            }
        }

        /// <summary>
        /// 地图画刷
        /// </summary>
        public Brush Brush
        {
            get
            {
                if (Tag > 0)
                {
                    return Brushes.Green;
                }
                return Brushes.Yellow;
            }
        }

        // 构造
        public MapNode()
            : base()
        { }
        public MapNode(Node node) 
            : base(node)
        { }
        public MapNode(int id, int tag, int x, int y)
            : base(id, tag, x, y)
        { }
    }
}
