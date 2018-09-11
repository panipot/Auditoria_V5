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
            Picker pickerBluetoothDevices = new Picker() { Title = "Select a bth device" };
            pickerBluetoothDevices.SetBinding(Picker.ItemsSourceProperty, "ListOfDevices");
            pickerBluetoothDevices.SetBinding(Picker.SelectedItemProperty, "SelectedBthDevice");
            pickerBluetoothDevices.SetBinding(VisualElement.IsEnabledProperty, "IsPickerEnabled");
            Entry entrySleepTime = new Entry() { Keyboard = Keyboard.Numeric, Placeholder = "Sleep time" };
            entrySleepTime.SetBinding(Entry.TextProperty, "SleepTime");
            Button buttonConnect = new Button() { Text = "Connect" };
            buttonConnect.SetBinding(Button.CommandProperty, "ConnectCommand");
            buttonConnect.IsEnabled = true;
            StackLayout slButtons = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { pickerBluetoothDevices, entrySleepTime, buttonConnect } };
            Content = slButtons;
           // InitializeComponent ();
		}
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}