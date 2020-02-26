using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA2DFA
{
    public class NFAOld
    {
        public NodeOld StartNode { get; set; }
        public List<string> Chars { get; set; } = new List<string>();
        public List<string> MoveSets { get; set; } = new List<string>();
        public NFAOld(NodeOld startnode)
        {
            StartNode = startnode;

        }

        public string NodeList2StringList(List<NodeOld> n)
        {
            return String.Join(", ", n.Select(e => e.Name));

        }

        public List<List<NodeOld>> e_closure(List<NodeOld> nodes, string c)
        {
            if (nodes == null)
            {
                return new List<List<NodeOld>>();
            }
            List<List<NodeOld>> e_closures = new List<List<NodeOld>>();
            foreach (var node in nodes)
            {
                e_closures.Add(Goto(node, c, false, false));
            }

            var tmp = e_closures.ToList();
            tmp.Clear();
            tmp.Add(e_closures.SelectMany(i => i).Distinct().ToList());
            e_closures = tmp.ToList();
            nodes = e_closures[0];
            var e_closures_eps = new List<List<NodeOld>>();
            foreach (var node in nodes)
            {
                e_closures_eps.Add(Goto(node, c, true, true));
            }

            // remove null element from e_closures_eps

            e_closures.Add(e_closures_eps.SelectMany(i => i).Distinct().Where(x => x != null).OrderBy(i => i.Name).ToList());
            if (e_closures[e_closures.Count - 1].Count == 0)
            {
                e_closures.Clear();
                return e_closures;
            }

            return e_closures.Distinct().ToList();

        }

        private class nodes_comparer : IEqualityComparer<List<NodeOld>>
        {
            public bool Equals(List<NodeOld> x, List<NodeOld> y)
            {
                if (x.Count != y.Count)
                {
                    return false;
                }
                for (int i = 0; i < x.Count; i++)
                {
                    if (x[i].Name != y[i].Name)
                    {
                        return false;
                    }
                }
                return true;
            }

            public int GetHashCode(List<NodeOld> obj)
            {
                throw new NotImplementedException();
            }
        }

        public NodeOld ToDFA(NodeOld NFA)
        {
            // Since nodes in this list (Dfa) is used by reference, 
            // It is easier to just add the nodes to the list one at the time
            // while still appending them to each edge and node. This way we don't
            // have to traverse the nodes to change them. fx. if we want to change
            // s3 we just change Dfa[3]. Instead if changing Dfa[0].EdgeL.Node.EdgeR.Node
            // You get the point :)
            // Pretty brilliant, huh :D
            List<NodeOld> Dfa = new List<NodeOld>();
            Dfa.Add(new NodeOld("s0", NodeOld.State.Initial, null, null));
            int index = 0;
            bool data_processed = false;
            var temp_nodes = new List<List<NodeOld>>();
            temp_nodes.Add(Goto(this.StartNode, ""));
            while (!data_processed)
            {
                foreach (var c in Chars) // we check each char 
                {
                    Edge edge = new Edge(new string[] { c }.ToList(), null);

                    var eps_closure = e_closure(temp_nodes[index], c);
                    if (eps_closure.Count != 0) // if an edge goes to a node  
                    {
                        if (!temp_nodes.Contains(eps_closure.Last(), new nodes_comparer()))
                        {
                            temp_nodes.Add(eps_closure.Last().ToList());
                            if (Dfa[index].LeftEdge == null) // If left child is  null
                            {
                                Dfa[index].LeftEdge = edge;
                            }
                            else
                            {
                                Dfa[index].RightEdge = edge;
                            }
                            NodeOld new_node = new NodeOld("s" + temp_nodes.Count, NodeOld.State.Normal, null, null);
                            Dfa.Add(new_node);
                            edge.Node = new_node;
                        }
                    }
                }

                index++;

                if (temp_nodes.Count <= index)
                {
                    data_processed = true;
                }
            }
            return Dfa[0];
        }
        public void MoveSet()
        {
            int index = 0;
            bool data_added = true;
            NodeOld current_node = StartNode;
            List<KeyValuePair<string, List<NodeOld>>> nodes = new List<KeyValuePair<string, List<NodeOld>>>();
            nodes.Add(new KeyValuePair<string, List<NodeOld>>("s" + nodes.Count, Goto(current_node, "")));
            MoveSets.Add(NodeList2StringList(Goto(current_node, "")));
            while (data_added)
            {

                foreach (var e in Chars)
                {

                    var l = e_closure(nodes[index].Value, e);

                    Console.WriteLine("Move(s{0}, \"{1}\") = eps-closure {{{2}}}", index, e, MoveSets[index]);

                    foreach (var set in l)
                    {
                        Console.WriteLine("              = eps-closure {{{0}}}", set.Count == 0 ? "Ø" : NodeList2StringList(set));
                    }
                    if (l.Count != 0)
                    {
                        int i = MoveSets.IndexOf(NodeList2StringList(l[l.Count - 1]));
                        if (i < 0)
                        {
                            nodes.Add(new KeyValuePair<string, List<NodeOld>>("s" + nodes.Count, l[l.Count - 1]));

                            MoveSets.Add(NodeList2StringList(l[l.Count - 1]));
                        }

                        Console.WriteLine("              = {0}", "s" + (i >= 0 ? i.ToString() : (MoveSets.Count - 1).ToString()));
                    }
                    else
                    {
                        if (nodes.Count(x => x.Value != null) == nodes.Count)
                        {
                            nodes.Add(new KeyValuePair<string, List<NodeOld>>("NULL", null));
                            MoveSets.Add("Ø");
                            Console.WriteLine("              = Ø");
                            Console.WriteLine("              = {0}", "s" + (MoveSets.Count - 1).ToString());
                        }

                    }

                }

                index++;
                if (MoveSets.Count <= index)
                    data_added = false;
            }
        }

        public string DFANodePlacement(int total, int num)
        {
            if (num == 0)
            {
                return "";
            }
            else if (num == 1)
            {
                return "[above right = of q_" + (num) + "]";
            }
            else if (num == 2)
            {
                return "[below right = of q_" + (num - 1) + "]";
            }
            else if (total % 2 == 0 && num * 2 == total)
            {
                return "[below right = of q_" + (num) + "]";
            }
            else if (num % 2 == 0)
            {
                return "[right = of q_" + (num - 2) + "]";
            }
            else
            {
                return "[right = of q_" + (num) + "]";
            }
        }

        int DFACount()
        {
            int index = 0;
            bool data_added = true;
            NodeOld current_node = StartNode;
            List<KeyValuePair<string, List<NodeOld>>> nodes = new List<KeyValuePair<string, List<NodeOld>>>();
            nodes.Add(new KeyValuePair<string, List<NodeOld>>("s" + nodes.Count, Goto(current_node, "")));
            MoveSets.Add(NodeList2StringList(Goto(current_node, "")));
            while (data_added)
            {

                foreach (var e in Chars)
                {
                    if (nodes.Count == index)
                    {
                        continue;
                    }
                    var l = e_closure(nodes[index].Value, e);
                    if (l.Count != 0)
                    {
                        nodes.Add(new KeyValuePair<string, List<NodeOld>>("s" + nodes.Count, l[l.Count - 1]));

                        int i = MoveSets.IndexOf(NodeList2StringList(l[l.Count - 1]));
                        if (i < 0)
                        {
                            MoveSets.Add(NodeList2StringList(l[l.Count - 1]));
                        }
                    }
                }

                index++;

                if (MoveSets.Count <= index)
                {
                    data_added = false;
                }

            }
            int c = MoveSets.Count;
            MoveSets.Clear();
            return c;

        }

        private bool isAcceptingNode(List<NodeOld> nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].StateOfNode == NodeOld.State.Accepting)
                    return true;
            }
            return false;
            //return nodes.Exists(n => n.Stateval == Node.State.Accepting);
        }


        public void TIKZLatex()
        {
            int total = DFACount();
            int index = 0;

            bool above = true;
            bool data_added = true;
            List<KeyValuePair<string, List<NodeOld>>> nodes = new List<KeyValuePair<string, List<NodeOld>>>();
            nodes.Add(new KeyValuePair<string, List<NodeOld>>("s" + nodes.Count, Goto(StartNode, "")));
            MoveSets.Add(NodeList2StringList(Goto(StartNode, "")));

            string node_setup = "\\documentclass{article}\n"
                              + "\\usepackage[utf8]{ inputenc}\n"
                              + "\\usepackage{ tikz}\n"
                              + "\\usetikzlibrary{ automata,positioning}\n"
                              + "\\begin{document}\n"
                              + "\\begin{tikzpicture}[shorten >=1pt,node distance=2cm,on grid,auto]\n";
            string node_path = "\\path[->]\n";
            while (data_added)
            {
                string state = nodes[index].Value == null ? "" : // if node is empty return empty string
                    isAcceptingNode(nodes[index].Value) && index == 0 ? ", accepting, initial" :  // if accepting and initial
                    (index == 0) ? ",initial" :                                                    // if initial
                    isAcceptingNode(nodes[index].Value) ? ", accepting" :                         // if accepting
                    "";
                node_setup = node_setup + string.Format("\\node[state{0}] (q_{1}) {2}  {{${3}$}}; ",
                    state,
                    index + 1,
                    DFANodePlacement(total, index),
                    "s" + index);
                node_setup += "\n";
                foreach (var e in Chars) // Go through each char ;-)
                {

                    var l = e_closure(nodes[index].Value, e);
                    if (l.Count != 0)
                    {
                        int i = MoveSets.IndexOf(NodeList2StringList(l[l.Count - 1]));
                        if (i < 0)
                        {

                            MoveSets.Add(NodeList2StringList(l[l.Count - 1]));
                            nodes.Add(new KeyValuePair<string, List<NodeOld>>("s" + nodes.Count, l[l.Count - 1]));

                        }

                        var tmp = i >= 0 ? (i + 1) : MoveSets.Count;
                        if (index + 1 == tmp)
                        {
                            above = !above;
                            node_path += string.Format("(q_{0}) edge[loop {1}]   node[above] {{{2}}} ", index + 1, above ? "above" : "below", e);
                        }
                        else
                        {
                            node_path += string.Format("(q_{0}) edge[bend left]   node[above] {{{1}}} ", index + 1, e);
                        }

                        node_path += string.Format("(q_{0})\n", (i >= 0 ? (i + 1).ToString() : (MoveSets.Count).ToString()));
                    }
                    else
                    {
                        if (nodes.Count(x => x.Value != null) == nodes.Count)
                        {
                            nodes.Add(new KeyValuePair<string, List<NodeOld>>("NULL", null));
                            MoveSets.Add("Ø");
                        }
                    }
                }

                index++;
                if (MoveSets.Count <= index)
                {
                    node_path += ";\n\\end{tikzpicture}\n\\end{document}";
                    data_added = false;
                }

            }
            Console.WriteLine(node_setup);
            Console.WriteLine(node_path);
        }
        public List<NodeOld> Goto(NodeOld nfa, string c, bool eps = true, bool include_first = true)
        {
            List<NodeOld> nodes = new List<NodeOld>();
            List<NodeOld> nodes_added = new List<NodeOld>();
            nodes.Add(nfa);
            bool data_inserted = true;
            int index = 0;
            while (data_inserted)
            {
                if (nodes[index] != null && nodes[index].LeftEdge != null && (nodes[index].LeftEdge.Value.Exists(a => a == c) || (eps && nodes[index].LeftEdge.Value.Exists(a => a == "eps"))))
                {
                    if (!nodes.Contains(nodes[index].LeftEdge.Node))
                    {
                        nodes.Add(nodes[index].LeftEdge.Node);
                    }
                    nodes_added.Add(nodes[index].LeftEdge.Node);
                }
                if (nodes[index] != null && nodes[index].RightEdge != null && (nodes[index].RightEdge.Value.Exists(a => a == c) || (eps && nodes[index].RightEdge.Value.Exists(a => a == "eps"))))
                {
                    if (!nodes.Contains(nodes[index].RightEdge.Node))
                    {
                        nodes.Add(nodes[index].RightEdge.Node);
                    }
                    nodes_added.Add(nodes[index].RightEdge.Node);
                }
                index++;
                if (nodes.Count <= index)
                    data_inserted = false;
            }
            if (include_first)
            {
                return nodes;
            }
            else
            {
                return nodes_added;
            }

        }
    }



    class Program
    {


        static string GetTextInsideParentheses(string text)
        {
            int posFirst = text.IndexOf("(");
            int posLast = text.LastIndexOf(")");
            if (posFirst >= 0 && posLast >= 0)
            {
                return text.Substring(posFirst + 1, (posLast - posFirst - 1));
            }
            else
            {
                return text;
            }
        }

        static List<NodeOld> GetAllNodes(string str)
        {
            string line;

            // Read the file and display it line by line.  
            List<NodeOld> nodes = new List<NodeOld>();
            System.IO.StreamReader file = new System.IO.StreamReader(str);

            while ((line = file.ReadLine()) != null)
            {
                var splittedText = line.Split(new string[] { "->" }, StringSplitOptions.None);

                // Node state is initial
                string firstNode = splittedText[0];
                if (firstNode.StartsWith("_"))
                {
                    AddInitialNodeToList(nodes, firstNode);
                }
                // Node state is accepting
                else if (GetTextInsideParentheses(splittedText[0]) != splittedText[0])
                {
                    AddAcceptingNodeToList(nodes, firstNode);
                }
                else // Node state is normal
                {
                    AddNormalNodeToList(nodes, firstNode);
                }
            }
            return nodes;
        }

       
        private static void AddInitialNodeToList(List<NodeOld> nodes, string nodeName)
        {
            if (!nodes.Exists(node => node.Name == nodeName.Substring(1)))
            {
                NodeOld n = new NodeOld(nodeName.Substring(1), NodeOld.State.Initial, null, null);
                nodes.Add(n);
            }
        }

        private static void AddNormalNodeToList(List<NodeOld> nodes, string nodeName)
        {
            if (!nodes.Exists(x => x.Name == nodeName))
            {
                NodeOld n = new NodeOld(nodeName, NodeOld.State.Normal, null, null);
                nodes.Add(n);
            }
        }

        private static void AddAcceptingNodeToList(List<NodeOld> nodes, string nodeName)
        {
            NodeOld n = new NodeOld(GetTextInsideParentheses(nodeName), NodeOld.State.Accepting, null, null);

            if (GetTextInsideParentheses(nodeName).StartsWith("_")) // If node is Accepting AND Initial state 
            {
                n.StateOfNode = NodeOld.State.Accepting | NodeOld.State.Initial;
            }

            nodes.Add(n);
        }

        static void Main(string[] args)
        {
            
            string path = "binary2.nfa";
            //List<NodeOld> Nodes = GetAllNodes(path);

            NFAFileReader fileReader = new NFAFileReader(path);
            fileReader.ConnectNodes();



            var nfa = new NFAOld(fileReader.FirstNode);
            nfa.Chars.Add("0");
            nfa.Chars.Add("1");
            nfa.MoveSet();
            Console.ReadKey();
            var dfa = nfa.ToDFA(null);
            nfa.TIKZLatex();
            Console.ReadKey();
        }
    }
}