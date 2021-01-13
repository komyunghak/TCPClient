using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelloCSharpWin
{
    public enum Operaotors { Add, Sub, Multi, Div }


    // enum은 정해진 값에 편리하게 사용하게끔 사용할 수 있는 데이터타입 ex) 계절, 달, 요일 등등
    public partial class Calculator : Form
    {
        public int Result = 0;
        public bool isNewNum = true;
        public Operaotors Opt = Operaotors.Add;


        public Calculator()
        {
            InitializeComponent();
        }
        private void NumButton1_Click(object sender, EventArgs e)
        {
            Button numButton = (Button)sender;
            SetNum(numButton.Text);
        }

        public void SetNum(string num)
        {
            if (isNewNum)
            {
                NumScreen.Text = num;
                isNewNum = false;
            }
            else if (NumScreen.Text == "0")
            {
                NumScreen.Text = num;
            }
            else
            {
                NumScreen.Text = NumScreen.Text + num;
            }
        }
        private void NumPlus_Click(object sender, EventArgs e)
        {
            if(isNewNum == false)
            {
                int num = int.Parse(NumScreen.Text);
                if (Opt == Operaotors.Add)
                    Result = Result + num;
                else if (Opt == Operaotors.Sub)
                    Result = Result - num;

                NumScreen.Text = Result.ToString();
                isNewNum = true;
            }

            Button optButton = (Button)sender;
            if (optButton.Text == "+")
                Opt = Operaotors.Add;
            else if (optButton.Text == "-")
                Opt = Operaotors.Sub;
        }
        private void NumClear_Click(object sender, EventArgs e)
        {
            Result = 0;
            isNewNum = true;
            Opt = Operaotors.Add;

            NumScreen.Text = "0";
        }
        /*
         * 변수 = 0
         * 연산자 = +
         * 
         * 숫자 입력
         * 연산자 버튼 - 숫자 완성, 변수와 숫자를 저장된 연산자로 연산, 결과를 변수에 저장, 현재 연산자를 저장
         * 숫자 입력
         * 연산자 버튼 - 숫자 완성, 변수와 숫자를 저장된 연산자로 연산, 결과를 변수에 저장, 현재 연산자를 저장
         */

        private void Calculator_Load(object sender, EventArgs e)
        {

        }

    }
}
