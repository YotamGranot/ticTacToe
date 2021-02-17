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
            btnExit = FindViewById<Button>(Resource.Id.btnExitResult);
            btnFrom.Click += BtnFrom_Click;
            
            // Create your application here
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
        }
    }
   
}