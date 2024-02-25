using Avalonia.Media.Imaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Homework_4
{
    public class FileSystemItem
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Bitmap Icon { get; set; }
        public string Path { get; set; }
    }

    public class Explorer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _mode;
        private string _currentPath;
        FileSystemItem _choosenItem;
        public ObservableCollection<FileSystemItem> Items { get; } = new ObservableCollection<FileSystemItem>();

        public FileSystemItem ChoosenItem
        {
            get => _choosenItem;
            set
            {
                _choosenItem = value;
                DoubleMouseClick(_choosenItem);
            }
        }

        public string CurrentPath
        {
            get => _currentPath;
            set
            {
                if (_currentPath != value)
                {
                    _currentPath = value;
                    LoadFileSystemItems(_currentPath);
                    OnPropertyChanged(nameof(CurrentPath));
                }
            }
        }

        public Explorer()
        {
            _mode = 0;
            CurrentPath = "C:\\";
        }

        private void LoadFileSystemItems(string path)
        {
            Items.Clear();
            if (_mode == 0)
            {
                Items.Add(new FileSystemItem { Name = "..", Type = "parent_directory", Path = path, Icon = new Bitmap("~/../../../../Assets/up_folder_icon.png") });

                foreach (string directory in Directory.GetDirectories(path))
                {
                    Items.Add(new FileSystemItem { Name = System.IO.Path.GetFileName(directory), Type = "directory", Path = path, Icon = new Bitmap("~/../../../../Assets/folder_icon.png") });
                }

                foreach (string file in Directory.GetFiles(path))
                {
                    Items.Add(new FileSystemItem { Name = System.IO.Path.GetFileName(file), Type = "file", Path = path, Icon = new Bitmap("~/../../../../Assets/file_icon.png") });
                }
            }
            else if(_mode == 1)
            {
                foreach (string drive in Directory.GetLogicalDrives())
                {
                    Items.Add(new FileSystemItem { Name = drive, Type = "logical_drive", Path = path, Icon = new Bitmap("~/../../../../Assets/logical_drive_icon.png") });
                }
                _mode = 0;
            }
        }

        public void DoubleMouseClick(FileSystemItem item)
        {
            if (item != null)
            {
                if (item.Name == ".." && Directory.GetParent(_currentPath) != null)
                {
                    CurrentPath = Directory.GetParent(CurrentPath)?.FullName;
                }
                else if(item.Name == ".." && Directory.GetParent(_currentPath) == null)
                {
                    _mode = 1;
                    LoadFileSystemItems(_currentPath);
                }
                else
                {
                    if (item.Type == "directory")
                    {
                        CurrentPath = Path.Combine(CurrentPath, item.Name);
                    }
                    else if( item.Type == "logical_drive")
                    {
                        LoadFileSystemItems(_currentPath);
                    }
                }
            }
        }
    }
}
