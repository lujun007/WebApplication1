using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Tree
    {
        public string Value;
        public Tree Left;
        public Tree Right;
    }

    /// <summary>
    /// 二叉树_顺序存储
    /// </summary>
    public class BinaryTree
    {
        private Tree tree;

        /// <summary>
        /// 初始化一个树
        /// </summary>
        public BinaryTree()
        {
            tree = new Tree() { Value = "A" };
            tree.Left = new Tree()
            {
                Value = "B",
                Left = new Tree() { Value = "D", Left = new Tree() { Value = "G" } },
                Right = new Tree() { Value = "E", Right = new Tree() { Value = "H" } }
            };
            tree.Right = new Tree() { Value = "C", Right = new Tree() { Value = "F" } };
        }
        public void Preorder()
        {
            if (tree == null)
            {
                return;
            }
            System.Collections.Generic.Stack<Tree> stack = new System.Collections.Generic.Stack<Tree>();
            Tree node = tree;
            while (node != null || stack.Any())
            {
                if (node != null)
                {
                    stack.Push(node);
                    System.Console.Write(node.Value+" ");
                    node = node.Left;
                }
                else
                {
                    var item = stack.Pop();
                    node = item.Right;
                }
            }
        }
    }
}
