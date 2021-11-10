using System;
using System.Windows;
using System.Windows.Controls;
using AssemblyBrowserLibrary;
using AssemblyBrowserLibrary.Structures;
using Microsoft.Win32;

namespace GuiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectAssembly(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"d:\учеба\СПП\";
            openFileDialog.Filter = "Assemblies (*.dll, *.exe)|*.dll;*.exe";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    TreeView.Items.Clear();
                    Node root = AssemblyBrowser.BuildTree(AssemblyBrowser.GetAssemblyInfo(openFileDialog.FileName));
                    AddNameSpaces(root);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
        
        private void AddTypeMembers(Node type, TreeViewItem typeItem)
        {
            foreach (var typeMember in type.Child)
            {
                TreeViewItem typeMemberItem = new TreeViewItem();
                typeMemberItem.Header = typeMember.Name;
                typeItem.Items.Add(typeMemberItem);
            }
        }

        private void AddTypes(Node nameSpace, TreeViewItem nameSpaceItem)
        {
            foreach (var type in nameSpace.Child)
            {
                TreeViewItem typeItem = new TreeViewItem();
                typeItem.Header = type.Name;
                nameSpaceItem.Items.Add(typeItem);
                AddTypeMembers(type, typeItem);
            }
        }

        private void AddNameSpaces(Node root)
        {
            foreach (var nameSpace in root.Child)
            {
                TreeViewItem nameSpaceItem = new TreeViewItem();
                nameSpaceItem.Header = nameSpace.Name;
                TreeView.Items.Add(nameSpaceItem);
                AddTypes(nameSpace, nameSpaceItem);
            }
        }
    }
}