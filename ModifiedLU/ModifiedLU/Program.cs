using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModifiedLU
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ListOfNodes = ReadInputData();
            ComputeLU();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static List<Node> ListOfNodes;


        private static void ComputeLU()
        {
            ComputeModifiedEndDates();
            ComputePositionForNodesInMachineLine();
        }

        private static void ComputePositionForNodesInMachineLine()
        {
            List<Node> activeTasks = new List<Node>();
            int timeNumber = 0;
            while(ListOfNodes.FirstOrDefault<Node>(s => s.finished == false) != null)
            {
                activeTasks = new List<Node>();
                SetActiveTasks(activeTasks, timeNumber);
                Node nodeWithSmallestModifiedD = GetNodeWithSmallestModifiedD(activeTasks);
                if(nodeWithSmallestModifiedD != null)
                {
                    nodeWithSmallestModifiedD.PositionsInMachineLine.Add(timeNumber);
                    if (nodeWithSmallestModifiedD.PositionsInMachineLine.Count == nodeWithSmallestModifiedD.P)
                    {
                        nodeWithSmallestModifiedD.finished = true;
                    }
                }
               
                timeNumber++;
            }
        }

        private static Node GetNodeWithSmallestModifiedD(List<Node> activeTasks)
        {
            Node nodeWithSmallestModfiedD = null;
            foreach(Node node in activeTasks)
            {
                if(nodeWithSmallestModfiedD == null)
                {
                    nodeWithSmallestModfiedD = node;
                }
                else
                {
                    if(nodeWithSmallestModfiedD.modifiedD > node.modifiedD)
                    {
                        nodeWithSmallestModfiedD = node;
                    }
                }
            }

            return nodeWithSmallestModfiedD;
        }

        private static void SetActiveTasks(List<Node> activeTask, int timeNumber)
        {
            foreach (Node node in ListOfNodes)
            {
                if (node.R <= timeNumber)
                {
                    if (!node.finished)
                    {
                        if(IsNodePredecessorsFinished(node.Predecessors))
                        {
                            activeTask.Add(node);
                        }
                    }
                }
            }
        }

        private static bool IsNodePredecessorsFinished(List<Node> predecessors)
        {
            foreach(Node node in predecessors)
            {
                if (node.finished == false)
                    return false;
            }

            return true;
        }

        private static void ComputeModifiedEndDates()
        {
            foreach(Node node in ListOfNodes)
            {
                node.modifiedD = ComputeSmallestEndDateValueFromNodeAndNodesSuccessors(node, node.D);
            }
        }

        private static int ComputeSmallestEndDateValueFromNodeAndNodesSuccessors(Node node, int smallestEndDate)
        {

            if(smallestEndDate > node.D)
            {
                smallestEndDate = node.D;
            }

            if(node.IsStop)
            {
                return smallestEndDate;
            }

            foreach(Node succesor in node.Successors)
            {
                return ComputeSmallestEndDateValueFromNodeAndNodesSuccessors(succesor, smallestEndDate);
            }

            return -1;
        }

        private static List<Node> ReadInputData()
        {
            List<Node> listOfNodes = new List<Node>();

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("\\Users\\Marek\\source\\repos\\ModifiedLU\\ModifiedLU\\data.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    sr.ReadLine();
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] splitLine = line.Split('|');

                        Node tempNode = new Node();
                        tempNode.Name = splitLine[0];
                        listOfNodes.Add(tempNode);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("\\Users\\Marek\\source\\repos\\ModifiedLU\\ModifiedLU\\data.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    sr.ReadLine();
                    int i = 0;
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] splitLine = line.Split('|');

                        string[] predecessorNames = splitLine[1].Split(',');
                        if (predecessorNames[0][0] != ' ')
                        {
                            foreach (string activityName in predecessorNames)
                            {
                                listOfNodes[i].Predecessors.Add(listOfNodes.First<Node>(s => s.Name == activityName));
                            }
                        }

                        string[] succesorsNames = splitLine[2].Split(',');
                        if (succesorsNames[0][0] != ' ')
                        {
                            foreach (string activityName in succesorsNames)
                            {
                                listOfNodes[i].Successors.Add(listOfNodes.First<Node>(s => s.Name == activityName));
                            }
                        }
                        else
                        {
                            listOfNodes[i].IsStop = true;
                        }
                        listOfNodes[i].P = Int32.Parse(splitLine[3]);
                        listOfNodes[i].D = Int32.Parse(splitLine[4]);
                        listOfNodes[i].R = Int32.Parse(splitLine[5]);

                        i++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return listOfNodes;
        }
    }
}
