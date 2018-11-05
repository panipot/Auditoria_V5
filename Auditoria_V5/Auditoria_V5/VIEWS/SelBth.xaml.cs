using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Auditoria_V5
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SelBth : ContentPage
	{
		public SelBth ()
		{
            this.BindingContext = new clsBthModel();
            this.BackgroundColor =Color.FromHex("#3e606f");
            Picker pickerBluetoothDevices = new Picker() { Title = "Select a bth device", VerticalOptions = LayoutOptions.Start };
            pickerBluetoothDevices.SetBinding(Picker.ItemsSourceProperty, "ListOfDevices");
            pickerBluetoothDevices.SetBinding(Picker.SelectedItemProperty, "SelectedBthDevice");
            pickerBluetoothDevices.SetBinding(VisualElement.IsEnabledProperty, "IsPickerEnabled");
            Entry entrySleepTime = new Entry() { Keyboard = Keyboard.Numeric, Placeholder = "Sleep time", VerticalOptions = LayoutOptions.Start };
            entrySleepTime.SetBinding(Entry.TextProperty, "SleepTime");
            Button buttonConnect = new Button() { Text = "Connect", VerticalOptions=LayoutOptions.Start};
            buttonConnect.SetBinding(Button.CommandProperty, "ConnectCommand");
            buttonConnect.IsEnabled = true;
            StackLayout slButtons = new StackLayout() { Orientation = StackOrientation.Vertical, Children = { pickerBluetoothDevices, entrySleepTime, buttonConnect } };
            Content = slButtons;
         
		}
        //protected override bool OnBackButtonPressed()
        //{
        //    return true;
        //}
    }
}