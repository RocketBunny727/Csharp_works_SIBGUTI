using Avalonia.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Homework_8.ViewModels
{
    public class DataGridViewModel : BaseViewModel
    {
        private ObservableCollection<User> _dataGrid;
        public ObservableCollection<User> DataGrid
        {
            get { return _dataGrid; }
            set { SetProperty(ref _dataGrid, value); }
        }

        private UserApp _userApp;

        public DataGridViewModel()
        {
            _userApp = new UserApp();
            DataGrid = new ObservableCollection<User>();
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
                    DataGrid.Add(user);
                }
            }
        }
    }
}