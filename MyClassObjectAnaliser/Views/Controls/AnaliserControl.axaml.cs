using Avalonia.Controls;
using MyClassObjectAnaliser.Models;
using MyClassObjectAnaliser.ViewModels;
using MyClassObjectAnaliser.Views.Controls;
using System.ComponentModel;

namespace MyClassObjectAnaliser.Views.Controls
{
    public partial class AnaliserControl : UserControl, INotifyPropertyChanged
    {
        public AnaliserView View { get; }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public AnaliserControl()
        {
            InitializeComponent();

            View = new AnaliserView();
            DataContext = View;

            var content = new TreeViewMaker();
            content.DisplayObjectProperties(View.User, treeView);

            /*foreach(var item in View.TreeView.Items)
            {
                treeView.Items.Add(item);
            }*/
            //treeView.ItemsSource = View.TreeData;
            //treeView = content.TreeView;
            OnPropertyChanged(nameof(treeView));
            int a = 0;
        }
    }
}
