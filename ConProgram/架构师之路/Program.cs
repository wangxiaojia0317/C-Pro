#define TEST
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace programer
{
#if TEST
    class Program
    {
        static void Main()
        {
            RedBlackTree<int> br = new RedBlackTree<int>();

            while (true)
            {
                int a =int.Parse(Console.ReadLine());
                br.Add(a);
            }
           
            Console.ReadKey();
            /*删除结束*/


        }
    }
#endif

    #region 数据结构篇

    #region 队列
    /*
    Queue<T>
    命名空间 System.Collections.Genertic
    DLL: System.dll

    继承层次结构：
        System.Object
            System.Collections.Genertic.Queue<T>   

    语法：（C#）
      [SerializableAttribute]
      [ComVisibleAttribute(false)]
      public class Queue<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>  

    构造函数：
      1.Queue<T>(): 初始化 Queue<T> 类的新实例，该实例为空并且具有默认初始容量。
      2.Queue<T>(IEnumerable<T>):初始化 Queue<T> 类的新实例，该实例包含从指定集合复制的元素并且具有足够的容量来容纳所复制的元素。
      3.Queue<T>(Int32):初始化 Queue<T> 类的新实例，该实例为空并且具有指定的初始容量。

    属性：
        Count:获取队列中的元素

    方法：
        1.Clear() 从Queue<T> 中移除所有对象。

        2.Contains(T)确定某元素是否在 Queue<T> 中。

        3.CopyTo(T[], Int32)从指定数组索引开始将 Queue<T> 元素复制到现有一维 Array 中。

        4.Enqueue(T)将对象添加到 Queue<T> 的结尾处。

        5.Equals(Object)确定指定的对象是否等于当前对象。（继承自 Object。）

        6.Finalize() 在垃圾回收将某一对象回收前允许该对象尝试释放资源并执行其他清理操作。（继承自 Object。）（*在此处，Finalize（）函数主要是释放非托管资源的，析构函数的目的就是如此，对比于Dispose（）函数，它也是释放资源的，不过它释放的是资源是所有的[托管的与非托管的都释放]）
            由于析构函数的调用将导致GC对对象回收的效率降低，所以如果已经完成了析构函数该干的事情（例如释放非托管资源），就应当使用SuppressFinalize方法告诉GC不需要再执行某个对象的析构函数。 

        7.GetEnumerator()返回循环访问 Queue<T> 的枚举数。

        8.GetHashCode() 作为默认哈希函数。（继承自 Object。）

        9.GetType() 获取当前实例的 Type。（继承自 Object。）

        10.MemberwiseClone()创建当前 Object 的浅表副本。（继承自 Object。）

        11.Peek()返回位于 Queue<T> 开始处的对象但不将其移除。

        12.ToArray()将 Queue<T> 元素复制到新数组。

        13.ToString()返回表示当前对象的字符串。（继承自 Object。）

        13.TrimExcess()如果元素数小于当前容量的 90%，将容量设置为 Queue<T> 中的实际元素数。

        14.Dequeue()移除并返回位于 Queue<T> 开始处的对象。

    */
    class QueueExample
    {
        public static void QueueMethod()
        {
            Queue<string> numbers = new Queue<string>();
            numbers.Enqueue("one");
            numbers.Enqueue("two");
            numbers.Enqueue("three");
            numbers.Enqueue("four");
            numbers.Enqueue("five");

            // A queue can be enumerated without disturbing its contents.
            foreach (string number in numbers)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("\nDequeuing '{0}'", numbers.Dequeue());
            Console.WriteLine("Peek at next item to dequeue: {0}",
                numbers.Peek());
            Console.WriteLine("Dequeuing '{0}'", numbers.Dequeue());

            // Create a copy of the queue, using the ToArray method and the
            // constructor that accepts an IEnumerable<T>.
            Queue<string> queueCopy = new Queue<string>(numbers.ToArray());

            Console.WriteLine("\nContents of the first copy:");
            foreach (string number in queueCopy)
            {
                Console.WriteLine(number);
            }

            // Create an array twice the size of the queue and copy the
            // elements of the queue, starting at the middle of the 
            // array. 
            string[] array2 = new string[numbers.Count * 2];
            numbers.CopyTo(array2, numbers.Count);

            // Create a second queue, using the constructor that accepts an
            // IEnumerable(Of T).
            Queue<string> queueCopy2 = new Queue<string>(array2);

            Console.WriteLine("\nContents of the second copy, with duplicates and nulls:");
            foreach (string number in queueCopy2)
            {
                Console.WriteLine(number);
            }

            Console.WriteLine("\nqueueCopy.Contains(\"four\") = {0}",
                queueCopy.Contains("four"));

            Console.WriteLine("\nqueueCopy.Clear()");
            queueCopy.Clear();
            Console.WriteLine("\nqueueCopy.Count = {0}", queueCopy.Count);
        }
    }


    #endregion

    #region 集合

    #region ArrayList
    //一个ArrayList是标准System.Collctions命名空间。它是一个动态数组。它提供了随机访问元素。一个ArrayList自动扩展数据添加。与数组,数组列表可以容纳多个数据类型的数据。数组列表中的元素通过一个整数索引访问。
    //索引是零基础。元素的索引,插入和删除的ArrayList需要持续时间。插入或删除一个元素的动态数组更昂贵。线性时间。
    #endregion

    #region List

    #endregion

    #region LinkedList
    public class LinkedListExample
    {
        public static void LinkedListMain()
        {
            // Create the link list.
            string[] words =
                { "the", "fox", "jumped", "over", "the", "dog" };
            LinkedList<string> sentence = new LinkedList<string>(words);



            LinkedListNode<string> current = sentence.FindLast("the");
            IndicateNode(current, "Test 5: Indicate last occurence of 'the':");

            // Add 'lazy' and 'old' after 'the' (the LinkedListNode named current).
            sentence.AddAfter(current, "old");
            sentence.AddAfter(current, "lazy");
            IndicateNode(current, "Test 6: Add 'lazy' and 'old' after 'the':");



            Console.ReadLine();
        }

        private static void Display(LinkedList<string> words, string test)
        {
            Console.WriteLine(test);
            foreach (string word in words)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void IndicateNode(LinkedListNode<string> node, string test)
        {
            Console.WriteLine(test);
            if (node.List == null)
            {
                Console.WriteLine("Node '{0}' is not in the list.\n",
                    node.Value);
                return;
            }

            StringBuilder result = new StringBuilder("(" + node.Value + ")");
            LinkedListNode<string> nodeP = node.Previous;

            while (nodeP != null)
            {
                result.Insert(0, nodeP.Value + " ");
                nodeP = nodeP.Previous;
            }

            node = node.Next;
            while (node != null)
            {
                result.Append(" " + node.Value);
                node = node.Next;
            }

            Console.WriteLine(result);
            Console.WriteLine();
        }
    }


    #endregion

    #endregion

    #endregion


    #region 常用算法
    #region 二叉树









    #region 二叉查找树

    //public class Node
    //{
    //    public int data;
    //    public Node left;
    //    public Node right;


    //    public void DisplayData()
    //    {

    //        Console.Write(data + "  ");

    //    }
    //}

    //public class Tree
    //{
    //    public Node root;
    //    public void Insert(int data)
    //    {
    //        if (root == null)
    //        {
    //            root = new Node();
    //            root.data = data;
    //        }
    //        else
    //        {
    //            Node current = root;
    //            while (true)
    //            {
    //                if (current.data > data)
    //                {

    //                    if (current.left != null)
    //                    {
    //                        current = current.left;
    //                    }
    //                    else
    //                    {
    //                        Node newNode = new Node();
    //                        newNode.data = data;
    //                        current.left = newNode;
    //                        break;
    //                    }
    //                }
    //                else
    //                {

    //                    if (current.right != null)
    //                    {
    //                        current = current.right;
    //                    }
    //                    else
    //                    {
    //                        Node newNode = new Node();
    //                        newNode.data = data;
    //                        current.right = newNode;
    //                        break;
    //                    }
    //                }
    //            }
    //        }


    //    }

    //    //注意递归的本质，进出关系
    //    public void InOrder(Node root)
    //    {
    //        if (root != null)
    //        {
    //            InOrder(root.left);
    //            root.DisplayData();
    //            InOrder(root.right);
    //        }

    //    }


    //    /// <summary>
    //    /// 根据二叉树的特性，最右子节点为最大值
    //    /// </summary>
    //    /// <returns></returns>
    //    public int FindMax()
    //    {
    //        int data = root.data;
    //        Node node = root;
    //        while (node.right != null)
    //        {
    //            node = node.right;
    //        }

    //        return node.data;
    //    }
    //    public Node FindMin()
    //    {

    //        Node node = root;
    //        while (node.left != null)
    //        {
    //            node = node.left;
    //        }
    //        return node;
    //    }



    //    public Node Delete(int key)
    //    {
    //        Node parent = root;

    //        Node current = root;

    //        //删除的节点为根结点时
    //        if (root.data == key)
    //        {
    //            while (current.left != null)
    //            {
    //                parent = current;
    //                current = current.left;
    //            }

    //            parent.left = null;

    //            current.left = root.left;
    //            current.right = root.right;
    //            root = current;

    //        }
    //        else
    //        {
    //            //寻找到该节点（非递归查找）
    //            while (true)
    //            {
    //                if (key < current.data)
    //                {
    //                    if (current.left == null) break;
    //                    parent = current;
    //                    current = current.left;
    //                }
    //                else if (key > current.data)
    //                {
    //                    if (current.right == null) break;
    //                    parent = current;
    //                    current = current.right;
    //                }
    //                else
    //                {
    //                    break;
    //                }
    //            }

    //            //无节点
    //            if (current.left == null & current.right == null)
    //            {
    //                if (key > parent.data)
    //                {
    //                    parent.right = null;
    //                }
    //                else
    //                {
    //                    parent.left = null;
    //                }
    //            }
    //            //有左节点
    //            else if (current.left != null & current.right == null)
    //            {
    //                parent.left = current.left;
    //            }
    //            //有右左节点
    //            else if (current.left == null & current.right != null)
    //            {
    //                parent.right = current.right;
    //            }
    //            //左右节点都有的
    //            else if (current.left != null & current.right != null)
    //            {
    //                parent.left = current.left;
    //                parent.right = current.right;
    //            }

    //        }
    //        return current;

    //    }


    //    Node tem;


    //    /// <summary>
    //    /// 递归查找
    //    /// </summary>
    //    /// <param name="key"></param>
    //    public void Find(int key)
    //    {
    //        if (tem == null)
    //        {
    //            tem = root;
    //        }
    //        if (tem.data > key)
    //        {
    //            tem = tem.left;

    //            if (tem.left != null)
    //            {
    //                if (tem.left.data == key)
    //                {
    //                    Console.WriteLine(tem.left.data);
    //                    tem = tem.right;
    //                }
    //                else
    //                {
    //                    Find(key);
    //                }
    //            }
    //            else
    //            {
    //                Console.WriteLine("sdgsdgdsg");
    //            }

    //        }
    //        else if (tem.data < key)
    //        {
    //            tem = tem.right;

    //            if (tem.right != null)
    //            {
    //                if (tem.right.data == key)
    //                {
    //                    Console.WriteLine(tem.right.data);
    //                    tem = tem.right;
    //                }
    //                else
    //                {
    //                    Find(key);
    //                }
    //            }
    //            else
    //            {
    //                Console.WriteLine("sdgsdgdsg");
    //            }

    //        }

    //    }
    //}
    #endregion



    #region 平衡二叉树
    //AVL树的删除操作：
    //同插入操作一样，删除结点时也有可能破坏平衡性，这就要求我们删除的时候要进行平衡性调整。
    //删除分为以下几种情况：
    //首先在整个二叉树中搜索要删除的结点，如果没搜索到直接返回不作处理，否则执行以下操作：
    //1.要删除的节点是当前根节点T。
    //如果左右子树都非空。在高度较大的子树中实施删除操作。
    //分两种情况：
    //(1)、左子树高度大于右子树高度，将左子树中最大的那个元素赋给当前根节点，然后删除左子树中元素值最大的那个节点。
    //(1)、左子树高度小于右子树高度，将右子树中最小的那个元素赋给当前根节点，然后删除右子树中元素值最小的那个节点。
    //如果左右子树中有一个为空，那么直接用那个非空子树或者是NULL替换当前根节点即可。
    //2、要删除的节点元素值小于当前根节点T值，在左子树中进行删除。
    //递归调用，在左子树中实施删除。
    //这个是需要判断当前根节点是否仍然满足平衡条件，
    //如果满足平衡条件，只需要更新当前根节点T的高度信息。
    //否则，需要进行旋转调整：
    //如果T的左子节点的左子树的高度大于T的左子节点的右子树的高度，进行相应的单旋转。否则进行双旋转。
    //3、要删除的节点元素值大于当前根节点T值，在右子树中进行删除。
 
    #endregion


    #region 平衡二叉B树（红黑树）

    /// 红黑树定义：

    /// 性质1.节点是红色或黑色

    /// 性质2.根是黑色

    /// 性质3.所有叶子都是黑色（叶子是NIL节点）

    /// 性质4.如果一个节点是红的，则它的两个子节点都是黑的(从每个叶子到根的所有路径上不能有两个连续的红色节点)

    /// 性质5.从任一节点到其叶子的所有路径都包含相同数目的黑色节点。



    ///红黑树左右旋
    ///1.左旋转：
    ///逆时针旋转红黑树的两个节点，使得父节点被自己的右孩子取代，而自己成为自己的左孩子。说起来很怪异（父节点自身变为右孩子的左孩子）

    ///2.右旋转：
    ///顺时针旋转红黑树的两个节点，使得父节点被自己的左孩子取代，而自己成为自己的右孩子。（父节点自身变为左孩子的右孩子）

    /*
     * 插入
       红黑树的插入大体思路是，首先找到要插入的位置，插入结点，标记为红色，然后根据情况判断是否打破了性质，然后，进行修正。如果初始标记为黑色，那么就打破了每条路径上黑结点相同的这一性质，修正起来比较麻烦。
        首先，建立新结点，标记为红色，然后根据普通BST的插入方法，找到合适位置插入。令指针P指向新结点。考察如下五种情况：
        情况一：P->parent==NULL。
                   这说明当前红黑树中只有这一个结点，修改其标记为黑色即可。
        情况二，P->parent!=NULL，且P->parent是黑色的。
                   这说明不会出现连续的两个红色结点，其他性质不会因为插入一个新结点而破坏。因此，不需要做任何改变。
        情况三，P->parent!=NULL，且P->parent是红色的，且P的叔父结点也是红色的。
                   修改P->parent->parent的颜色为红色，修改P->parent->left和P->parent->right的颜色为黑色。
                   修改P=p->parent->parent，进入情况一，考察这个新的P结点，即旧的P的祖父。
        情况四，P->parent!=NULL，且P->parent是红色的，且P的叔父结点是黑色的，且P的祖父与P的关系为LR或者RL型。
                   LR型指，P的父节点在P的祖父结点的左边，P结点在P的父节点的右边。RL型刚好相反。
                   LR型可以用如下代码检查 P->value<P->parent->parent->value且p->value>P->parent->value
                   如果是LR型，对P->parent进行左旋，变成LL型，如果是RL型，对P->parent进行右旋，变成RR型。(旋转后要额外要修改新的子树的根与子树父节点的链接关系)。如果得到LL型，将新子树的左孩子，传入情况五处理。如果得到RR型，将，新子树的右孩子，传入情况五处理。
        情况五，P->parent!=NULL，且P->parent是红色的，且P的叔父是黑色的，且P的祖父与P的关系为LL或者RR型。
                   修改P->parent为黑色，修改P->parent->parent为红色，然后对于LL型，右旋P的祖父，对于RR型左旋P的祖父(旋转后要额外要修改新的子树的根与子树父节点的链接关系)。旋转后，子树的新根即为旋转前的P->parent，现在已经标记为黑色了，所以可以结束循环。  
     */


    /*删除
     * 
    1、 删除操作中真正被删除的必定只有一个红色孩子或者没有孩子的结点
    2、 如果真正删除的是一个红色节点，那么他一定是叶子节点

        在以下讨论中，我们使用蓝色箭头表示真正的删除点，它也是旋转操作过程中的第一个判定点；真正的删除点使用“旧”标注，旧点所在位置将被它的的孩子结点所取代（最多只有一个孩子），我们使用“新”表示旧点的孩子结点。
        删除操作可分为以下几种情形：
        1>旧节点为红色节点：
             直接删除
        2>一红一黑
             当旧节点为黑色节点，新节点为红色节点时，将新节点取代旧节点位置后，将新节点变为黑色。*旧点为红色，新点为黑色的情况可能不存在
        3>双黑:当旧点和新点都为黑色时（新点为空结点时，亦属于这种情况），情况比较复杂，需要根据旧点兄弟结点的颜色来决定进行什么样的操作。
            _3.1
        2>
        2>
        2>
        2>
        2>
        2>
        2>
        2>
        2>

    */




    #region 红黑树例子
    public class RedBlackTree<T>

    {

        //根节点

        private RedBlackTreeNode<T> mRoot;

        //比较器

        private Comparer<T> mComparer;

        private const bool RED = true;

        private const bool BLACK = false;



        public RedBlackTree()

        {

            mRoot = null;

            mComparer = Comparer<T>.Default;

        }



        public bool Contains(T value)

        {

            RedBlackTreeNode<T> node;

            return Contain(value, out node);

        }



        public bool Contain(T value, out RedBlackTreeNode<T> newNode)

        {

            if (value == null)

            {

                throw new ArgumentNullException();

            }

            newNode = null;

            RedBlackTreeNode<T> node = mRoot;

            while (node != null)
            {
                int comparer = mComparer.Compare(value, node.Data);

                if (comparer > 0)

                {

                    node = node.RightChild;

                }

                else if (comparer < 0)

                {

                    node = node.LeftChild;

                }

                else

                {

                    newNode = node;

                    return true;

                }

            }

            return false;

        }
        

        public void Add(T value)

        {

            if (mRoot == null)

            {

                // 根节点是黑色的

                mRoot = new RedBlackTreeNode<T>(value, BLACK);

            }

            else

            {

                // 新插入节点是红色的

                Insert1(new RedBlackTreeNode<T>(value, RED), value);

            }

        }


        private void Insert1(RedBlackTreeNode<T> newNode, T value)

        {

            //遍历找到插入位置

            RedBlackTreeNode<T> node = mRoot;

            //插入节点的父节点

            RedBlackTreeNode<T> parent = null;

            while (node != null)
            {
                parent = node;

                int comparer = mComparer.Compare(value, node.Data);

                if (comparer > 0)

                {

                    node = node.RightChild;

                }

                else if (comparer < 0)

                {

                    node = node.LeftChild;

                }

                else

                {

                    node.Data = value;

                    return;

                }

            }

            //找到插入位置，设置新插入节点的父节点为current

            newNode.Parent = parent;

            //比较插入节点的值跟插入位置的值的大小, 插入新节点

            int comparer1 = mComparer.Compare(value, parent.Data);

            if (comparer1 > 0)

            {

                parent.RightChild = newNode;

            }

            else if (comparer1 < 0)

            {

                parent.LeftChild = newNode;

            }

            //将它重新修整为一颗红黑树

            InsertFixUp(newNode);

        }



        private void InsertFixUp(RedBlackTreeNode<T> newNode)

        {

            RedBlackTreeNode<T> parent = newNode.Parent; //插入节点的父节点

            RedBlackTreeNode<T> gParent = null; //插入节点的祖父节点

            //父节点的颜色是红色,并且不为空

            while (IsRed(parent) && parent != null)

            {

                //获取祖父节点，这里不用判空，

                //因为如果祖父节点为空，parent就是根节点，根节点是黑色，不会再次进入循环

                gParent = parent.Parent;

                //若父节点是祖父节点的左子节点 

                if (parent == gParent.LeftChild)

                {

                    RedBlackTreeNode<T> uncle = gParent.RightChild; //获得叔叔节点  



                    //case1: 叔叔节点也是红色  

                    if (uncle != null && IsRed(uncle))

                    {

                        //把父节点和叔叔节点涂黑,祖父节点涂红

                        parent.Color = BLACK;

                        uncle.Color = BLACK;

                        gParent.Color = RED;

                        //把祖父节点作为插入节点，向上继续遍历

                        newNode = gParent;

                        parent = newNode.Parent;

                        continue; //继续while，重新判断  

                    }



                    //case2: 叔叔节点是黑色，且当前节点是右子节点  

                    if (newNode == parent.RightChild)

                    {

                        //从父节点处左旋

                        //当这种情况时，只能左旋，因为父亲节点和祖父节点变色，无论左旋还是右旋，都会违背红黑树的基本性质

                        RotateLeft(parent);

                        //当左旋后，红黑树变成case3的情况，区别就是插入节点是父节点

                        //所以，将父节点和插入节点调换一下，为下面右旋做准备

                        RedBlackTreeNode<T> tmp = parent;

                        parent = newNode;

                        newNode = tmp;

                    }



                    //case3: 叔叔节点是黑色，且当前节点是左子节点

                    // 父亲和祖父节点变色，从祖父节点处右旋

                    parent.Color = BLACK;

                    gParent.Color = RED;

                    RotateRight(gParent);

                }

                else

                {

                    //若父节点是祖父节点的右子节点,与上面的完全相反

                    RedBlackTreeNode<T> uncle = gParent.LeftChild;



                    //case1: 叔叔节点也是红色  

                    if (uncle != null & IsRed(uncle))

                    {

                        //把父节点和叔叔节点涂黑,祖父节点涂红

                        parent.Color = BLACK;

                        uncle.Color = BLACK;

                        gParent.Color = RED;

                        //把祖父节点作为插入节点，向上继续遍历

                        newNode = gParent;

                        parent = newNode.Parent;

                        continue;//继续while，重新判断

                    }



                    //case2: 叔叔节点是黑色的，且当前节点是左子节点  

                    if (newNode == parent.LeftChild)

                    {

                        //从父节点处右旋

                        //当这种情况时，只能右旋，因为父亲节点和祖父节点变色，无论左旋还是右旋，都会违背红黑树的基本性质

                        RotateRight(parent);

                        RedBlackTreeNode<T> tmp = parent;

                        parent = newNode;

                        newNode = tmp;

                    }



                    //case3: 叔叔节点是黑色的，且当前节点是右子节点  

                    // 父亲和祖父节点变色，从祖父节点处右旋

                    parent.Color = BLACK;

                    gParent.Color = RED;

                    RotateLeft(gParent);

                }

            }

            //将根节点设置为黑色

            mRoot.Color = BLACK;

        }



        public bool IsRed(RedBlackTreeNode<T> node)

        {

            if (node == null)

            {

                return false;

            }

            if (node.Color == RED)

            {

                return true;

            }

            return false;

        }



        public bool IsBlack(RedBlackTreeNode<T> node)

        {

            if (node == null)

            {

                return false;

            }

            if (node.Color == BLACK)

            {

                return true;

            }

            return false;

        }



        // 左旋转,逆时针旋转

        /*************对红黑树节点x进行左旋操作 ******************/

        /* 

         * 左旋示意图：对节点x进行左旋 

         *     p                       p 

         *    /                       / 

         *   x                       y 

         *  / \                     / \ 

         * lx  y      ----->       x  ry 

         *    / \                 / \ 

         *   ly ry               lx ly 

         * 左旋做了三件事： 

         * 1. 将y的左子节点赋给x的右子节点,并将x赋给y左子节点的父节点(y左子节点非空时) 

         * 2. 将x的父节点p(非空时)赋给y的父节点，同时更新p的子节点为y(左或右) 

         * 3. 将y的左子节点设为x，将x的父节点设为y 

         */

        private void RotateLeft(RedBlackTreeNode<T> x)

        {

            //1. 将y的左子节点赋给x的右子节点，并将x赋给y左子节点的父节点(y左子节点非空时)  

            RedBlackTreeNode<T> y = x.RightChild;

            x.RightChild = y.LeftChild;



            if (y.LeftChild != null)

                y.LeftChild.Parent = x;



            //2. 将x的父节点p(非空时)赋给y的父节点，同时更新p的子节点为y(左或右)  

            y.Parent = x.Parent;



            if (x.Parent == null)

            {

                mRoot = y; //如果x的父节点为空，则将y设为父节点  

            }

            else

            {

                if (x == x.Parent.LeftChild) //如果x是左子节点  

                    x.Parent.LeftChild = y; //则也将y设为左子节点  

                else

                    x.Parent.RightChild = y;//否则将y设为右子节点  

            }



            //3. 将y的左子节点设为x，将x的父节点设为y  

            y.LeftChild = x;

            x.Parent = y;

        }



        // 右旋转,顺时针旋转

        /*************对红黑树节点y进行右旋操作 ******************/

        /* 

         * 左旋示意图：对节点y进行右旋 

         *        p                   p 

         *       /                   / 

         *      y                   x 

         *     / \                 / \ 

         *    x  ry   ----->      lx  y 

         *   / \                     / \ 

         * lx  rx                   rx ry 

         * 右旋做了三件事： 

         * 1. 将x的右子节点赋给y的左子节点,并将y赋给x右子节点的父节点(x右子节点非空时) 

         * 2. 将y的父节点p(非空时)赋给x的父节点，同时更新p的子节点为x(左或右) 

         * 3. 将x的右子节点设为y，将y的父节点设为x 

         */

        private void RotateRight(RedBlackTreeNode<T> y)

        {

            //1.将x的右子节点赋值给y的左子节点，同时将y赋值给x的右子节点的父节点(如果x的右子节点非空)

            RedBlackTreeNode<T> x = y.LeftChild;

            y.LeftChild = x.RightChild;



            if (x.RightChild != null)

            {

                x.RightChild.Parent = y;

            }



            //2.如果y的父节点非空时，将y的父节点赋值给x的父节点，同时更新p的子节点为x

            if (y.Parent != null)

            {

                x.Parent = y.Parent;

            }



            if (y.Parent.LeftChild == y)

            {

                y.Parent.LeftChild = x;

            }

            else

            {

                y.Parent.RightChild = x;

            }

            //3.将x的右子节点设为y，y的父节点设置为x

            x.RightChild = y;

            y.Parent = x;

        }



        public int Count

        {

            get

            {

                return CountLeafNode(mRoot);

            }

        }



        private int CountLeafNode(RedBlackTreeNode<T> root)

        {

            if (root == null)

            {

                return 0;

            }

            else

            {

                return CountLeafNode(root.LeftChild) + CountLeafNode(root.RightChild) + 1;

            }

        }



        public int Depth

        {

            get

            {

                return GetHeight(mRoot);

            }

        }



        private int GetHeight(RedBlackTreeNode<T> root)

        {

            if (root == null)

            {

                return 0;

            }

            int leftHight = GetHeight(root.LeftChild);

            int rightHight = GetHeight(root.RightChild);

            return leftHight > rightHight ? leftHight + 1 : rightHight + 1;

        }



        public T Max

        {

            get

            {

                RedBlackTreeNode<T> node = mRoot;

                while (node.RightChild != null)

                {

                    node = node.RightChild;

                }

                return node.Data;

            }

        }



        public T Min

        {

            get

            {

                if (mRoot != null)

                {

                    RedBlackTreeNode<T> node = GetMinNode(mRoot);

                    return node.Data;

                }

                else

                {

                    return default(T);

                }

            }

        }



        public void DelMin()

        {

            mRoot = DelMin(mRoot);

        }



        private RedBlackTreeNode<T> DelMin(RedBlackTreeNode<T> node)

        {

            if (node.LeftChild == null)

            {

                return node.RightChild;

            }

            node.LeftChild = DelMin(node.LeftChild);

            return node;

        }



        public void Remove(T value)

        {

            mRoot = Delete(mRoot, value);

        }



        private RedBlackTreeNode<T> Delete(RedBlackTreeNode<T> node, T value)

        {

            if (node == null)

            {

                throw new ArgumentNullException();

            }

            int comparer = mComparer.Compare(value, node.Data);

            if (comparer > 0)

            {

                node.RightChild = Delete(node.RightChild, value);

            }

            else if (comparer < 0)

            {

                node.LeftChild = Delete(node.LeftChild, value);

            }

            else

            {

                // a.如果删除节点没有子节点，直接返回null

                // b.如果只有一个子节点，返回其子节点代替删除节点即可

                if (node.LeftChild == null)

                {

                    if (node.RightChild != null)

                    {

                        node.RightChild.Parent = node.Parent;

                    }

                    return node.RightChild;

                }

                else if (node.RightChild == null)

                {

                    if (node.LeftChild != null)

                    {

                        node.LeftChild.Parent = node.Parent;

                    }

                    return node.LeftChild;

                }

                else

                {

                    // c.被删除的节点“左右子节点都不为空”的情况  

                    RedBlackTreeNode<T> child;

                    RedBlackTreeNode<T> parent;

                    bool color;

                    // 1. 先找到“删除节点的右子树中的最小节点”，用它来取代被删除节点的位置

                    // 注意：这里也可以选择“删除节点的左子树中的最大节点”作为被删除节点的替换节点

                    RedBlackTreeNode<T> replace = node;

                    replace = GetMinNode(replace.RightChild);



                    // 2. 更新删除父节点及其子节点

                    // 要删除的节点不是根节点  

                    if (node.Parent != null)

                    {

                        // 要删除的节点是：删除节点的父节点的左子节点 

                        if (node == node.Parent.LeftChild)

                        {

                            // 把“删除节点的右子树中的最小节点”赋值给“删除节点的父节点的左子节点” 

                            node.Parent.LeftChild = replace;

                        }

                        else

                        {

                            // 把“删除节点的右子树中的最小节点”赋值给“删除节点的父节点的右子节点”

                            node.Parent.RightChild = replace;

                        }

                    }

                    else

                    {

                        // 要删除的节点是根节点

                        // 如果只有一个根节点，把mRoot赋值为null，这时replace为null

                        // 如果不止一个节点，返回根节点的右子树中的最小节点

                        mRoot = replace;

                    }



                    // 记录被删除节点的右子树中的最小节点的右子节点，父亲节点及颜色，没有左子节点

                    child = replace.RightChild;

                    parent = replace.Parent;

                    color = replace.Color;



                    // 3. 删除“被删除节点的右子树中的最小节点”，同时更新替换节点的左右子节点，父亲节点及颜色

                    // 替换节点 也就是 最小节点

                    if (parent == node)

                    {

                        // 被删除节点的右子树中的最小节点是被删除节点的子节点

                        parent = replace;

                    }

                    else

                    {

                        //如果最小节点的右子节点不为空，更新其父节点

                        if (child != null)

                        {

                            child.Parent = parent;

                        }

                        //更新最小节点的父节点的左子节点，指向最小节点的右子节点

                        parent.LeftChild = child;

                        //更新替换节点的右子节点

                        replace.RightChild = node.RightChild;

                        //更新删除节点的右子节点的父节点

                        node.RightChild.Parent = replace;

                    }

                    //更新替换节点的左右子节点，父亲节点及颜色

                    replace.Parent = node.Parent;

                    //保持原来位置的颜色

                    replace.Color = node.Color;

                    replace.LeftChild = node.LeftChild;

                    //更新删除节点的左子节点的父节点

                    node.LeftChild.Parent = replace;



                    //红黑树平衡修复

                    //如果删除的最小节点颜色是黑色，需要重新平衡红黑树

                    //如果删除的最小节点颜色是红色，只需要替换删除节点后，涂黑即可

                    //上面的保持原来位置的颜色已经处理了这种情况，这里只需要判断最小节点是黑色的情况

                    if (color == BLACK)

                    {

                        //将最小节点的child和parent传进去

                        RemoveFixUp(child, parent);

                    }

                    return replace;

                }

            }

            return node;

        }



        private void RemoveFixUp(RedBlackTreeNode<T> node, RedBlackTreeNode<T> parent)

        {

            RedBlackTreeNode<T> brother;

            // 被删除节点的右子树中的最小节点 不是 被删除节点的子节点的情况

            while ((node == null || IsBlack(node)) && (node != mRoot))

            {

                if (parent.LeftChild == node)

                {

                    //node是左子节点，下面else与这里的刚好相反  

                    brother = parent.RightChild; //node的兄弟节点  

                    if (IsRed(brother))

                    {

                        //case1: node的兄弟节点brother是红色的 

                        brother.Color = BLACK;

                        parent.Color = RED;

                        RotateLeft(parent);

                        brother = parent.RightChild;

                    }



                    //case2: node的兄弟节点brother是黑色的，且brother的两个子节点也都是黑色的

                    //继续向上遍历  

                    if ((brother.LeftChild == null || IsBlack(brother.LeftChild)) &&

                        (brother.RightChild == null || IsBlack(brother.RightChild)))

                    {

                        //把兄弟节点设置为黑色，平衡红黑树

                        brother.Color = RED;

                        node = parent;

                        parent = node.Parent;

                    }

                    else

                    {

                        //case3: node的兄弟节点brother是黑色的，且brother的左子节点是红色，右子节点是黑色  

                        if (brother.RightChild == null || IsBlack(brother.RightChild))

                        {

                            brother.LeftChild.Color = BLACK;

                            brother.Color = RED;

                            RotateRight(brother);

                            brother = parent.RightChild;

                        }



                        //case4: node的兄弟节点brother是黑色的，且brother的右子节点是红色，左子节点任意颜色  

                        brother.Color = parent.Color;

                        parent.Color = BLACK;

                        brother.RightChild.Color = BLACK;

                        RotateLeft(parent);

                        node = mRoot;

                        break;

                    }

                }

                else

                {

                    //与上面的对称  

                    brother = parent.LeftChild;

                    if (IsRed(brother))

                    {

                        // Case 1: node的兄弟brother是红色的   

                        brother.Color = BLACK;

                        parent.Color = RED;

                        RotateRight(parent);

                        brother = parent.LeftChild;

                    }

                    // Case 2: node的兄弟brother是黑色，且brother的俩个子节点都是黑色的 

                    if ((brother.LeftChild == null || IsBlack(brother.LeftChild)) &&

                        (brother.RightChild == null || IsBlack(brother.RightChild)))

                    {

                        //把兄弟节点设置为黑色，平衡红黑树

                        brother.Color = RED;

                        node = parent;

                        parent = node.Parent;

                    }

                    else

                    {

                        // Case 3: node的兄弟brother是黑色的，并且brother的左子节点是红色，右子节点为黑色。

                        if (brother.LeftChild == null || IsBlack(brother.LeftChild))

                        {

                            brother.RightChild.Color = BLACK;

                            brother.Color = RED;

                            RotateLeft(brother);

                            brother = parent.LeftChild;

                        }



                        // Case 4: node的兄弟brother是黑色的；并且brother的左子节点是红色的，右子节点任意颜色  

                        brother.Color = parent.Color;

                        parent.Color = BLACK;

                        brother.LeftChild.Color = BLACK;

                        RotateRight(parent);

                        node = mRoot;

                        break;

                    }

                }

            }

            //如果删除的最小节点的右子节点是红色，只需要替换最小节点后，涂黑即可

            if (node != null)

            {

                node.Color = BLACK;

            }

        }



        private RedBlackTreeNode<T> GetMinNode(RedBlackTreeNode<T> node)

        {

            while (node.LeftChild != null)

            {

                node = node.LeftChild;

            }

            return node;

        }



        // 中序遍历：首先遍历其左子树，然后访问根结点，最后遍历其右子树。

        // 递归方法实现体内再次调用方法本身的本质是多个方法的简写，递归一定要有出口

        public void ShowTree()

        {

            ShowTree(mRoot);

        }



        private void ShowTree(RedBlackTreeNode<T> node)

        {

            if (node == null)

            {

                return;

            }

            ShowTree(node.LeftChild);

            string nodeColor = node.Color == RED ? "red" : "black";

            string log;

            if (node.Parent != null)

            {

                log = node.Data + " " + nodeColor + " parent= " + node.Parent.Data;

            }

            else

            {

                log = node.Data + " " + nodeColor + " parent= null";

            }

            //打印节点数据

            Console.WriteLine(log);

            ShowTree(node.RightChild);

        }

    }



    public class RedBlackTreeNode<T>

    {

        //数据

        public T Data { get; set; }



        //左子节点

        public RedBlackTreeNode<T> LeftChild { get; set; }



        //右子节点

        public RedBlackTreeNode<T> RightChild { get; set; }



        //父节点

        public RedBlackTreeNode<T> Parent { get; set; }



        //该节点颜色

        public bool Color { get; set; }



        public RedBlackTreeNode(T value, bool color)

        {

            Data = value;

            LeftChild = null;

            RightChild = null;

            Color = color;

        }




    }




    #endregion




    //public class Node
    //{
    //    public Node Left;
    //    public Node Right;
    //    public Node Parent;
    //    public int Data;
    //    public bool isRed=true;

    //    public Node()
    //    { }

    //    public Node(int data,bool isred)
    //    {
    //        this.Data = data;
    //        this.isRed = isred;
    //    }
    //}


    //public class BR_Tree
    //{
    //    public Node root;

    //    public void Insert(int data)
    //    {
    //        if (root==null)
    //        {
    //            Node newNode = new Node(data,true);
    //            root = newNode;
    //        }
    //        else
    //        {
    //            InsertPos(data);
    //        }
    //    }


    //    private void InsertPos(int data)
    //    {
    //        Node newNode = new Node(data,true);
    //        Node current = root;

    //        while (true)
    //        {
    //            if (current.Data>data)
    //            {
    //                if (current.Left==null)
    //                {

    //                    current.Left = newNode;
    //                    newNode.Parent = current;
    //                    break;
    //                }
    //                else
    //                {

    //                    current = current.Left;
    //                }
    //            }
    //            else
    //            {
    //                if (current.Right == null)
    //                {
    //                    current.Right = newNode;
    //                    break;
    //                }
    //                else
    //                {

    //                    current = current.Right;
    //                }
    //            }
    //        }


    //       // Change(newNode);

    //    }

    //    private void Change(Node node)
    //    {
    //        if (node == root)
    //        {
    //            node.isRed = false;
    //        }
    //        else
    //        {

    //            if (node.Parent.isRed == false)
    //            {

    //            }
    //            else
    //            {
    //                //寻找叔叔节点
    //                Node uncle = node == node.Parent.Parent.Left ? node.Parent.Parent.Right : node.Parent.Left;

    //                if (uncle.isRed)
    //                {

    //                }
    //                else
    //                {

    //                }
    //            }

    //        }
    //    }


    //    public void Test()
    //    {
    //        Insert(30);
    //        Insert(18);
    //        Insert(60);
    //        Insert(15);
    //        Insert(21);
    //        Insert(59);
    //        Insert(70);

    //    }


    //}










    #endregion


    #endregion
    #endregion

    #region 并发

    #endregion

    #region 操作系统

    #endregion

    #region 设计模式

    #endregion

    #region 运维&统计&技术支持

    #endregion


    #region 中间件

    #endregion


    #region 网络

    #endregion


    #region 数据库

    #endregion

    #region 搜索引擎

    #endregion


    #region 性能

    #endregion

    #region 大数据

    #endregion


    #region 安全

    #endregion

    #region 常用开源框架

    #endregion


    #region 分布式设计

    #endregion

    #region 设计思想&开发模式

    #endregion


    #region 数据项目管理结构篇

    #endregion





}
