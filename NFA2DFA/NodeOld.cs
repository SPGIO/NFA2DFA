using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA2DFA
{
    public class NodeOld
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

        public NodeOld(string name, State stateOfNode, Edge leftEdge, Edge rightEdge)
        {
            Name = name;
            StateOfNode = stateOfNode;
            LeftEdge = leftEdge;
            RightEdge = rightEdge;
        }
        public Edge LeftEdge { get; set; }
        public Edge RightEdge { get; set; }
    }

    public class Node
    {
       
        public string Name { get; set; }

        public Node(string name, Edge leftEdge, Edge rightEdge)
        {
            Name = name;
            LeftEdge = leftEdge;
            RightEdge = rightEdge;
        }

        public Node(string name) : this(name, null, null)
        {

        }

        public Edge LeftEdge { get; set; }
        public Edge RightEdge { get; set; }
    }

    public class AcceptingNode : Node
    {
        public AcceptingNode(string name) : base(name) { }
        public AcceptingNode(string name, Edge leftEdge, Edge rightEdge) : base(name, leftEdge, rightEdge)
        {
        }
    }

    public class InitialAndAcceptingNode : Node
    {
        public InitialAndAcceptingNode(string name) : base(name) { }
        public InitialAndAcceptingNode(string name, Edge leftEdge, Edge rightEdge) : base(name, leftEdge, rightEdge)
        {
        }
    }

    public class NormalNode : Node
    {
        public NormalNode(string name) : base(name) { }
        public NormalNode(string name, Edge leftEdge, Edge rightEdge) : base(name, leftEdge, rightEdge)
        {
        }
    }

    public class InitialNode : Node
    {
        public InitialNode(string name) : base(name) { }
        public InitialNode(string name, Edge leftEdge, Edge rightEdge) : base(name, leftEdge, rightEdge)
        {
        }
    }
}
