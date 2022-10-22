using CustomPickerControl.Models;

namespace CustomPickerControl;

public partial class MainPage : ContentPage
{
	int count = 0;

	

	public MainPage()
	{
		InitializeComponent();
		this.BindingContext = new MainPageViewModel();
    }

    private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	private async void dropdownControl_OpenPickerEvent(object sender, EventArgs e)
	{
		dropdownControl.IsLoading = true;

        // response return from api

        var items = new List<TitleValue>();

        items.Add(new TitleValue { Title = "Title 1" });
        items.Add(new TitleValue { Title = "Title 2" });
        items.Add(new TitleValue { Title = "Title 3" });
        items.Add(new TitleValue { Title = "Title 4" });
        items.Add(new TitleValue { Title = "Title 5" });
        items.Add(new TitleValue { Title = "Title 6" });
        items.Add(new TitleValue { Title = "Title 7" });

        dropdownControl.ItemSource = items;


        await Task.Delay(1000);

        dropdownControl.IsLoading = false;
		dropdownControl.IsDisplayPickerControl = true;
    }
}

