using DictionaryApp.Views;
using System;
using System.Collections.Generic;
using System.IO;
using SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using DictionaryApp.Modals;

namespace DictionaryApp
{
    public partial class MainPage : ContentPage
    {
        string _dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "myDB.db3");
        SQLite.SQLiteConnection db;

        public MainPage()
        {
            InitializeComponent();

            NavigationPage.SetBackButtonTitle(this, "Menu");

            btn_config.Clicked += (s, e) => Navigation.PushAsync(new ConfigPage());

            db = new SQLite.SQLiteConnection(_dbPath);

            db.CreateTable<WordPair>();
        }

        void TranslateFunction(object sender, EventArgs e)
        {
            var maxPK = db.Table<WordPair>().OrderByDescending(x => x.ID).FirstOrDefault();

            if (btn_lang.Text == "TR>EN")
            {
                if (maxPK == null)
                {
                    lbl_translate.Text = "Veritabaında henüz kelime bulunmamaktadır.";
                }
                else
                {
                    var item = from p in db.Table<WordPair>()
                               where p.TR == entry_translate.Text
                               select p;
                    try
                    {
                        lbl_translate.Text = item.SingleOrDefault().EN;
                    }
                    catch (NullReferenceException ex)
                    {
                        lbl_translate.Text = "Sözcük veritabanında bulunamadı.";
                    }
                }
            }
            else
            {
                if (maxPK == null)
                {
                    lbl_translate.Text = "There isn't any words in database yet.";
                }
                else
                {
                    var item = from p in db.Table<WordPair>()
                               where p.EN == entry_translate.Text
                               select p;
                    try
                    {
                        lbl_translate.Text = item.SingleOrDefault().TR;
                    }
                    catch(NullReferenceException ex)
                    {
                        lbl_translate.Text = "Word is not found is database.";
                    }
                }
                
            }
        }

        void LanguageReverseFunction(object sender, EventArgs e)
        {
            // change the buttons' text
            // change entry placeholder

            if (btn_lang.Text == "TR>EN")
            {
                btn_lang.Text = "EN>TR";
                btn_config.Text = "CONFIGURE";
                entry_translate.Placeholder = "Enter some word..";
                lbl_translate.Text = "";
                entry_translate.Text = "";
            }
            else
            {
                btn_lang.Text = "TR>EN";
                btn_config.Text = "YAPILANDIR";
                entry_translate.Placeholder = "Kelime giriniz..";
                lbl_translate.Text = "";
                entry_translate.Text = "";
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            entry_translate.Text = "";
            lbl_translate.Text = "";
        }
    }
}
