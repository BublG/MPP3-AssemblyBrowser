using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AssemblyBrowserLibrary.Structures
{
    public class Node
    {
        public string Name { get; set; }
        public List<Node> Child { get; }

        public Node()
        {
            Child = new List<Node>();
        }

        public override string ToString()
        {
            return Name + "\n";
        }
    }
}