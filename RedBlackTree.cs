using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using RedBlackTreeHelp;

namespace RedBlackTree
{
    /// <summary>
    /// Красно-чёрное дерево — двоичное дерево поиска, в котором каждый узел имеет атрибут цвет,
    /// принимающий значения красный или чёрный. В дополнение к обычным требованиям, 
    /// налагаемым на двоичные деревья поиска, к красно-чёрным деревьям применяются следующие требования:
    /// 1) Узел либо красный, либо чёрный.
    /// 2) Корень — чёрный. (В других определениях это правило иногда опускается. 
    ///    Это правило слабо влияет на анализ, так как корень всегда может быть изменен с красного на чёрный, 
    ///    но не обязательно наоборот).
    /// 3) Все листья(NIL) — чёрные.
    /// 4) Оба потомка каждого красного узла — чёрные.
    /// 5) Всякий простой путь от данного узла до любого листового узла, являющегося его потомком, 
    ///    содержит одинаковое число чёрных узлов.
    /// </summary>
    class RedBlackTree
    {
        private RBNode _root;
        private int _count = 0;

        public int Count { get { return _count; } }
        private RBNode Root { get { return _root; } }

        public RedBlackTree()
        {
            _root = null;
        }

        public void PrintTreeConsoleVertical()
        {
            _root.PrintVertical();
            Console.ResetColor();
        }

        public void PrintTreeConsoleHorizontal()
        {
            _root.PrintHorizontal(0);
            Console.ResetColor();
        }

        public void PrintTreeConsoleColumn()
        {
            _root.PrintColumn();
            Console.ResetColor();
        }

        public void AddData(string data)
        {
            RBNode n = InsertRBNode(data);
            if (n == null)
                return;
            else
                _count++;
            CorrectCase1(n);
        }

        public void DeleteData(string data)
        {
            RBNode[] nodes = RemoveRBNodeFindReplaced(data);
            if (nodes == null)
                return;
            RBNode n = nodes[0];
            RBNode replaced = nodes[1];

            if (n.Color == RBColor.Black)
            {
                if (replaced.Color == RBColor.Red)
                    replaced.Color = RBColor.Black;
                else
                    DeleteCorrectCase1(replaced);
            }
        }

        public void Clear()
        {
            _root = null;
            _count = 0;
        }

        private RBNode InsertRBNode(string data)
        {
            if (_root == null)
                return new RBNode(data);

            RBNode tmp = _root;
            while (tmp != null)
            {
                if (data.CompareTo(tmp.data) > 0) // Если значение больше, чем значение текущего узла
                {
                    if (tmp.right != null)
                    {
                        tmp = tmp.right;
                    }
                    else
                    {
                        RBNode inseted = new RBNode(data, tmp);
                        tmp.right = inseted;
                        return inseted;
                    }
                }
                else if (data.CompareTo(tmp.data) < 0)
                {
                    if (tmp.left != null)
                    {
                        tmp = tmp.left;
                    }
                    else
                    {
                        RBNode inseted = new RBNode(data, tmp);
                        tmp.left = inseted;
                        return inseted;
                    }
                }
                else
                    return null;
            }

            return null;
            throw new Exception("WHAT? in Insert method");

        }

        private void CorrectCase1(RBNode n)
        {
            /*
             *   пустое дерево
             */
            if (n.parent == null) 
            {
                n.Color = RBColor.Black;
                _root = n; // Добавляем корень
            }
            /*
             *   некое непустое дерево
             */
            else
                CorrectCase2(n);
        }

        private void CorrectCase2(RBNode n)
        {
            // Дерево непустое, значитродитель точно есть
            if (n.parent == null)
                throw new NullReferenceException("У узла точно прям 100500 должен быть родитель");
            /* 
             *    Если родитель черный, то все ОК; св 5 сохр
             */
            if (n.parent.Color == RBColor.Black)
                return; /* Tree is still valid */
            /* 
             *    Если родитель красный, то родитель и потомок красные - неправильно по св. 4
             */
            else
                CorrectCase3(n);
        }

        private void CorrectCase3(RBNode n)
        {
            // Дерево непустое, значитродитель точно есть
            if (n.parent == null)
                throw new NullReferenceException("У узла точно прям 100500 должен быть родитель");

            RBNode u = n.Uncle();
            /*   Оба потомка каждого красного узла не черные (правый или левый родитель или узел не важно)
             *   дядя сущ. и красный (родитель тож красный из 2 кейса)
             *          g, ч                       g, к
             *         /    \                     /     \
             *       p, к    u, к     =>         р, ч    u, ч
             *      /    \                      /    \
             *     n, к                        n, к
             */
            if ((u != null) && (u.Color == RBColor.Red))// && (n->parent->color == RED) 
                                                        // Второе условие проверяется в AddNodeCase2,
                                                        // то есть родитель уже является красным.
            {
                n.parent.Color = RBColor.Black;
                u.Color = RBColor.Black;
                RBNode g = n.Grandparent();
                g.Color = RBColor.Red;
                CorrectCase1(g);
            }
            else // дядя не сущ или черный
                CorrectCase4(n);
        }

        private void CorrectCase4(RBNode n)
        {
            // Дерево непустое, значитродитель точно есть
            if (n.parent == null)
                throw new NullReferenceException("У узла точно прям 100500 должен быть родитель");

            RBNode g = n.Grandparent();
            // И т.к родитель красный, то дед точно есть, потому что корень всегда черный и р не мб корнем
            if (g == null)
                throw new NullReferenceException("У узла точно прям 100500 должен быть дед ибо родитель красный");

            RBNode n1 = n;

            // дядя не сущ или черный (родитель красный)
            /*       g,ч                    g,2
             *     /    \         или      /    \
             *    p,к    u,ч              u,2    p, k   (с этим ничего не делаем)
             *   /  \                            /   \
             *       n,к                             n,k
             *                 стало:
             *      g,ч                    g,2
             *     /    \         или     /    \
             *    n,к    u,ч            u,2    p,k
             *   /                                \  
             *  p,к                               n,k
             *  (n1)                             (n1)
             */
            if ((n == n.parent.right) && (n.parent == g.left))
            /*|| ((n == n.parent.right) && (n.parent == g.right)) - с этим ничего не делаем. кейс 5 все сделает*/
            {
                RotateLeft(n.parent);
                n1 = n.left;
            }
            /*      g,ч                  g,2
             *     /    \        или    /   \
             *    u,ч   p,к            p,k   u,2
             *          /  \          /  \
             *         n,к           n,k
             *         
             *                 стало:
             *                 
             *      g,ч                  g,2
             *     /    \        или     /   \
             *    u,ч   n,к             p,k   u,2
             *            \            /
             *            p,к         n,k
             *            (n1)        (n1)
             */
            else if ((n == n.parent.left) && (n.parent == g.right))
            //   || ((n == n.parent.left) && (n.parent == g.left)) - с этим тож ничего не делаем
            {
                RotateRight(n.parent);
                n1 = n.right;
            }
            CorrectCase5(n1);
        }

        private void CorrectCase5(RBNode n)
        {
            RBNode g = n.Grandparent();
            n.parent.Color = RBColor.Black;
            g.Color = RBColor.Red;

            /*      g,к                   
             *     /    \        
             *    p,ч    u,ч         
             *   /                         
             *  n,к                        
             *  (n1)                       
             */
            if ((n == n.parent.left) && (n.parent == g.left))
            {
                RotateRight(g);
            }
            /*        g,к
             *      /   \
             *           p,ч
             *            \
             *             n,k
             */
            else if ((n == n.parent.right) && (n.parent == g.right) )
            {
                RotateLeft(g);
            }
        }

        private void DeleteCorrectCase1(RBNode n)
        {
            if (n.parent == null)
                _root = n;
            else
                DeleteCorrectCase2(n);
        }

        private void DeleteCorrectCase2(RBNode n)
        {
            // у n черный цвет - 100500 и есть родитель
            // если есть родитель и у n черный цвет, то по св-ву 5 есть и брат

            RBNode s = n.Sibling();
            
            if (s.Color == RBColor.Red)  // родитель при этом не мб красным! ибо св-во 4
            {
	            n.parent.Color = RBColor.Red;
	            s.Color = RBColor.Black;
	            if (n == n.parent.left)
		            RotateLeft(n.parent);
	            else
		            RotateRight(n.parent);       
            }
            else
                DeleteCorrectCase3(n);
        }

       
        private void DeleteCorrectCase3(RBNode n)
        {
            ///
            ///
            RBNode s = n.Sibling();

            if ((n.parent.Color == RBColor.Black) && 
                (s.Color == RBColor.Black) && 
                (s.left.Color == RBColor.Black) &&
                (s.right.Color == RBColor.Black))
            {
                s.Color = RBColor.Red;
                DeleteCorrectCase1(n.parent);
            }
            else
                DeleteCorrectCase4(n);
        }

        private void DeleteCorrectCase4(RBNode n)
        {
            RBNode s = n.Sibling();

            if ((n.parent.Color == RBColor.Red) &&
                (s.Color == RBColor.Black) &&
                (s.left.Color == RBColor.Black) &&
                (s.right.Color == RBColor.Black))
            {
                s.Color = RBColor.Red;
                n.parent.Color = RBColor.Black;
            }
            else
                DeleteCorrectCase5(n);
        }

        private void DeleteCorrectCase5(RBNode n)
        {
            RBNode s = n.Sibling();

            if (s.Color == RBColor.Black)
            {
                if ((n == n.parent.left) &&
                    (s.right.Color == RBColor.Black) &&
                    (s.left.Color == RBColor.Red))
                {
                    s.Color = RBColor.Red;
                    s.left.Color = RBColor.Black;
                    RotateRight(s);
                }
                else if ((n == n.parent.right) &&
                    (s.left.Color == RBColor.Black) &&
                    (s.right.Color == RBColor.Red))
                {
                    s.Color = RBColor.Red;
                    s.right.Color = RBColor.Black;
                    RotateLeft(s);
                }
            }
            DeleteCorrectCase6(n);
        }

        private void DeleteCorrectCase6(RBNode n)
        {
            RBNode s = n.Sibling();

            s.Color = n.parent.Color;
            n.parent.Color = RBColor.Black;

            if (n == n.parent.left)
            {
                s.right.Color = RBColor.Black;
                RotateLeft(n.parent);
            }
            else
            {
                s.left.Color = RBColor.Black;
                RotateRight(n.parent);
            }
        }


        private RBNode[] RemoveRBNodeFindReplaced(string data)
        {
            RBNode[] nodes = new RBNode[3];

            RBNode n = SearchRBNode(data);

            if (n == null)
                return null;

            nodes[2] = n.Sibling();

            RBNode replaced = null;
            RBNode p = n.parent;

            if (n.left == null || n.right == null)
            {
                if (n.left == null)
                    replaced = n.right;
                else
                    replaced = n.left;
            }
            else
            {
                replaced = FindMax(n.left);

                if (replaced != n.left)
                {
                    replaced.parent.right = null;
                    replaced.left = n.left;
                    n.left.parent = replaced;
                }

                replaced.right = n.right;
                n.right.parent = replaced;
            }

            if (replaced != null)
                replaced.parent = p;
            //else
            //{ 
              //  DeleteCorrectCase1(n);
            //}

            if (p != null)
            {
                if (n == p.left)
                    p.left = replaced;
                else if (n == p.right)
                    p.right = replaced;
            }

            _count--;
            nodes[0] = n;
            nodes[1] = replaced;
            return nodes;

        }

        public bool SearchWord( string value )
        {
            RBNode searched = SearchRBNode(value);

            if (searched == null)
                return false;
            else
                return true;
        }

        private RBNode SearchRBNode(string value)
        {
            RBNode tmp = _root;
            while (tmp != null)
            {
                if (value.CompareTo(tmp.data) > 0)
                    tmp = tmp.right;
                else if (value.CompareTo(tmp.data) < 0)
                    tmp = tmp.left;
                else
                    return tmp;

            }
            return null;
        }

        private RBNode FindMax(RBNode n)
        {
            RBNode max = n;

            if (max == null)
                return null;

            while (max.right != null)
            {
                max = max.right;
            }
            return max;
        }

        private void RotateLeft(RBNode n)
        {
            /*          Pivot - 2, n - 1
             *          
             *       1                 2
             *      /  \             /   \
             *    a     2     =>    1     c
             *         /  \        / \
             *        b    c      a   b
             */
            RBNode pivot = n.right;

            pivot.parent = n.parent; /* при этом, возможно, pivot (ось вращения) становится корнем дерева */
            if (n.parent != null)
            {
                if (n.parent.left == n)
                    n.parent.left = pivot;
                else
                    n.parent.right = pivot;
            }
            else
                _root = pivot;

            n.right = pivot.left;
            if (pivot.left != null)
                pivot.left.parent = n;

            n.parent = pivot;
            pivot.left = n;
        }

        private void RotateRight(RBNode n)
        {
            /*          Pivot - 2, n - 1
             *          
             *       2                 1
             *      /  \             /   \
             *    b     1     <=    2     a
             *         /  \        / \
             *        c    a      b   c
             */

            RBNode pivot = n.left;
	
            pivot.parent = n.parent; /* при этом, возможно, pivot становится корнем дерева */
            if (n.parent != null)
            {
                if (n.parent.left == n)
                    n.parent.left = pivot;
                else
                    n.parent.right = pivot;
            }
            else
                _root = pivot;

            n.left = pivot.right;
            if (pivot.right != null)
                pivot.right.parent = n;

            n.parent = pivot;
            pivot.right = n;
        }
    }
}

