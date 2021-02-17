using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace App10
{
    class Records
    {

        public static List<MyRecord> myRecords = new List<MyRecord>();

        public static void enterRecord(DateTime dt, int user)
        {
            MyRecord record = new MyRecord
            {
                GamePlayed = dt,
                UserWon = (MyRecord.Users)user
            };
            myRecords.Add(record);
        }
        public static void saveRecords(Android.Content.ISharedPreferences sp)
        {
            string jstring = JsonConvert.SerializeObject(myRecords);
            var editor = sp.Edit();
            editor.PutString("records", jstring);

        }
        public static void initRecords(Android.Content.ISharedPreferences sp)
        {

            myRecords = JsonConvert.DeserializeObject<List<MyRecord>>(sp.GetString("records", null));
        }
    }
    class MyRecord
    {
        public enum Users
        {
            computer,
            circle,
            ex,
            draw
        }
        public DateTime GamePlayed { get; set; }
        public Users UserWon { get; set; }
    }
}