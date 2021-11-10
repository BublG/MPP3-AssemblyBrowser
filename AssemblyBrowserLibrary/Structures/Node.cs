using System.Collections.ObjectModel;

namespace AssemblyBrowserLibrary.Structures
{
    public class Node
    {
        public string Name { get; set; }
        public ObservableCollection<Node> Child { get; }

        public Node()
        {
            Child = new ObservableCollection<Node>();
        }

        public override string ToString()
        {
            return Name + "\n";
        }
    }
}