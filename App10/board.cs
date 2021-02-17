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

namespace App10
{
    class Board
    {
        int turns = 0;
        enum States
        {
            empty,
            circle,
            ex,
        }
        private States[,] XOboard = new States[,] { { States.empty, States.empty, States.empty }, { States.empty, States.empty, States.empty }, { States.empty, States.empty, States.empty } };
        public Board()
        {
           
        }
        public bool SetCell(int[] cor, bool turn)
        {
            if(XOboard[cor[0],cor[1]] != States.empty)
            {
                return false;
            }
            if (turn)
            {
                XOboard[cor[0], cor[1]] = States.circle;
                turns++;
                return true;
            }
            XOboard[cor[0], cor[1]] = States.ex;
            turns++;
            return true;
        }
        public bool CheckWin()
        {
            if(XOboard[0,0]== XOboard[0, 1]&& XOboard[0, 0] == XOboard[0, 2]&& XOboard[0, 0] != States.empty)
            {
                return true;
            }
            if (XOboard[1, 0] == XOboard[1, 1] && XOboard[1, 0] == XOboard[1, 2] && XOboard[1, 0] != States.empty)
            {
                return true;
            }
            if (XOboard[2, 0] == XOboard[2, 1] && XOboard[2, 0] == XOboard[2, 2] && XOboard[2, 0] != States.empty)
            {
                return true;
            }
            if (XOboard[0, 0] == XOboard[1, 0] && XOboard[0, 0] == XOboard[2, 0] && XOboard[0, 0] != States.empty)
            {
                return true;
            }
            if (XOboard[0, 1] == XOboard[1, 1] && XOboard[0, 1] == XOboard[2, 1] && XOboard[0, 1] != States.empty)
            {
                return true;
            }
            if (XOboard[0,2] == XOboard[1, 2] && XOboard[0, 2] == XOboard[2, 2] && XOboard[0, 2] != States.empty)
            {
                return true;
            }
            if (XOboard[0, 0] == XOboard[1, 1] && XOboard[0, 0] == XOboard[2, 2] && XOboard[0, 0] != States.empty)
            {
                return true;
            }
            if (XOboard[0, 2] == XOboard[1, 1] && XOboard[0, 2] == XOboard[2, 0] && XOboard[0, 2] != States.empty)
            {
                return true;
            }
            return false;
        }
        public string calculateNextCell()
        {
            if (turns == 1 && XOboard[1, 1] == States.empty)
            {
                return "11";
            }
            string str = checkTwo(States.circle);
            if (str.Length > 0)
            {
                return str;
            }
            str = checkTwo(States.ex);
            if (str.Length > 0)
            {
                return str;
            }
            if(XOboard[0,0] == States.empty && isColEmpty(0)&& isRowEmpty(0))
            {
                return "00";
            }
            if (XOboard[2, 2] == States.empty && isColEmpty(2) && isRowEmpty(2))
            {
                return "22";
            }
            if (XOboard[0, 2] == States.empty && isColEmpty(2) && isRowEmpty(0))
            {
                return "02";
            }
            if (XOboard[2, 0] == States.empty && isColEmpty(0) && isRowEmpty(2))
            {
                return "20";
            }
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j<3; j++)
                {
                    if(XOboard[i, j] == States.empty)
                    {
                        return "" + i + j;
                    }
                }
            }
            return "";
        }
        private string checkTwo(States state)
        {
            for (int i = 0; i < 3; i++)
            {
                int index = isTwoInCol(i,state);
                if (index != -1)
                {
                    return "" + index + i;
                }
            }
            for (int i = 0; i < 3; i++)
            {
                int index = isTwoInRow(i,state);
                if (index != -1)
                {
                    return "" + i+ index;
                }
            }
            for (int i = 0; i <= 2; i+=2)
            {
                int index = isTwoInDiag(i,state);
                if (index != -1 && i == 0)
                {
                    return "" + index + index;
                }
                if (index != -1 && i == 2)
                {
                    return "" + (2 - index) + index;
                }
            }
            return "";
        }
        private int isTwoInRow(int row,States state)
        {
            int count = 0;
            int miss = -1;
            for(int i = 0; i < 3; i++)
            {
                if (XOboard[row, i] == state)
                    count++;
                else if(XOboard[row,i] == States.empty)
                    miss = i;
            }
            if (count == 2 && miss != -1)
                return miss;
            else
                return -1;
        }
        private int isTwoInCol(int col, States state)
        {
            int count = 0;
            int miss = -1;
            for (int i = 0; i < 3; i++)
            {
                if (XOboard[i, col] == state)
                    count++;
                else if (XOboard[i, col] == States.empty)
                    miss = i;
            }
            if (count == 2&& miss != -1)
                return miss;
            else
                return -1;
        }
        private int isTwoInDiag(int start, States state)
        {
            int count = 0;
            int miss = -1;
            bool left = start == 0 ? true : false;
            for (int i = 0; i < 3; i++)
            {
                if (XOboard[start, i] == state)
                {
                    count++;
                }
                else if (XOboard[start, i] == States.empty)
                {
                    miss = i;
                }
                   
                if (left)
                {
                    start++;
                }
                else
                {
                    start--;
                }

            }
            if (count == 2&& miss !=-1)
                return miss;
            else
                return -1;
        }
        private bool isRowEmpty(int row)
        {
            for(int i =0; i<3; i++)
            {
                if(XOboard[row,i] != States.empty)
                {
                    return false;
                }
            }
            return true;
        }
        private bool isColEmpty(int col)
        {
            for (int i = 0; i < 3; i++)
            {
                if (XOboard[i, col] != States.empty)
                {
                    return false;
                }
            }
            return true;
        }
    }
}