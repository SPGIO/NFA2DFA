using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA2DFA.DFAOutputFormatter
{
    class TikzFormatter
    {
        public string Output()
        {
            throw new NotImplementedException();
            //    int total = DFACount();
            //    int index = 0;

            //    bool above = true;
            //    bool data_added = true;
            //    List<KeyValuePair<string, List<NodeOld>>> nodes = new List<KeyValuePair<string, List<NodeOld>>>();
            //    nodes.Add(new KeyValuePair<string, List<NodeOld>>("s" + nodes.Count, Goto(StartNode, "")));
            //    MoveSets.Add(NodeList2StringList(Goto(StartNode, "")));

            //    string node_setup = "\\documentclass{article}\n"
            //                      + "\\usepackage[utf8]{ inputenc}\n"
            //                      + "\\usepackage{ tikz}\n"
            //                      + "\\usetikzlibrary{ automata,positioning}\n"
            //                      + "\\begin{document}\n"
            //                      + "\\begin{tikzpicture}[shorten >=1pt,node distance=2cm,on grid,auto]\n";
            //    string node_path = "\\path[->]\n";
            //    while (data_added)
            //    {
            //        string state = nodes[index].Value == null ? "" : // if node is empty return empty string
            //            isAcceptingNode(nodes[index].Value) && index == 0 ? ", accepting, initial" :  // if accepting and initial
            //            (index == 0) ? ",initial" :                                                    // if initial
            //            isAcceptingNode(nodes[index].Value) ? ", accepting" :                         // if accepting
            //            "";
            //        node_setup = node_setup + string.Format("\\node[state{0}] (q_{1}) {2}  {{${3}$}}; ",
            //            state,
            //            index + 1,
            //            DFANodePlacement(total, index),
            //            "s" + index);
            //        node_setup += "\n";
            //        foreach (var e in Chars) // Go through each char ;-)
            //        {

            //            var l = e_closure(nodes[index].Value, e);
            //            if (l.Count != 0)
            //            {
            //                int i = MoveSets.IndexOf(NodeList2StringList(l[l.Count - 1]));
            //                if (i < 0)
            //                {

            //                    MoveSets.Add(NodeList2StringList(l[l.Count - 1]));
            //                    nodes.Add(new KeyValuePair<string, List<NodeOld>>("s" + nodes.Count, l[l.Count - 1]));

            //                }

            //                var tmp = i >= 0 ? (i + 1) : MoveSets.Count;
            //                if (index + 1 == tmp)
            //                {
            //                    above = !above;
            //                    node_path += string.Format("(q_{0}) edge[loop {1}]   node[above] {{{2}}} ", index + 1, above ? "above" : "below", e);
            //                }
            //                else
            //                {
            //                    node_path += string.Format("(q_{0}) edge[bend left]   node[above] {{{1}}} ", index + 1, e);
            //                }

            //                node_path += string.Format("(q_{0})\n", (i >= 0 ? (i + 1).ToString() : (MoveSets.Count).ToString()));
            //            }
            //            else
            //            {
            //                if (nodes.Count(x => x.Value != null) == nodes.Count)
            //                {
            //                    nodes.Add(new KeyValuePair<string, List<NodeOld>>("NULL", null));
            //                    MoveSets.Add("Ø");
            //                }
            //            }
            //        }

            //        index++;
            //        if (MoveSets.Count <= index)
            //        {
            //            node_path += ";\n\\end{tikzpicture}\n\\end{document}";
            //            data_added = false;
            //        }

            //    }
            //    string.Format("{0}\n{1}", node_setup, node_path);
            //}
        }
    }
}
