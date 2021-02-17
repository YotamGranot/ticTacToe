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
using Android.Support.V7.App;

namespace App10
{
    [Activity(Label = "MyResults")]
    public class MyResults : Activity
    {
        Button btnFrom, btnTo, btnGo, btnExit;
        DateTime from, to;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.result_layout);
            btnFrom = FindViewById<Button>(Resource.Id.btnFrom);
            btnGo = FindViewById<Button>(Resource.Id.btnGo);
            btnTo = FindViewById<Button>(Resource.Id.btnTo);
            btnExit = FindViewById<Button>(Resource.Id.btnExitRecord);
            btnFrom.Click += BtnFrom_Click;
            btnExit.Click += BtnExit_Click;
            btnGo.Click += BtnGo_Click;
            btnTo.Click += BtnTo_Click;
            // Create your application here
        }

        private void BtnTo_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;



            DatePickerDialog datePickerDialog = new DatePickerDialog(this, OnDateSetTo, today.Year, today.Month - 1, today.Day);

            datePickerDialog.Show();
        }

        private void BtnGo_Click(object sender, EventArgs e)
        {
            for (int i = 0;  i < Records.myRecords.Count; i++)
            {
                if (Records.myRecords[i].GamePlayed.Date >=from && Records.myRecords[i].GamePlayed.Date <= to)
                {
                    LinearLayout ll =  FindViewById<LinearLayout>(Resource.Id.ll);
                    LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams(LinearLayout.LayoutParams.MatchParent, LinearLayout.LayoutParams.WrapContent);
                    TextView record = new TextView(this);
                    record.LayoutParameters = layoutParams;
                    record.Text = string.Format(Records.myRecords[i].ToString());
                    record.TextSize = 20;
                    ll.AddView(record);
                }
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void BtnFrom_Click(object sender, EventArgs e)
        {

            DateTime today = DateTime.Today;



            DatePickerDialog datePickerDialog = new DatePickerDialog(this, OnDateSetFrom, today.Year, today.Month - 1, today.Day);

            datePickerDialog.Show();
        }
        void OnDateSetFrom(object sender, DatePickerDialog.DateSetEventArgs e)

        {
            from = e.Date;

            string str = e.Date.ToLongDateString();
            Toast.MakeText(this, str, ToastLength.Long).Show();

            
        }
        void OnDateSetTo(object sender, DatePickerDialog.DateSetEventArgs e)

        {
            to = e.Date;
            string str = e.Date.ToLongDateString();
            Toast.MakeText(this, str, ToastLength.Long).Show();

           
        }
    }
   
}