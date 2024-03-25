using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Homework_8.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Homework_8.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty] private object content;
    [ObservableProperty] private ObservableCollection<BaseViewModel> vmbaseCollection;
    public RelayCommand<object> SwitchViewCommand { get; }
    public MainViewModel()
    {
        vmbaseCollection = new ObservableCollection<BaseViewModel>
        {
            new DataGridViewModel(),
            new TreeViewModel()
        };

        Content = null;
        SwitchViewCommand = new RelayCommand<object>(SwitchView);
    }

    private void SwitchView(object parameter)
    {
        if (parameter is BaseViewModel selectedViewModel)
        {
            Content = selectedViewModel;
        }
    }
}
