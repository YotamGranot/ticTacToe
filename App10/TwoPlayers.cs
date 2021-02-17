using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Views.View;

namespace App10
{
    [Activity(Label = "TwoPlayers")]
    public class TwoPlayers : Activity, IOnClickListener
    {
        bool turn = false;
        Board board = new Board();
        Button[,] buttons = new Button[3, 3];
        Button exitBoard;
        int turnCount = 0;
        TextView turnText, title;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.board_layout);
            // Create your application here
            title = FindViewById<TextView>(Resource.Id.title);
            title.Text = "Two players";
            turnText = FindViewById<TextView>(Resource.Id.turn);
            turnText.Text = "X's turn";
            exitBoard = FindViewById<Button>(Resource.Id.btnExitBoard);
            buttons[0, 0] = FindViewById<Button>(Resource.Id.btn0);
            buttons[0, 1] = FindViewById<Button>(Resource.Id.btn1);
            buttons[0, 2] =  FindViewById<Button>(Resource.Id.btn2);
            buttons[1, 0] = FindViewById<Button>(Resource.Id.btn3); 
            buttons[1, 1] = FindViewById<Button>(Resource.Id.btn4); 
            buttons[1, 2] = FindViewById<Button>(Resource.Id.btn5);
            buttons[2,0] = FindViewById<Button>(Resource.Id.btn6); 
            buttons[2, 1] = FindViewById<Button>(Resource.Id.btn7);
            buttons[2, 2] = FindViewById<Button>(Resource.Id.btn8);
            exitBoard = FindViewById<Button>(Resource.Id.btnExitBoard);
            buttons[0, 0].SetOnClickListener(this);
            buttons[0, 1].SetOnClickListener(this);
            buttons[0, 2].SetOnClickListener(this);
            buttons[1, 0].SetOnClickListener(this);
            buttons[1, 1].SetOnClickListener(this);
            buttons[1, 2].SetOnClickListener(this);
            buttons[2, 0].SetOnClickListener(this);
            buttons[2, 1].SetOnClickListener(this);
            buttons[2, 2].SetOnClickListener(this);
            exitBoard.Click += ExitBoard_Click;
            
        }

        private void ExitBoard_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            intent.PutExtra("winner", "draw");
            SetResult(Android.App.Result.Ok, intent);
            Finish();
        }

        public void OnClick(View v)
        {
            Button btn = (Button)v;
            if (btn.Text != "O" && btn.Text != "X")
            {
                turnCount++;
                if (turn)
                {
                    int[] cor = new int[2] { int.Parse(btn.Text[0].ToString()), int.Parse(btn.Text[1].ToString()) };
                    btn.Text = "O";
                    btn.SetTextColor(Color.Black);
                    board.SetCell(cor, turn);
                    turn = !turn;
                    turnText.Text = "X's turn";
                }
                else
                {
                    int[] cor = new int[2] { int.Parse(btn.Text[0].ToString()), int.Parse(btn.Text[1].ToString()) };
                    btn.Text = "X";
                    btn.SetTextColor(Color.Black);
                    board.SetCell(cor, turn);
                    turn = !turn;
                    turnText.Text = "Circle's turn";
                    
                }
            }
            Intent intent = new Intent();
            if (board.CheckWin())
            {
                string msg;
                if (!turn)
                {
                    msg = "circle won";
                    intent.PutExtra("winner", "circle");
                    SetResult(Android.App.Result.Ok, intent);
                }
                else
                {
                    msg = "X won";
                    intent.PutExtra("winner", "x");
                    SetResult(Android.App.Result.Ok, intent);
                }
                Toast.MakeText(this, msg, ToastLength.Long).Show();
                Finish();
            }
            if (turnCount == 9)
            {
                string msg = "it's a draw";
                intent.PutExtra("winner", "draw");
                SetResult(Android.App.Result.Ok, intent);
                Toast.MakeText(this, msg, ToastLength.Long).Show();
                Finish();
            }

        }
    }
}