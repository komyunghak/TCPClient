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
    public partial class Calculator2 : Form
    {
        public Calculator2()
        {
            InitializeComponent();
        }

        /*어플리케이션이 실행이되면 가장 먼저 운영체제가 Main Fuction을 찾아서 코드를 실행을 시킨다.
         여기서 이벤트가 실행이되면 Function을 실행시키는데 이 Function이 이벤트 핸들러이다.
         */
        private void HelloLabel_Click(object sender, EventArgs e)
        {
            int number1 = 1;
            int number2 = 2;

            int sum = number1 + number2;
            HelloLabel.Text = sum.ToString();
            //정수를 문자형으로 타입변환할떄 사용하는 메서드로는 .ToString();  or Convert.Toint32();
        }

        private void SumNumbers_Click(object sender, EventArgs e)
        {
            int number1 = 0;
            int number2 = 0;
            if (String.IsNullOrWhiteSpace(Sum1.Text))
            //문자열은 꼭 ""을 표기 안에가 비었다면 아무것도없을시 아래 코드가 실행.
            /*String.IsNullOrWhiteSpace란 공백은 공백이지만 스페이스바나 탭키같은 공백은 false로 나오기떄문에
            나온 편리한 메서드로 탭키와 스페이스바도 공백으로 포함시켜준다.*/
            {
                MessageBox.Show("Sum1에 숫자를 입력해주세요.");
                Sum1.Focus();
                return;
                //현재는 void라 리턴에 값이 없어도되지만 void가아니라 int인 경우에는 값을넣어줘야한다.
                //return 0;
            }


            if (int.TryParse(Sum1.Text, out number1) == false)
            {
                MessageBox.Show("Sum1에 문자가 들어왔습니다. 숫자를 입력해주세요. ");
                Sum1.SelectAll();
                Sum1.Focus();
                return;
            }

            if (String.IsNullOrWhiteSpace(Sum2.Text))
            {
                MessageBox.Show("Sum2에 숫자를 입력해주세요.");
                Sum2.Focus();
                return;
            }

            if (int.TryParse(Sum2.Text, out number2) == false)
            {
                MessageBox.Show("Sum2에 문자가 들어왔습니다. 숫자를 입력해주세요. ");
                Sum2.SelectAll();
                Sum2.Focus();
                return;
            }

            int sum = Add(number1, number2);
            SumResult.Text = sum.ToString();
        }

        public int Add(int number1, int number2)
        {
            int sum = number1 + number2;
            return sum;
        }

        private void Calculator2_Load(object sender, EventArgs e)
        {

        }
        /* 밥을 하는 기능으로 코딩을 알아보자 
밥통을 사서 물로 쌀을 씻고 쌀과 물을 넣어서  취사를 눌르면 밥이된다. 이것이 Function이다.
쌀과 물이 Input이 되고, 이렇게 Function에 넘겨주는 값을 매개변수/파라미터 라고 한다.
취사버튼을 누르는 행위가 Function을 호출하는것이고 밥이 Output(결과)이다
*/
    }
}
