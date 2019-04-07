using System;
using System.Collections.Generic;
using System.Linq;
using DictionaryApp.Modals;
using DictionaryApp.AppSettings;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Messaging;
using System.Text.RegularExpressions;
using System.Net.Mail;

namespace DictionaryApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        bool valid = false;

		public LoginPage ()
		{
			InitializeComponent ();
		}

        void SignInOrRegFunction(object sender, EventArgs e)
        {
            User user = new User(entry_email.Text, entry_password.Text);

            checkValidation();

            if (valid)
            {
                var navPage = new NavigationPage(new DictionaryApp.MainPage());
                //Navigation.PushModalAsync(new LoginPage()); 
                navPage.BarBackgroundColor = Color.FromHex("#2196F3");
                navPage.BarTextColor = Color.White;

                if (user.CheckInfo())
                {
                    DisplayAlert("Success!", "Email and password are confirmed. Welcome!", "OK");
                    
                    Navigation.PushModalAsync(navPage);
                }
                else
                {
                    LocalStorageSettings.MailKey = entry_email.Text;
                    LocalStorageSettings.PasswordKey = entry_password.Text;

                    DisplayAlert("New user!", "Your info has been recorded. Enjoy!", "OK");

                    Navigation.PushModalAsync(navPage);
                }
            }
            
        }

        void checkValidation()
        {
            bool validE = false;
            bool validP = false;

            // mail validation
            if (validateMail(entry_email.Text))
            {
                validE = true;
                help_lbl_email.Text = "";
            }
            else
                help_lbl_email.Text = " *invalid email";

            // password validation
            if (entry_password.Text.Length >= 5)
            {
                validP = true;
                help_lbl_password.Text = "";
            }
            else
                help_lbl_password.Text = " *password must contains at least 5 characters";

            valid = validE & validP;
        }
        
        bool validateMail(string email)
        {
            bool validation = false;

            //emailregex.com
            var mailPattern = "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";

            if (Regex.IsMatch(email, mailPattern) && entry_email != null) validation = true;

            return validation;
        }
    }
}