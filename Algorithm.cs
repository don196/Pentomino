using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentomino
{
    public class Algorithm
    {
        public Head Root = new Head(-1);
        public List<int> solution = new List<int>();
        MainForm.Worker worker;
        int bestSolution;
        int deletedColumns = 0;
        List<int> bestSolutionList = new List<int>();

        public Algorithm(MainForm.Worker worker)
        {
            this.worker = worker;
        }


        public void Restruct(List<List<int>> structure, int size)
        {
            for (int i = 0; i < size; i++)
            {
                Head head = new Head(i);
                Root.AddHead(head);
                bool first = true;
                Node lastNode = new Node(head, -1);
                for (int j = structure.Count - 1; j >= 0; j--)
                {
                    if(structure[j].Contains(i))
                        {
                            Node node = new Node(head, j);
                            if (first)
                            {
                                head.AddNode(node);
                                lastNode = node;
                                first = false;
                            }
                            else
                            {
                                lastNode.AddToColumn(node);
                            }
                            lastNode = node;
                        }
                }
            }
            for (int i = 0; i < structure.Count; i++)
            {
                Head chosenHead = Root.RightHead;
                while (chosenHead.ColumnNumber != structure[i][0]) chosenHead = chosenHead.RightHead;
                Node chosenNode = chosenHead.Down;
                while (chosenNode.RowNumber != i) chosenNode = chosenNode.Down;
                for (int j = 0; j < structure[i].Count - 1; j++)
                {
                    chosenHead = chosenHead.RightHead;
                    while (chosenHead.ColumnNumber != structure[i][j + 1]) chosenHead = chosenHead.RightHead;
                    Node tempNode = chosenHead.Down;
                    while (tempNode.RowNumber != i) tempNode = tempNode.Down;
                    chosenNode.AddToRow(tempNode);
                    chosenNode = tempNode;
                }
            }
            bestSolution = size;
        }


        public void AddToSolution(int i)
        {
            solution.Add(i);
        }


        public void RemoveFromSolution(int i)
        {
            solution.Remove(i);
        }


        public void Step()
        {
            if (Root.RightHead.Equals(Root))
            {
                worker.SolutionFound(this);
            }
            else
            {
                Head chosenHead = ChooseHead();
                if (chosenHead.RowCount != 0)
                {
                    if(worker.token.IsCancellationRequested)
                    {
                        worker.SolutionFound();
                    }
                    Node chosenNode = chosenHead.Down;
                    while (!chosenNode.Equals(chosenHead))
                    {
                        deleteColumn(chosenNode);
                        Step();
                        returnColumn(chosenNode);
                        chosenNode = chosenNode.Down;
                    }
                }
            }
        }


        public void StartMultiSteps()
        {
            MultiStep();
            solution = bestSolutionList;
            worker.SolutionFound(this);
        }


        public void MultiStep()
        {
            if (Root.RightHead.Equals(Root))
            {
                if (deletedColumns < bestSolution)
                {
                    bestSolution = deletedColumns;
                    bestSolutionList = new List<int>();
                    for (int i = 0; i < solution.Count; i++)
                    {
                        bestSolutionList.Add(solution[i]);
                    }
                }
            }
            else
            {
                Head chosenHead = Root.RightHead;
                int emptyCols = 0;
                while (chosenHead.Equals(Root))
                {
                    if (chosenHead.RowCount == 0) emptyCols++;
                    chosenHead = chosenHead.RightHead;
                }
                if(emptyCols + deletedColumns < bestSolution)
                {
                chosenHead = ChooseHead();
                List<Head> deletedColumnsList = new List<Head>();
                while (!chosenHead.Equals(Root))
                {
                    if (worker.token.IsCancellationRequested)
                    {
                        solution = bestSolutionList;
                        worker.SolutionFound(this);
                    }
                    if (chosenHead.RowCount != 0)
                    {
                        Node chosenNode = chosenHead.Down;
                        while (!chosenNode.Equals(chosenHead))
                        {
                            deleteColumn(chosenNode);
                            Head tempHead = Root.RightHead;
                            emptyCols = 0;
                            int heads = 0;
                            while (!tempHead.Equals(Root))
                            {
                                if (tempHead.RowCount == 0) emptyCols++;
                                heads++;
                                tempHead = tempHead.RightHead;
                            }
                            if(bestSolution > heads + deletedColumns)
                            {
                                bestSolution = heads + deletedColumns;
                                bestSolutionList = new List<int>();
                                for(int i = 0; i < solution.Count; i++)
                                {
                                    bestSolutionList.Add(solution[i]);
                                }
                            }
                            MultiStep();
                            returnColumn(chosenNode);
                            chosenNode = chosenNode.Down;
                        }
                    }
                    if (!chosenHead.Equals(Root))
                    {
                        deletedColumns++;
                        deletedColumnsList.Add(chosenHead);
                        deleteColumn(chosenHead, true);
                        chosenHead = ChooseHead();
                    }
                }
                for (int i = deletedColumnsList.Count - 1; i >= 0; i--)
                    {
                        deletedColumns--;
                        returnColumn(deletedColumnsList[i], true);
                        deletedColumnsList.RemoveAt(i);
                    }
                }
            }
        }


        public void deleteColumn(Node chosenNode, bool isModDel = false)
        {
            Node nodeInRow = chosenNode;
            if(!isModDel)
                AddToSolution(chosenNode.RowNumber);
            do
            {
                nodeInRow.Header.DeleteHead();
                Node nodeInColumn = nodeInRow.Down;
                while (!nodeInColumn.Equals(nodeInRow))
                {
                    Node tempNode = nodeInColumn.Right;
                    while (!tempNode.Equals(nodeInColumn))
                    {
                        tempNode.DeleteFromColumn();
                        tempNode = tempNode.Right;
                    }
                    nodeInColumn = nodeInColumn.Down;
                }
                nodeInRow = nodeInRow.Right;
            } while (!nodeInRow.Equals(chosenNode));
        }


        public void returnColumn(Node chosenNode, bool isModDel = false)
        {
            Node nodeInRow = chosenNode.Left;
            if (!isModDel)
                RemoveFromSolution(chosenNode.RowNumber);
            do
            {
                nodeInRow.Header.ReturnHead();
                Node nodeInColumn = nodeInRow.Up;
                while (!nodeInColumn.Equals(nodeInRow))
                {
                    Node tempNode = nodeInColumn.Left;
                    while (!tempNode.Equals(nodeInColumn))
                    {
                        tempNode.ReturnToColumn();
                        tempNode = tempNode.Left;
                    }
                    nodeInColumn = nodeInColumn.Up;
                }
                nodeInRow = nodeInRow.Left;
            } while (!nodeInRow.Equals(chosenNode.Left));
        }


        public Head ChooseHead()
        {
            Head chosenHead = null;
            Head head = Root.RightHead;
            while (!head.Equals(Root))
            {
                if (chosenHead == null || head.RowCount < chosenHead.RowCount)
                {
                    chosenHead = head;
                }
                head = head.RightHead;
            }
            if (chosenHead == null) chosenHead = head;
            return chosenHead;
        }
    }


    public class Node
    {
        public Node(Head head, int row)
        {
            Left = Right = Up = Down = this;
            Header = head;
            RowNumber = row;
        }


        protected Node()
        {
            Left = Right = Up = Down = this;
            RowNumber = -1;
        }


        public Node Left { get; private set; }
        public Node Right { get; private set; }
        public Node Up { get; private set; }
        public Node Down { get; private set; }
        public Head Header { get; set; }
        public int RowNumber { get; private set; }


        public void AddToColumn(Node node)
        {
            Up.Down = node;
            node.Down = this;
            node.Up = Up;
            Up = node;
            this.Header.RowCount++;
        }


        public void AddToRow(Node node)
        {
            Right.Left = node;
            node.Left = this;
            node.Right = Right;
            Right = node;
        }


        public void DeleteFromColumn()
        {
            Down.Up = Up;
            Up.Down = Down;
            this.Header.RowCount--;
        }


        public void ReturnToColumn()
        {
            Down.Up = this;
            Up.Down = this;
            this.Header.RowCount++;
        }
    }

    public class Head : Node
    {
        public Head(int column)
        {
            LeftHead = RightHead = this;
            ColumnNumber = column;
            RowCount = 0;
            Header = this;
        }


        public Head LeftHead { get; private set; }
        public Head RightHead { get; private set; }
        public int RowCount { get; set; }
        public int ColumnNumber { get; private set; }


        public void AddHead(Head head)
        {
            LeftHead.RightHead = head;
            head.RightHead = this;
            head.LeftHead = LeftHead;
            LeftHead = head;
        }


        public void DeleteHead()
        {
            LeftHead.RightHead = RightHead;
            RightHead.LeftHead = LeftHead;
        }


        public void ReturnHead()
        {
            LeftHead.RightHead = this;
            RightHead.LeftHead = this;
        }


        public void AddNode(Node node)
        {
            AddToColumn(node);
            RowCount++;
        }
    }
}
