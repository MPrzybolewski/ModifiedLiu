using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModifiedLU
{
    public class Node
    {

        public Node()
        {
            Predecessors = new List<Node>();
            Successors = new List<Node>();
            PositionsInMachineLine = new List<int>();
        }
        public string Name { get; set; }
        public int ReplanDuration { get; set; }
        public List<Node> Predecessors { get; set; }
        public List<Node> Successors { get; set; }
        public bool IsStart { get; set; }
        public bool IsStop { get; set; }
        public int max { get; set; }
        public int X { get; set; }
        public int RectangleLength { get; set; }

        public List<int> PositionsInMachineLine { get; set; }
        public int P { get; set; }
        public int D { get; set; }
        public int R { get; set; }
        public int modifiedD { get; set; }
        public bool finished { get; set; }




    }
}
