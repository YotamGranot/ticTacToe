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
            editor.Commit();

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
        public override string ToString()
        {
            string user = "";
            switch (UserWon)
            {
                case Users.computer:
                    {
                        user = "computer";
                        break;
                    }
                case Users.circle:
                    {
                        user = "circle";
                        break;
                    }
                case Users.ex:
                    {
                        user = "X";
                        break;
                    }
                case Users.draw:
                    {
                        return "Date: " + GamePlayed.ToString() + " game was drawn";
                        
                    }
                default:
                    {
                        user = "";
                        break;
                    }

            }
            return "Date: " + GamePlayed.ToString() + " " +user +" won";
        }
    }
}