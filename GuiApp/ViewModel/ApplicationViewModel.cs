using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AssemblyBrowserLibrary;
using GuiApp.Command;
using GuiApp.Model;
using Microsoft.Win32;

namespace GuiApp.ViewModel
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public ApplicationViewModel()
        {
            Dlls = new ObservableCollection<Dll>();
        }

        public ObservableCollection<Dll> Dlls { get; set; }

        private Dll _selectedDll;
        public Dll SelectedDll
        {
            get => _selectedDll;
            set
            {
                _selectedDll = value;
                OnPropertyChanged();
            }
        }

        private MyCommand _addCommand;
        public MyCommand AddCommand
        {
            get
            {
                return _addCommand ??
                       (_addCommand = new MyCommand(obj =>
                       {
                           var openFileDialog = new OpenFileDialog();
                           openFileDialog.Filter = "Dll files(*.dll)|*.dll";
                           openFileDialog.ShowDialog();
                           var filename = openFileDialog.FileName;
                           if (filename == "") return;
                           var node = AssemblyBrowser.BuildTree(AssemblyBrowser.GetAssemblyInfo(filename));

                           var dll = new Dll(openFileDialog.FileName, openFileDialog.SafeFileName, node);
                           Dlls.Insert(0, dll);
                           SelectedDll = dll;
                       }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}