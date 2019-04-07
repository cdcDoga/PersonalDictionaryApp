using System;
using System.Collections.Generic;
using System.Text;
using DictionaryApp.AppSettings;

namespace DictionaryApp.Modals
{
    public class User
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public User() { }
        public User(string email, string password)
        {
            this.email = email;
            this.password = password;
        }

        public bool CheckInfo()
        {
            if (this.email == LocalStorageSettings.MailKey && this.password == LocalStorageSettings.PasswordKey)
                return true;
            else
                return false;
        }
    }
}
