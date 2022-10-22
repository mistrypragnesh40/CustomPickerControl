using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CustomPickerControl.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomPickerControl
{
    public partial class MainPageViewModel : ObservableObject
    {
        public ObservableCollection<TitleValue> Items { get; set; } = new ObservableCollection<TitleValue>();


        [ObservableProperty]
        private bool _isLoading;


        [ObservableProperty]
        private TitleValue _currentItem;


        [ObservableProperty]
        private bool _isDisplayPicker;

        public MainPageViewModel()
        {
            CurrentItem  =new TitleValue() { Title="Default",Value="Default"};
        }


        [RelayCommand]
        public async void OpenPicker()
        {
            IsLoading = true;
            // wait till api response is return

            await Task.Delay(1000);

            Items.Clear();
            Items.Add(new TitleValue { Title = "Title 1", Value = "Value 1" });
            Items.Add(new TitleValue { Title = "Title 2", Value = "Value 2" });
            Items.Add(new TitleValue { Title = "Title 3", Value = "Value 3" });
            Items.Add(new TitleValue { Title = "Title 4", Value = "Value 4" });
            Items.Add(new TitleValue { Title = "Title 5", Value = "Value 5" });
            Items.Add(new TitleValue { Title = "Title 6", Value = "Value 6" });
            Items.Add(new TitleValue { Title = "Title 7", Value = "Value 7" });
            Items.Add(new TitleValue { Title = "Title 8", Value = "Value 8" });
            Items.Add(new TitleValue { Title = "Title 9", Value = "Value 9" });

            IsLoading = false;

            IsDisplayPicker = true;

        }
    }
}
