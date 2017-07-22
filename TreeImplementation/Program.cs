using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            Node root = InitializeTree();
            Console.WriteLine("All nodes");
            PrintTree(root);
            Console.WriteLine("\nLeaf Nodes: ");
            PrintLeafNodes(root);
            Console.WriteLine("\nInternal nodes: ");
            PrintInternalNodes(root);
            Console.WriteLine("\nNumber of leaf nodes:" + CountLeafNodes(root));
            Console.WriteLine("Number of internal nodes:" + CountInternalNodes(root));
            Console.WriteLine("Height of the tree is: " + GetHeight(root));
            Console.WriteLine("Is Strict :" + IsStrict(root));
            Console.WriteLine("\nInorder Successor of  : ");
            int value =Convert.ToInt16(Console.ReadLine());
            InorderSuccessesor(root, value);
            Console.WriteLine("\nInorder Predecessor : ");
            InorderPredecessor(root, value);
            Console.WriteLine("\nLevel order traversal ");
            LevelOrderTraversal(root);
            Console.WriteLine("\nPrint all Predecessors ");
            PrintAllPredecessors(root, value);
            Console.Read();

        }

        class Node
        {
            public Node(int value,Node parent=null)
            {
                Value = value;
                Parent = parent;
            }
            public int Value;
            public Node Left;
            public Node Right;
            public Node Parent;

        }

        private static Node InitializeTree()
        {
            Node root = new Node(7);
            root.Left = new Node(4,root);
            root.Right = new Node(10,root);
            root.Left.Left = new Node(2, root.Left);
            root.Left.Right = new Node(5, root.Left);
            root.Right.Left = new Node(8, root.Right);
            root.Right.Right = new Node(11, root.Right);
            root.Left.Left.Left = new Node(1, root.Left.Left);
            root.Left.Left.Right = new Node(3, root.Left.Left);
            root.Left.Right.Right = new Node(6, root.Left.Right);

            root.Right.Right.Right = new Node(12, root.Right.Right);
            root.Right.Left.Right = new Node(9, root.Right.Left);
            //root.Right.Right.Right.Right = new Node(13);
            return root;
        }

        private static void PrintTree(Node root)
        {
            if (root == null)
                return;           
            PrintTree(root.Left);
            Console.Write(root.Value + ",");
            PrintTree(root.Right);
        }
        private static void PrintLeafNodes(Node root)
        {
            if (root == null)
            {
                return;
            }
            if (root.Left == null && root.Right == null)
            {
                Console.Write(root.Value + ",");
            }

            PrintLeafNodes(root.Left);
            PrintLeafNodes(root.Right);
        }

        private static void PrintInternalNodes(Node root)
        {
            if (root == null)
                return;
            else
            {
                if (root.Left != null || root.Right != null)
                {
                    Console.Write(root.Value + ",");
                }

                PrintInternalNodes(root.Left);
                PrintInternalNodes(root.Right);
            }
        }

        private static int CountLeafNodes(Node root)
        {
            if (root == null)
            {
                return 0;
            }
            else if (root.Left == null && root.Right == null)
            {
                return 1;
            }
            else
            {
                int a = CountLeafNodes(root.Left);
                int b = CountLeafNodes(root.Right);
                return (a + b);
            }

        }

        private static int CountInternalNodes(Node root)
        {
            if (root == null)
            {
                return 0;
            }
            else
            {
                if (root.Left == null && root.Right == null)
                {
                    return 0;
                }

                else
                {
                    int a = CountInternalNodes(root.Left);
                    int b = CountInternalNodes(root.Right);
                    return (1 + a + b);
                }

            }
        }

        private static int GetHeight(Node root)
        {
            if (root == null)
                return 0;
            else if (root.Left == null && root.Right == null)
            {
                return 0;
            }

            else
            {
                int a = GetHeight(root.Left) + 1;
                int b = GetHeight(root.Right) + 1;
                if (a > b)
                    return a;
                else
                    return b;
            }
        }

        private static bool IsStrict(Node root)
        {
            if (root == null)
                return true;

            if (root.Left == null && root.Right == null)
            {
                return true;
            }

            else if (root.Left != null && root.Right != null)
            {
                return IsStrict(root.Left) && IsStrict(root.Right);
            }
            else return false;

        }

        private static void InorderSuccessesor(Node root,int value)
        {
            if (root == null)
                return;
            else if(root.Value==value)
            {
                if(root.Right!=null)
                {
                    Console.WriteLine(GetMinimum(root.Right).Value);
                }
                //else if(root.Left==null && root.Right==null)
                //{
                //    if(root.Parent.Left==root)
                //    {
                //        Console.WriteLine(root.Parent.Value);
                //    }
                //}

                else
                {
                    Node temp = root.Parent;
                    while(temp.Parent!=null && temp.Parent.Left != temp)
                    {
                        temp = temp.Parent;
                    }
                    Console.WriteLine(temp.Parent.Value);                    
                }
               
            }
            InorderSuccessesor(root.Left,value);
            InorderSuccessesor(root.Right, value);

        }

        private static Node GetMinimum(Node root)
        {
            if (root == null)
                return root;
            if (root.Left == null)
                return root;
            else
                return GetMinimum(root.Left);
        }

        private static Node GetMax(Node root)
        {
            if (root == null)
                return null;
            if (root.Right==null)
            {
                return root;
            }
            else
            {
               return GetMax(root.Right);
            }

        }

        private static void InorderPredecessor(Node root ,int value)
        {
            if (root == null)
                return;
            if(root.Value==value)
            {
                if(root.Left!=null)
                {
                    Console.Write(GetMax(root.Left).Value);
                }
                else
                {
                    if(root.Parent!=null && root.Parent.Right==root)
                    {
                        Console.Write(root.Parent.Value);
                    }
                    else
                    {
                        Console.WriteLine("No predecessor");
                    }
                }

            }
            InorderPredecessor(root.Left, value);
            InorderPredecessor(root.Right, value);
        }

        private static void LevelOrderTraversal(Node root)
        {
            for(int i=1;i<=GetHeight(root)+1;i++)
            {
                PrintLevelOrder(root, i);
            }
        }
        private static void PrintLevelOrder(Node root,int level )
        {
            if (root == null)
                return;
            if(level==1)
            {
                Console.Write(root.Value+",");
            }
            else if(level>1)
            {
                PrintLevelOrder(root.Left, level - 1);
                PrintLevelOrder(root.Right, level - 1);
            }
        }

        private static bool PrintAllPredecessors(Node root,int value)
        {
            if (root == null)
                return false;
            if(root.Value==value)
            {
                return true;
            }

            if (PrintAllPredecessors(root.Left, value)|| PrintAllPredecessors(root.Right, value))
                Console.WriteLine(root.Value + ",");
            return false;
        }
    }
}
