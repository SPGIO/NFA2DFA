using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA2DFA
{

    static class StringManipulator
    {
        public static string[] RemoveEmptyEntries(this string[] list)
        {
            return list.Where(element => !string.IsNullOrEmpty(element)).ToArray();
        }

        public static string[] Lines(this string text)
        {
            text = text.Replace("\r", "");
            string[] lines = text.Split('\n');
            return lines.RemoveEmptyEntries();
        }

        public static bool HasParentheses(this string text)
        {
            int indexOfLeftParentheses = text.IndexOf("(");
            int indexOfRightParentheses= text.LastIndexOf(")");
            bool hasLeftParentheses =  indexOfLeftParentheses >= 0;
            bool hasRightParentheses = indexOfRightParentheses >= 0;
            return hasLeftParentheses && hasRightParentheses;
        }

        public static string GetTextInsideParentheses(this string text)
        {
            int posFirst = text.IndexOf("(");
            int posLast = text.LastIndexOf(")");
            if (posFirst < 0 && posLast < 0)
            {
                return string.Empty;
            }
            return text.Substring(posFirst + 1, (posLast - posFirst - 1));
        }

        public static string StripTextOfParenthesesAndUnderscore(this string name)
        {
            string strippedName = name;
            if (HasParentheses(name))
                strippedName = GetTextInsideParentheses(name);
            return strippedName.TrimStart('_');
        }
    }


    class NFAFileReader
    {

        private string[] FileContent;



        private string[] ReadFile(string path)
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader(path))
            {
                string content =  file.ReadToEnd();
                return content.Lines();
            }
        }

        public NFAFileReader(string path)
        {
            FileContent = ReadFile(path);
        }





        private static Edge CreateEdge(List<NodeOld> nodes, NodeOld destinationNode, string product)
        {
            Edge edge = new Edge(product.Split('|').ToList(), destinationNode);
            return edge;
        }
        private void UpdateEdgeOnSourceNode(List<NodeOld> nodes, NodeOld sourceNode, Edge edge)
        {
            if (sourceNode.LeftEdge == null)
                sourceNode.LeftEdge = edge;
            else sourceNode.RightEdge = edge;

        }
        private void CreateAndUpdateEdgeOnSourceNode(List<NodeOld> nodes, NodeOld sourceNode, NodeOld destinationNode, string product)
        {
            Edge edge = CreateEdge(nodes, destinationNode, product);
            UpdateEdgeOnSourceNode(nodes, sourceNode, edge);
        }

        private NodeOld GetNodeByName(List<NodeOld> nodes, string name)
        {
            string strippedName = name.StripTextOfParenthesesAndUnderscore();
            NodeOld nodeFound = nodes.Find(x => x.Name == strippedName);
            return nodeFound;

        }
        public void ConnectNodes(List<NodeOld> nodes)
        {
            foreach (string line in FileContent)
            {
                var splittedText = line.Split(new string[] { "->" }, StringSplitOptions.None);
                if (splittedText.Count() == 1) { continue; }
                string product = splittedText[1];
                NodeOld sourceNode = GetNodeByName(nodes, splittedText[0]);
                NodeOld destinationNode = GetNodeByName(nodes, splittedText[2]);
                CreateAndUpdateEdgeOnSourceNode(nodes, sourceNode, destinationNode, product);
            }
        }



        private void AddInitialNodeToList(List<NodeOld> nodes, string nodeName)
        {
            if (!nodes.Exists(node => node.Name == nodeName.Substring(1)))
            {
                NodeOld n = new NodeOld(nodeName.Substring(1), NodeOld.State.Initial, null, null);
                nodes.Add(n);
            }
        }

        private void AddNormalNodeToList(List<NodeOld> nodes, string nodeName)
        {
            if (!nodes.Exists(x => x.Name == nodeName))
            {
                NodeOld n = new NodeOld(nodeName, NodeOld.State.Normal, null, null);
                nodes.Add(n);
            }
        }

        private void AddAcceptingNodeToList(List<NodeOld> nodes, string nodeName)
        {
            NodeOld n = new NodeOld(nodeName.GetTextInsideParentheses(), NodeOld.State.Accepting, null, null);

            if (nodeName.GetTextInsideParentheses().StartsWith("_")) // If node is Accepting AND Initial state 
            {
                n.StateOfNode = NodeOld.State.Accepting | NodeOld.State.Initial;
            }

            nodes.Add(n);
        }

        private List<NodeOld> GetAllNodesInFile()
        {
            List<NodeOld> nodes = new List<NodeOld>();

            foreach (string line in FileContent)
            {
                var splittedText = line.Split(new string[] { "->" }, StringSplitOptions.None);

                // Node state is initial
                string firstNode = splittedText[0];
                if (firstNode.StartsWith("_"))
                {
                    AddInitialNodeToList(nodes, firstNode);
                }
                // Node state is accepting
                else if (splittedText[0].GetTextInsideParentheses() != splittedText[0])
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
    }
}
