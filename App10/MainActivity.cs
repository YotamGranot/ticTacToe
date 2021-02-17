using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace App10
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        Android.Content.ISharedPreferences sp;
        LinearLayout ll;
        Button twoPlayers;
        Button onePlayer;
        Button res;
        Dialog d;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            twoPlayers = FindViewById<Button>(Resource.Id.btnFirstGameMode);
            onePlayer = FindViewById<Button>(Resource.Id.btnSecondGameMode);
            res = FindViewById<Button>(Resource.Id.btnRes);
            twoPlayers.Click += TwoPlayers_Click;
            onePlayer.Click += OnePlayer_Click;
            res.Click += Res_Click; ; 
            ll = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            sp = this.GetSharedPreferences("details", Android.Content.FileCreationMode.Private);
            //sp.Edit().Clear();
            //sp.Edit().Commit();
            string str = sp.GetString("records",null);
            if (str != null) {
                Records.initRecords(sp);
            }
            
        }

        private void Res_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MyResults));
            StartActivity(intent);
        }

        private void OnePlayer_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(OnePlayer));
            StartActivityForResult(intent, 1);
        }

        private void TwoPlayers_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(TwoPlayers));
            StartActivityForResult(intent, 0);
        }
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
           
            {
                
                Button yes, no;
                string winner = data.GetStringExtra("winner");
                d = new Dialog(this);
                d.SetCancelable(true);
                d.SetContentView(Resource.Layout.finished_layout);
                if (winner != "draw")
                {
                    d.SetTitle(string.Format("{0} won", winner));
                    if (winner == "x")
                    {
                        Records.enterRecord(System.DateTime.Today, 2);
                        Records.saveRecords(sp);
                    }
                }
                else
                {
                    d.SetTitle("it's a draw!!!");
                    Records.enterRecord(System.DateTime.Today, 3);
                    Records.saveRecords(sp);
                }
                if (requestCode == 0)
                {
                    yes = d.FindViewById<Button>(Resource.Id.btnYes);
                    no = d.FindViewById<Button>(Resource.Id.btnNo);
                    no.Click += No_Click;
                    yes.Click += Yes_Click_two;
                    if (winner == "circle")
                    {
                        Records.enterRecord(System.DateTime.Today, 1);
                        Records.saveRecords(sp);
                    }
                }
                if(requestCode == 1)
                {
                    yes = d.FindViewById<Button>(Resource.Id.btnYes);
                    no = d.FindViewById<Button>(Resource.Id.btnNo);
                    no.Click += No_Click;
                    yes.Click += Yes_Click_one;
                    if (winner == "circle")
                    {
                        Records.enterRecord(System.DateTime.Today, 0);
                        Records.saveRecords(sp);
                    }
                }
                d.Show();


            }
        }
        private void Yes_Click_two(object sender, System.EventArgs e)
        {
            TwoPlayers_Click(sender, e);
            d.Dismiss();
        }
        private void Yes_Click_one(object sender, System.EventArgs e)
        {
            OnePlayer_Click(sender, e);
            d.Dismiss();
        }

        private void No_Click(object sender, System.EventArgs e)
        {
            d.Dismiss();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}