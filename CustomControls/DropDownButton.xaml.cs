using MauiPopup;
using System.Collections;
using System.Windows.Input;

namespace CustomPickerControl.CustomControls;

public partial class DropDownButton : Frame
{
    public DropDownButton()
    {
        InitializeComponent();
    }



    public static readonly BindableProperty CurrentItemProperty = BindableProperty.Create(
        propertyName: nameof(CurrentItem),
        returnType: typeof(object),
        declaringType: typeof(DropDownButton),
        propertyChanged: CurrentItemPropertyChanged,
        defaultBindingMode: BindingMode.TwoWay);

    private static void CurrentItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var controls = (DropDownButton)bindable;

        if (newValue != null)
        {
            if (!string.IsNullOrWhiteSpace(controls.DisplayMember))
                controls.lblDropDown.Text = newValue.GetType().GetProperty(controls.DisplayMember).GetValue(newValue, null).ToString();
        }
    }

    public object CurrentItem
    {
        get => (object)GetValue(CurrentItemProperty);
        set => SetValue(CurrentItemProperty, value);
    }

    public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
        propertyName: nameof(Placeholder),
        returnType: typeof(string),
        declaringType: typeof(DropDownButton),
        propertyChanged: PlaceholderPropertyChanged,
        defaultBindingMode: BindingMode.OneWay);

    private static void PlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var controls = (DropDownButton)bindable;

        if (controls.CurrentItem == null)
        {
            if (newValue != null)
            {
                controls.lblDropDown.Text = newValue.ToString();
            }
        }
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(
        propertyName: nameof(ItemSource),
        returnType: typeof(IEnumerable),
        declaringType: typeof(DropDownButton),
        defaultBindingMode: BindingMode.OneWay);

    public IEnumerable ItemSource
    {
        get => (IEnumerable)GetValue(ItemSourceProperty);
        set => SetValue(ItemSourceProperty, value);
    }

    public static readonly BindableProperty IsLoadingProperty = BindableProperty.Create(
       propertyName: nameof(IsLoading),
       returnType: typeof(bool),
       declaringType: typeof(DropDownButton),
       defaultBindingMode: BindingMode.OneWay);

    public bool IsLoading
    {
        get => (bool)GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }


    public event EventHandler<EventArgs> OpenPickerEvent;
    public static readonly BindableProperty OpenPickerCommandProperty = BindableProperty.Create(
       propertyName: nameof(OpenPickerCommand),
       returnType: typeof(ICommand),
       declaringType: typeof(DropDownButton),
       defaultBindingMode: BindingMode.TwoWay);

    public ICommand OpenPickerCommand
    {
        get => (ICommand)GetValue(OpenPickerCommandProperty);
        set => SetValue(OpenPickerCommandProperty, value);
    }

    public static readonly BindableProperty IsDisplayPickerControlProperty = BindableProperty.Create(
      propertyName: nameof(IsDisplayPickerControl),
      returnType: typeof(bool),
      declaringType: typeof(DropDownButton),
      propertyChanged: IsDisplayPickerControlPropertyChanged,
      defaultBindingMode: BindingMode.TwoWay);

    private async static void IsDisplayPickerControlPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var controls = (DropDownButton)bindable;

        if (newValue != null)
        {
            if ((bool)newValue)
            {
                var response = await PopupAction.DisplayPopup<object>(new PickerControlView(controls.ItemSource, controls.ItemTemplate, controls.PickerHeightRequest));
                if (response != null)
                {
                    controls.CurrentItem = response;
                }
                controls.IsDisplayPickerControl = false;
            }
        }
    }

    public bool IsDisplayPickerControl
    {
        get => (bool)GetValue(IsDisplayPickerControlProperty);
        set => SetValue(IsDisplayPickerControlProperty, value);
    }



    public DataTemplate ItemTemplate { get; set; }
    public double PickerHeightRequest { get; set; }

    public string DisplayMember { get; set; }


    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

        OpenPickerCommand?.Execute(null);
        OpenPickerEvent?.Invoke(sender, e);

    }
}