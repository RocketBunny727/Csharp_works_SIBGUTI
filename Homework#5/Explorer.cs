using Avalonia.Media.Imaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace Homework_5
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
        private Bitmap _image;
        FileSystemItem _choosenItem;
        FileSystemItem _choosenImage;
        public ObservableCollection<FileSystemItem> Items { get; } = new ObservableCollection<FileSystemItem>();

        private FileSystemWatcher _fileSystemWatcher;

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
                    LoadFileSystemItemsAsync(CurrentPath);
                    OnPropertyChanged(nameof(CurrentPath));
                }
            }
        }

        public FileSystemItem ChoosenImage
        {
            get => _choosenImage;
            set
            {
                _choosenImage = value;
                ImageViewer(_choosenImage);
            }
        }

        public Bitmap Image
        {
            get => _image;
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public Explorer()
        {
            _mode = 0;
            CurrentPath = "C:\\";
            SetupFileSystemWatcher();
        }

        private void SetupFileSystemWatcher()
        {
            _fileSystemWatcher = new FileSystemWatcher();
            _fileSystemWatcher.Path = CurrentPath;
            _fileSystemWatcher.IncludeSubdirectories = false;
            _fileSystemWatcher.EnableRaisingEvents = true;

            _fileSystemWatcher.Created += OnFileSystemChanged;
            _fileSystemWatcher.Deleted += OnFileSystemChanged;
            _fileSystemWatcher.Renamed += OnFileSystemChanged;
            _fileSystemWatcher.Changed += OnFileSystemChanged;
        }

        private void OnFileSystemChanged(object sender, FileSystemEventArgs e)
        {
            LoadFileSystemItemsAsync(CurrentPath);
        }

        private async Task LoadFileSystemItemsAsync(string path)
        {
            await Task.Run(() =>
            {
                LoadFileSystemItems(path);
            });
        }

        private void LoadFileSystemItems(string path)
        {
            Items.Clear();
            string extension;

            if (_mode == 0)
            {
                Items.Add(new FileSystemItem { Name = "..", Type = "parent_directory", Path = path, Icon = new Bitmap("~/../../../../Assets/up_folder_icon.png") });

                foreach (string directory in Directory.GetDirectories(path))
                {
                    Items.Add(new FileSystemItem { Name = Path.GetFileName(directory), Type = "directory", Path = path, Icon = new Bitmap("~/../../../../Assets/folder_icon.png") });
                }

                foreach (string file in Directory.GetFiles(path))
                {
                    extension = Path.GetExtension(file);
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp")
                    {
                        Items.Add(new FileSystemItem { Name = Path.GetFileName(file), Type = "image", Path = path, Icon = new Bitmap("~/../../../../Assets/file_icon.png") });
                    }
                }
            }
            else if (_mode == 1)
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
                else if (item.Name == ".." && Directory.GetParent(_currentPath) == null)
                {
                    _mode = 1;
                    LoadFileSystemItemsAsync(_currentPath);
                }
                else
                {
                    if (item.Type == "directory")
                    {
                        CurrentPath = Path.Combine(CurrentPath, item.Name);
                    }
                    else if (item.Type == "logical_drive")
                    {
                        LoadFileSystemItemsAsync(_currentPath);
                    }
                }
            }
        }

        public void ImageViewer(FileSystemItem item)
        {
            Image = new Bitmap(Path.Combine(CurrentPath, item.Name));
        }
    }
}
