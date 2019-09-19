using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {

        static void Main(string[] args)
        {
            #region 数组
            //ArrayList num = new ArrayList();
            //num.Add("a");
            //num.Add("b");
            //num.Add("c");
            //foreach(var item in num)
            //{
            //    Console.WriteLine(num.IndexOf(item) + ":" + item);
            //}
            //Console.WriteLine(num.Capacity);
            //Console.WriteLine(num.Count);
            //num.Add("d");
            //num.Add("e");
            //num.Add("f");
            //foreach (var item in num)
            //{
            //    Console.WriteLine(num.IndexOf(item) + ":" + item);
            //}
            //Console.WriteLine(num.Capacity);
            //Console.WriteLine(num.Count);
            //num.Remove("f");
            //num.Remove("e");
            //num.Remove("d");
            //Console.WriteLine(num.Capacity);
            //Console.WriteLine(num.Count);
            #endregion

            #region 双向链表
            //LinkedList<string> linked = new LinkedList<string>();
            //linked.AddFirst("a");
            //linked.AddFirst("b");
            //linked.AddFirst("c");
            //linked.AddLast("d");

            //foreach(var item in linked)
            //{
            //    Console.WriteLine(item);
            //}

            //DoubleLink<int> dlink = new DoubleLink<int>();// 创建双向链表
            //Console.WriteLine("将 20 插入到表头之后");
            //dlink.Append(0, 10);
            //dlink.ShowAll();
            //Console.WriteLine("将 40 插入到表头之后");
            //dlink.Append(1, 30);
            //dlink.ShowAll();
            //Console.WriteLine("将 10 插入到表头之前");
            //dlink.Insert(0, 40);
            //dlink.ShowAll();
            //Console.WriteLine("将 30 插入到第一个位置之前");
            //dlink.Insert(1, 20);
            //dlink.ShowAll();

            //dlink.Insert(2, 25);
            //dlink.ShowAll();
            //Console.WriteLine("展示第一个:" + dlink.GetFirst());
            //Console.WriteLine("删除第一个");
            //dlink.DelFirst();
            //Console.WriteLine("展示第一个:" + dlink.GetFirst());
            //Console.WriteLine("展示最后一个:" + dlink.GetLast());
            //Console.WriteLine("删除最后一个");
            //dlink.DelLast();
            //Console.WriteLine("展示最后一个:" + dlink.GetLast());
            //dlink.ShowAll();
            //Console.ReadKey();
            #endregion

            #region 二叉查询树
            BinaryTree binaryTree = new BinaryTree();
            //for (var i = 0; i < 5; i++)
            //{
            //    Random random = new Random();
            //    int num = random.Next(0, 100);
            //    binaryTree.Add(num);
            //}
            binaryTree.Add(15);
            binaryTree.Add(20);
            binaryTree.Add(10);
            binaryTree.Add(5);
            binaryTree.Add(18);
            binaryTree.Add(6);
            binaryTree.Add(19);
            binaryTree.Add(17);
            binaryTree.Add(4);

            binaryTree.Preorder();


            Tree tree = binaryTree.Search(4);
            //if (tree != null)
            //{
            //    Console.WriteLine(tree.Data);
            //}
            binaryTree.Delete(4);
            #endregion

            #region Hash一致性
            //int Replicas = 100;
            //KetamaNodeLocator.AddNode("127.0.0.1:6379", Replicas);
            //KetamaNodeLocator.AddNode("127.0.0.1:6380", Replicas);
            //KetamaNodeLocator.AddNode("127.0.0.1:6381", Replicas);
            //List<string> nodes = new List<string>();
            //for (int i = 0; i < 100; i++)
            //{
            //    nodes.Add(KetamaNodeLocator.GetTargetNode(i + "test" + (char)i));
            //}
            //var counts = nodes.GroupBy(n => n, n => n.Count()).ToList();
            //counts.ForEach(index => Console.WriteLine(index.Key + "-" + index.Count()));

            //Console.ReadLine();
            #endregion

            Cat.sid = 100;
            Cat mimi = new Cat("mimi");
            Cat pipi = new Cat("pipi");
            bool bol = true;
            Int32 i = new Int32();
            int j = 1;
            int k = bol ? i : j;
            Console.WriteLine(k);

            Console.ReadLine();
        }

        #region 链表
        /// <summary>
        /// 链表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class BdNode<T>
        {
            public T Data { get; set; }

            public BdNode<T> Next { set; get; }

            public BdNode<T> Prev { set; get; }

            public BdNode(T val, BdNode<T> prev, BdNode<T> next)
            {
                this.Data = val;
                this.Prev = prev;
                this.Next = next;
            }
        }

        public class DoubleLink<T>
        {
            private readonly BdNode<T> _linkHead;//表头
            private int _size;//节点个数

            public DoubleLink()
            {
                _linkHead = new BdNode<T>(default(T), null, null);
                _linkHead.Prev = _linkHead;
                _linkHead.Next = _linkHead;
                _size = 0;
            }

            public int GetSize() => _size;

            public bool IsEmpty() => (_size == 0);

            private BdNode<T> GetNode(int index)
            {
                if (index < 0 || index > _size)
                {
                    throw new IndexOutOfRangeException("索引溢出或链表为空");
                }
                if (index < _size / 2)//正向查找
                {
                    BdNode<T> node = _linkHead.Next;
                    for (int i = 0; i < index; i++)
                    {
                        node = node.Next;
                    }
                    return node;
                }
                //方向查询
                BdNode<T> rnode = _linkHead.Prev;
                int rindex = _size - index - 1;
                for (int i = 0; i < rindex; i++)
                {
                    rnode = rnode.Prev;
                }
                return rnode;
            }

            public T Get(int index) => GetNode(index).Data;

            public T GetFirst() => GetNode(0).Data;

            public T GetLast() => GetNode(_size - 1).Data;

            // 将节点插入到第index位置之前
            public void Insert(int index, T t)
            {
                if (_size < 1 || index >= _size)
                {
                    throw new IndexOutOfRangeException("索引溢出或链表为空");
                }
                if (index == 0)
                {
                    Append(_size, t);
                }
                else
                {
                    BdNode<T> inode = GetNode(index);
                    BdNode<T> tnode = new BdNode<T>(t, inode.Prev, inode);
                    inode.Prev.Next = tnode;
                    inode.Prev = tnode;
                    _size++;
                }
            }

            //追加到index位置之后
            public void Append(int index, T t)
            {
                BdNode<T> inode;
                if (index == 0)
                {
                    inode = _linkHead;
                }
                else
                {
                    index = index - 1;
                    if (index < 0)
                    {
                        throw new IndexOutOfRangeException("位置不存在");
                    }
                    inode = GetNode(index);
                }
                BdNode<T> tnode = new BdNode<T>(t, inode, inode.Next);
                inode.Next.Prev = tnode;
                inode.Next = tnode;
                _size++;
            }

            public void Del(int index)
            {
                BdNode<T> inode = GetNode(index);
                inode.Prev.Next = inode.Next;
                inode.Next.Prev = inode.Prev;
                _size--;
            }

            public void DelFirst() => Del(0);

            public void DelLast() => Del(_size - 1);

            public void ShowAll()
            {
                Console.WriteLine("******************* 链表数据如下 *******************");
                for (int i = 0; i < _size; i++)
                    Console.WriteLine("(" + i + ")=" + Get(i));
                Console.WriteLine("******************* 链表数据展示完毕 *******************\n");
            }
        }

        #endregion

    }

    public class Cat
    {
        /**
         * 静态成员变量
         */
        public static int sid = 0;
        private String name;
        int id;
        public Cat(String _name)
        {
            this.name = _name;
            id = ++sid;
            _name = _name + "11";
            Console.WriteLine("My Name is " + name + ",NO." + id);
        }
        public void info()
        {

        }
    }
}
