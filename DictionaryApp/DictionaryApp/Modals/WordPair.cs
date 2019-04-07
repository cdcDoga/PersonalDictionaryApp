using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DictionaryApp.Modals
{
    public class WordPair
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(250), Unique]
        public string TR { get; set; }
        [MaxLength(250), Unique]
        public string EN { get; set; }

        public override string ToString()
        {
            return this.TR + "-" + this.EN;
        }
    }
}
