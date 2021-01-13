using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;       ////이 이하 3가지는 네트워크 소켓 쓰레딩을 하기 위해서 선언해줍니다.
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public partial class Form1 : Form
    {

        Socket listen_socket;
        Socket client_socket;   //소켓 선언
        bool isConnected;  //bool로 서버와 클라이언트가 연결되었는지 true /false로 판단

        byte[] bytes = new byte[1024];
        string data;  //채팅서버이기 때문에 글자 데이터를 읽고 불러오는데에 쓰이는 선언.
        public Form1()
        {
            InitializeComponent();
            isConnected = false;   //초기화부문
        }

        private void button1_Click(object sender, EventArgs e)
        {
            start("127.0.0.1", 8000, 10);   //버튼1 을 눌렀을 때 실행될 것 127.0.0.1 은 Local입니다
                                            //여러분도 위와 같은 것으로 해보세요 연습할때는 ㅎㅎ
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void start(string host, int port, int backlog)
        {
            this.listen_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress address;
            if (host == "0.0.0.0")
            {
                address = IPAddress.Any;
            }
            else
            {
                address = IPAddress.Parse(host);
            }
            IPEndPoint endpoint = new IPEndPoint(address, port);

            try
            {
                listen_socket.Bind(endpoint);
                listen_socket.Listen(backlog);

                client_socket = listen_socket.Accept();
                //클라이언트가 접속하지 않으면 서버는 이곳에서 디버깅이 멈춰져 있습니다. 클라이언트가 접속할 시에
                //아래 문단부터 다시 돌아가게 되지요 ㅎㅎ
                listBox1.Items.Add("연결을 시작합니다~~");
                isConnected = true;
                Thread listen_thread = new Thread(do_receive); //이 줄과 밑에 줄은 쓰레드입니다. 즉
                                                               //글자를 받는 것이라고 보시면 되겠습니다. 클라이언트로부터.
                listen_thread.Start();
            }
            catch (Exception e)
            { }
            //c++하셨다면 try / catch 구문이야 잘 아시겠죠 전 잘모름.
        }
        void do_receive()//do receive 함수입니다. 이 함수가 채팅서버의 결정적인 함수입니다.
        {
            while (isConnected)
            {
                while (true)
                {
                    byte[] bytes = new byte[1024];  //바이트 배열 선언
                    int bytesRec = client_socket.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);  //인코딩과 GetString
                    if (data.IndexOf("<eof>") > -1)  //Index Of  의 그것입니다 숫자 0 아닙니다.
                        break;
                }
                data = data.Substring(0, data.Length - 5);
                Invoke((MethodInvoker)delegate
                {
                    listBox1.Items.Add(data);  //이 부문은 listBox로 우리가 위에서 만든UI에 클라이언트로부터
                                               //받아온 글자 데이터를 뿌려주는 역할을 합니다.
                }
                );
                data = "";  //한 번 채팅을 치고 보내기를 누르면 그 글자는 사라져야겠죠? 그 부분입니다.
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isConnected == false)
                return;
            byte[] msg = Encoding.UTF8.GetBytes(textBox1.Text + "<eof>");
            int bytesSent = client_socket.Send(msg);
            textBox1.Clear();
            textBox1.Text = "";
        }  //서버로서의 역할만 한다면 사실 필요 없는 부분입니다만 클라이언트의 역할도 하기 때문에 '보내기' 버튼으 ㅣ역할입니다.

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}