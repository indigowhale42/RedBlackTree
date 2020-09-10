﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedBlackTreeHelp
{
    public static class RBTreePrinter
    {
        class NodeInfo
        {
            public RBNode Node;
            public string Text;
            public int StartPos;
            public int Size { get { return Text.Length; } }
            public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
            public NodeInfo Parent, Left, Right;
        }

        public static void PrintHorizontal(this RBNode node, int level)
        {
            if (node == null)
                return;

            PrintHorizontal(node.right, level + 1);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < level; i++)
                sb.Append("               ");
            sb.Append(node.data);

            string line = sb.ToString();
            

            if (node.parent != null)
                line += " p: " + node.parent.data.First().ToString();

            if (node.Color == RBColor.Red)
                Console.ForegroundColor = ConsoleColor.Red; // устанавливаем цвет
            else
                Console.ForegroundColor = ConsoleColor.DarkGray; // устанавливаем цвет

            Console.WriteLine(line);//вывод
            
            PrintHorizontal(node.left, level + 1);
        }

        public static void PrintColumn(this RBNode node)
        {
            if (node == null)
                return;

            PrintColumn(node.right);

            string line = node.data;


            if (node.parent != null)
                line += " p: " + node.parent.data.First().ToString();

            if (node.Color == RBColor.Red)
                Console.ForegroundColor = ConsoleColor.Red; // устанавливаем цвет
            else
                Console.ForegroundColor = ConsoleColor.DarkGray; // устанавливаем цвет

            Console.WriteLine(line);//вывод

            PrintColumn(node.left);
        }


        public static void PrintVertical(this RBNode root, string textFormat = "0", int spacing = 1, int topMargin = 2, int leftMargin = 2)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo>();

            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo { Node = next, Text = next.data };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + spacing;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.left)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos - 1);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos + 1);
                    }
                }
                next = next.left ?? next.right;
                for (; next == null; item = item.Parent)
                {
                    int top = rootTop + 2 * level;

                    if (item.Node.Color == RBColor.Red)
                        Console.ForegroundColor = ConsoleColor.Red; // устанавливаем цвет
                    else
                        Console.ForegroundColor = ConsoleColor.DarkGray; // устанавливаем цвет
                    Print(item.Text, top, item.StartPos);

                    if (item.Left != null)
                    {
                        Print("/", top + 1, item.Left.EndPos);
                        Print("_", top, item.Left.EndPos + 1, item.StartPos);
                    }
                    if (item.Right != null)
                    {
                        Print("_", top, item.EndPos, item.Right.StartPos - 1);
                        Print("\\", top + 1, item.Right.StartPos - 1);
                    }
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos + 1;
                        next = item.Parent.Node.right;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos - 1;
                        else
                            item.Parent.StartPos += (item.StartPos - 1 - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print(string s, int top, int left, int right = -1)
        {
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }
    }
}
