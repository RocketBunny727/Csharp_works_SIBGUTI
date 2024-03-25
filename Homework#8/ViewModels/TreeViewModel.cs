using Avalonia.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Homework_8.ViewModels
{
    public class TreeViewModel : BaseViewModel
    {
        private ObservableCollection<User> _dataTree;
        public ObservableCollection<User> DataTree
        {
            get { return _dataTree; }
            set { SetProperty(ref _dataTree, value); }
        }

        private UserApp _userApp;

        public TreeViewModel()
        {
            _userApp = new UserApp();
            DataTree = new ObservableCollection<User>();
            InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await _userApp.LoadUsersAsync();
            LoadUsers();
        }

        private void LoadUsers()
        {
            ObservableCollection<User> users = new ObservableCollection<User>(_userApp.Users);
            if (users != null)
            {
                foreach (var user in users)
                {
                    DataTree.Add(user);
                }
            }
        }
    }

}