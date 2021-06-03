using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace PhonewordXAML
{
    public partial class MainPage : ContentPage
    {
        string translatedNumber;
        public MainPage()
        {
            InitializeComponent();
        }
        //Logic for TranslateButton
        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = phoneNumberText.Text;
            translatedNumber = PhonewordXAML.PhonewordTranslator.ToNumber(enteredNumber);

            if (!string.IsNullOrEmpty(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }

        //Logic for callButton
        private async void OnCall(object sender, System.EventArgs e)
        {
            if (await this.DisplayAlert(
                "Dial a Number",
                "Would you like to call " + translatedNumber + "?",
                "Yes",
                "No"))
            {
                try
                {
                    PhoneDialer.Open(translatedNumber);
                }
                catch (ArgumentNullException)
                {
                    await DisplayAlert("Unable to dial", "Num not valid", "Ok");
                }
                catch (FeatureNotEnabledException)
                {
                    await DisplayAlert("Unable", "Not supported", "Ok");
                }
                catch (Exception)
                {
                    await DisplayAlert("Unable", "Failed", "OK");
                }
            }
        }
    }
}
