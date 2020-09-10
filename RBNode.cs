using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTreeHelp
{
    /// <summary>
    /// Смотри требование 1 к красно-черным деревьям
    /// </summary>
    public enum RBColor { Red, Black };

    public class RBNode
    {
        RBColor _color;
        public RBColor Color { get { return _color; } set { _color = value; } }
        public string data; // информация, хранимая в узле
        public RBNode right, left, parent;

        public RBNode(string data, RBNode parent = null)
        {
            this.data = data;
            _color = RBColor.Red;
            right = left = null;
            this.parent = parent;
        }

        public RBNode Grandparent()
        {
            if (this.parent != null)
                return this.parent.parent;
            else
                return null;
        }

        public RBNode Uncle()
        {
            RBNode g = this.Grandparent();
            if (g == null)
                return null;  // No grandparent means no uncle

            if (this.parent == g.left)
                return g.right;
            else
                return g.left;
        }

        public RBNode Sibling()
        {
            if (this.parent == null)
                return null;
            if (this == this.parent.left)
                return this.parent.right;
            else
                return this.parent.left;
        }
    }
}
