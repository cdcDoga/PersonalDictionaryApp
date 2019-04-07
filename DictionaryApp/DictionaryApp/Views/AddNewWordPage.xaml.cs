using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DictionaryApp.Modals;

namespace DictionaryApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewWordPage : ContentPage
	{
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");

		public AddNewWordPage ()
		{
			InitializeComponent ();
		}

        async void SaveAdditionFunction(object sender, EventArgs e)
        {
            if(entry_en.Text != "" && entry_tr.Text != "")
            {
                var db = new SQLite.SQLiteConnection(_dbPath);
                // db.CreateTable<WordPair>();

                WordPair wp = new WordPair()
                {
                    EN = entry_en.Text,
                    TR = entry_tr.Text
                };
                db.Insert(wp);

                await DisplayAlert("Info", wp.TR + "-" + wp.EN + " pair is saved successfully.", "OK");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Warning!", "Lack of entry.", "OK");
            }
        }
    }
}