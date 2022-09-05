using CommunityToolkit.Mvvm.ComponentModel;

namespace Elysium.Playground
{
    [INotifyPropertyChanged]
    public partial class BasicViewModel
    {
        [ObservableProperty]
        private string _name = string.Empty;
        
        private void Start()
        {
            Name = "Asdf";
        }
    }
}