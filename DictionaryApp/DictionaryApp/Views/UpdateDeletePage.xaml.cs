using DictionaryApp.Modals;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DictionaryApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class UpdateDeletePage : ContentPage
	{
        WordPair item;

        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        WordPair wp = new WordPair();

        public UpdateDeletePage (WordPair data)
		{
			InitializeComponent ();

            item = data;

            entry_tr.Text = item.TR;
            entry_en.Text = item.EN;
        }

        async void DeleteFunction(object sender, EventArgs e)
        {
            if (item.TR == entry_tr.Text && item.EN == entry_en.Text)
            {
                var db = new SQLite.SQLiteConnection(_dbPath);

                var answer = await DisplayAlert("Attention!", item.TR + "-" + item.EN + " pair will be deleted parmanently. Are you sure to delete the pair?", "YES", "NO");
                if (answer == true)
                {
                    db.Table<WordPair>().Delete(x => x.ID == item.ID);

                    await DisplayAlert("Info!", item.TR + "-" + item.EN + " pair is deleted successfully.", "OK");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    // answer = "NO" => do nothing
                }
            }
            else
            {
                await DisplayAlert("Warning!", "You have changed the selected item. It can't be deleted.", "OK");
            }
            
        }

        async void UpdateFunction(object sender, EventArgs e)
        {
            if (entry_en.Text != "" && entry_tr.Text != "")
            {
                var db = new SQLite.SQLiteConnection(_dbPath);

                WordPair wp = new WordPair()
                {
                    ID = item.ID,
                    TR = entry_tr.Text,
                    EN = entry_en.Text
                };
                db.Update(wp);

                await DisplayAlert("Info", wp.TR + "-" + wp.EN + " pair is updated successfully.", "OK");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Warning!", "Lack of entry.", "OK");
            }
            
        }
    }
}