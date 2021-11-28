using AssemblyBrowserLibrary.Structures;

namespace GuiApp.Model
{
    public class Dll
    {
        public Dll(string path, string dllName, Node node)
        {
            Path = path;
            Name = dllName;
            Node = node;
        }

        public string Path { get; set; }

        public string Name { get; set; }

        public Node Node { get; }
    }
}