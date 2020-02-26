using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA2DFA
{
    public class Node
    {
        [Flags]
        public enum State
        {
            Accepting = 1,
            Initial = 2,
            Normal = 4
        }

        public State StateOfNode { get; set; }

        public string Name { get; set; }

        public Node(string name, State stateOfNode, Edge leftEdge, Edge rightEdge)
        {
            Name = name;
            StateOfNode = stateOfNode;
            LeftEdge = leftEdge;
            RightEdge = rightEdge;
        }
        public Edge LeftEdge { get; set; }
        public Edge RightEdge { get; set; }
    }
}
