using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA2DFA
{
    public class Edge
    {
        public Node Node { get; set; }
        public List<string> Value { get; set; }
        public Edge(List<string> value, Node node)
        {
            Value = value;
            Node = node;
        }
    }
}
