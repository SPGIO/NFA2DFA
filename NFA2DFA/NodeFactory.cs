using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFA2DFA
{
    public static class NodeFactory
    {

        private static bool isInitialNode(string name)
        {
            return name.StartsWith("_");
        }

        private static bool isAcceptingNode(string name)
        {
            return name.HasParentheses();
        }

        private static bool isInitialAndAcceptingNode(string name)
        {
            return isInitialNode(name.GetTextInsideParentheses());
        }

        public static Node CreateNode(string name)
        {
            string strippedName = name.StripTextOfParenthesesAndUnderscore();
            if (isAcceptingNode(name)) // Node is accepting
                return new AcceptingNode(strippedName);

            if (isInitialAndAcceptingNode(name))
                return new InitialAndAcceptingNode(strippedName);
                
            if(isInitialNode(name))
                return new InitialNode(strippedName);
        
            return new NormalNode(strippedName);
        }
    }
}
