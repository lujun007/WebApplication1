using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Tree
    {
        public int Data;
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
            //tree = new Tree() { Value = "A" };
            //tree.Left = new Tree()
            //{
            //    Value = "B",
            //    Left = new Tree() { Value = "D", Left = new Tree() { Value = "G" } },
            //    Right = new Tree() { Value = "E", Right = new Tree() { Value = "H" } }
            //};
            //tree.Right = new Tree() { Value = "C", Right = new Tree() { Value = "F" } };
        }

        //public static Tree GetTree()
        //{
        //    if (tree == null)
        //    {
        //        return tree = new Tree();
        //    }
        //    return tree;
        //}

        public void Add(int value)
        {
            Tree Parent;
            Tree newNode = new Tree() { Data = value };
            if (tree == null)//如果为空树，给它赋值到根节点
            {
                tree = new Tree() { Data = value };
            }
            else//否则的话根据规则将值插入到合适的位置
            {
                Tree node = tree;
                while (true)
                {
                    Parent = node;
                    if (value < node.Data)
                    {
                        node = node.Left;
                        if (node == null)
                        {
                            Parent.Left = newNode;
                            break;
                        }
                    }
                    else
                    {

                        node = node.Right;
                        if (node == null)
                        {
                            Parent.Right = newNode;
                            break;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Tree Search(int value)
        {
            Tree node = tree;
            while (true)
            {
                if (value < node.Data)
                {
                    if (node.Left == null)
                        break;
                    node = node.Left;
                }
                else if (value > node.Data)
                {
                    if (node.Right == null)
                        break;
                    node = node.Right;
                }
                else
                {
                    return node;
                }
            }
            if (node.Data != value)
            {
                return null;
            }
            return node;
        }

        #region 遍历
        /// <summary>
        /// 先需遍历
        /// 先根节点，然后左节点，后右节点
        /// </summary>
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
                    System.Console.Write(node.Data+" ");
                    node = node.Left;
                }
                else
                {
                    var item = stack.Pop();
                    node = item.Right;
                }
            }
        }

        /// <summary>
        /// 中序遍历
        /// 先左节点，然后根节点，后右节点
        /// </summary>
        public void InOrder()
        {
            if (tree == null)
            {
                return;
            }
            System.Collections.Generic.Stack<Tree> stack = new System.Collections.Generic.Stack<Tree>();
            Tree node = tree;
            while (true)
            {
                if (node != null)
                {
                    stack.Push(node);
                    node = node.Left;
                }
                else
                {
                    var item = stack.Pop();
                    System.Console.Write(node.Data + " ");
                    node = item.Right;
                }
            }
        }
        
        /// <summary>
        /// 后序遍历
        /// 先左节点，然后右节点，后根节点
        /// </summary>
        public void PostOrderNo()
        {
            if (tree == null)
            {
                return;
            }
            HashSet<Tree> visited = new HashSet<Tree>();
            System.Collections.Generic.Stack<Tree> stack = new System.Collections.Generic.Stack<Tree>();
            Tree node = tree;

            while (stack.Any())
            {
                node = stack.Peek();
                if (node!=null)
                {
                    stack.Push(node);
                    node = node.Left;
                }
                else
                {
                    var item = stack.Peek();
                    if (item.Right != null && !visited.Contains(item.Right))
                    {
                        node = item.Right;
                    }
                    else
                    {
                        System.Console.Write(node.Data + " ");
                        visited.Add(item);
                        stack.Pop();
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// 删除节点
        /// </summary>
        public void Delete(int value)
        {
            Tree node=tree;
            Tree father = null;
            Tree current = null;
            while (true)
            {
                if (value < node.Data)
                {
                    if (node.Left == null)
                        break;
                    father = node;
                    node = node.Left;
                }
                else if (value > node.Data)
                {
                    if (node.Right == null)
                        break;
                    father = node;
                    node = node.Right;
                }
                else
                {
                    current = node;
                    break;
                }
            }
            ////如果被删除的节点,没有子节点
            //if (cureent.Left == null && cureent.Right == null)
            //{
            //    //tree = new Tree();//直接清空
            //    cureent = null;
            //}
            ////如果被删除的节点,只有左节点
            //if (cureent.Left != null && cureent.Right == null)
            //{
            //    node = node.Left;
            //}
            ////如果被删除的节点，只有右节点
            //if (cureent.Left == null && cureent.Right != null)
            //{
            //    cureent = node.Right;
            //}
            ////如果被删除的节点，子节点都有
            //if (cureent.Left != null && cureent.Right != null)
            //{

            //}
        }
    }
}
