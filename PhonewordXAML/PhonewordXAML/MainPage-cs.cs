using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Essentials;

namespace Phoneword
{
    public class MainPage : ContentPage
    {
        //Create elements
        Entry phoneNumberText;
        Button translateButton;
        Button callButton;
        string translatedNumber;
        public MainPage()
        {
            //Instantiate UI
            this.Padding = new Thickness(20);

            
            StackLayout panel = new StackLayout
            {
                Spacing = 15
            };

            panel.Children.Add(new Label
            {
                Text = "Enter a Phoneword please:",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            });

            panel.Children.Add(phoneNumberText = new Entry
            {
                Text = "1-999-Testingicle"
            });

            panel.Children.Add(translateButton = new Button
            {
                Text = "Translate"
            });

            panel.Children.Add(callButton = new Button
            {
                Text = "Call",
                IsEnabled = false
                
            });

            
            //Instantiate Listeners
            translateButton.Clicked += OnTranslate;
            callButton.Clicked += OnCall;
            this.Content = panel;
        }

        //Logic for TranslateButton
        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = phoneNumberText.Text;
            translatedNumber = Phoneword.PhonewordTranslator.ToNumber(enteredNumber);

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
        private async void OnCall (object sender, System.EventArgs e)
        {
            if (await this.DisplayAlert (
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