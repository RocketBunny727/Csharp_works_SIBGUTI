using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace MyClassObjectAnaliser.Models
{
    public partial class TreeViewMaker : ObservableObject
    {

        public TreeViewMaker()
        {
        }

        public void DisplayObjectProperties(object obj, TreeView TreeView)
        {
            TreeView.Items.Clear();

            var rootNode = new TreeViewItem();
            rootNode.Header = obj.GetType().Name;
            TreeView.Items.Add(rootNode);

            AddPropertiesToTreeView(obj, rootNode);
        }

        private void AddPropertiesToTreeView(object obj, TreeViewItem parentNode)
        {
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(obj);

                var propertyNode = new TreeViewItem();
                if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                {
                    propertyNode.Header = $"{property.Name}: {propertyValue}";
                }
                else
                {
                    propertyNode.Header = property.Name;
                    AddPropertiesToTreeView(propertyValue, propertyNode);
                }

                parentNode.Items.Add(propertyNode);
            }
        }

    }
}
