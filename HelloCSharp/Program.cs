using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using(키워드) System(네임스페이스) = 보따리라고 이해

namespace HelloCSharp
    //사실상 이게 using System의 보따리 (라이브러리라고 이해)
{
    class Program
        //툴   윈도우 OS가 이 클래스 프로그램을 실행시켜줌.(툴 안에는 많은 기능이 있다.)
    {
        static void Main(string[] args)
        //항상 가장 먼저 실행되는 메서드(툴의 기능)
        //함수의 프로토타입    string[] args = 인자  기계가 일을하기위해 재료를 뜻함.
        {
            Console.WriteLine("Hello C#");
            // 콘솔창에 Hello C# 표기
            Console.WriteLine("Hello " + args[0]);
            //현재 args에는 변수가 없기에 에러가나지만 cmd창에  변수를 넣어서 실행을 해주면 변수가 나온다.
            Console.ReadKey();
            //ReadKey에는 값이 없기에 타이핑을 쳐주면 콘솔창이 종료된다.
            
        }


    }
}
