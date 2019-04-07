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
	public partial class ConfigPage : ContentPage
	{
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        SQLite.SQLiteConnection db;

        public ConfigPage ()
		{
			InitializeComponent ();

            db = new SQLite.SQLiteConnection(_dbPath);

            //db.CreateTable<WordPair>();

            var maxPK = db.Table<WordPair>().OrderByDescending(x => x.ID).FirstOrDefault();

            if(maxPK == null)
            {
                WordPairList.ItemsSource = new List<string>() {"There isn't any words in database yet."};
            }
            else
            {
                WordPairList.ItemsSource = db.Table<WordPair>().OrderBy(x => x.TR).ToList();
            }
            
            NavigationPage.SetBackButtonTitle(this, "Menu");
            btn_add.Clicked += (s, e) => Navigation.PushModalAsync(new AddNewWordPage());
            WordPairList.ItemSelected += (s, e) => Navigation.PushModalAsync(new UpdateDeletePage(e.SelectedItem as WordPair));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            db = new SQLite.SQLiteConnection(_dbPath);

            var maxPK = db.Table<WordPair>().OrderByDescending(x => x.ID).FirstOrDefault();

            if (maxPK == null)
            {
                WordPairList.ItemsSource = new List<string>() { "There isn't any words in database yet." };
            }
            else
            {
                WordPairList.ItemsSource = db.Table<WordPair>().OrderBy(x => x.TR).ToList();
            }
        }
    }
}