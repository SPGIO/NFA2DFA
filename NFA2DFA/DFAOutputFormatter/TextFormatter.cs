using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA2DFA.DFAOutputFormatter
{
    class TextFormatter : IDFAOutputFormatter
    {
        public string Output()
        {
            throw new NotImplementedException();
            //// Needs a startnode change later
            //NodeOld StartNode = null;

            //int index = 0;
            //bool data_added = true;
            //NodeOld current_node = StartNode;
            //List<KeyValuePair<string, List<NodeOld>>> nodes = new List<KeyValuePair<string, List<NodeOld>>>();
            //nodes.Add(new KeyValuePair<string, List<NodeOld>>("s" + nodes.Count, Goto(current_node, "")));
            //MoveSets.Add(NodeList2StringList(Goto(current_node, "")));
            //while (data_added)
            //{

            //    foreach (var e in Chars)
            //    {

            //        var l = e_closure(nodes[index].Value, e);

            //        Console.WriteLine("Move(s{0}, \"{1}\") = eps-closure {{{2}}}", index, e, MoveSets[index]);

            //        foreach (var set in l)
            //        {
            //            Console.WriteLine("              = eps-closure {{{0}}}", set.Count == 0 ? "Ø" : NodeList2StringList(set));
            //        }
            //        if (l.Count != 0)
            //        {
            //            int i = MoveSets.IndexOf(NodeList2StringList(l[l.Count - 1]));
            //            if (i < 0)
            //            {
            //                nodes.Add(new KeyValuePair<string, List<NodeOld>>("s" + nodes.Count, l[l.Count - 1]));

            //                MoveSets.Add(NodeList2StringList(l[l.Count - 1]));
            //            }

            //            Console.WriteLine("              = {0}", "s" + (i >= 0 ? i.ToString() : (MoveSets.Count - 1).ToString()));
            //        }
            //        else
            //        {
            //            if (nodes.Count(x => x.Value != null) == nodes.Count)
            //            {
            //                nodes.Add(new KeyValuePair<string, List<NodeOld>>("NULL", null));
            //                MoveSets.Add("Ø");
            //                Console.WriteLine("              = Ø");
            //                Console.WriteLine("              = {0}", "s" + (MoveSets.Count - 1).ToString());
            //            }

            //        }

            //    }

            //    index++;
            //    if (MoveSets.Count <= index)
            //        data_added = false;
            //}
        }
    }
}
