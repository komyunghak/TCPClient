using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;   //아래 3개 역시 마찬가지.
using System.Net.Sockets;
using System.Threading;

namespace Client
{
    public partial class Form1 : Form
    {
        Socket client_socket;
        bool isConnected;
        byte[] bytes = new byte[1024];
        string data;

        public Form1()
        {
            InitializeComponent();
            isConnected = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {  //send 부분입니다. 보내기 버튼을 눌렀을 때 글자 데이터를 보내는 역할입니다. 
           //연결이 안되어있을때는 그냥 return으로 보내졌을 때는 Clear함수와 함께 채팅창을 다시 원위치시키는것으로 마무리.
            if (isConnected == false)
                return;
            byte[] msg = Encoding.UTF8.GetBytes(textBox1.Text + "<eof>");
            int bytesSent = client_socket.Send(msg);
            textBox1.Clear();
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {  //접속 버튼을 눌렀을 때 발생하는 것입니다.
           //여기서 말하는 textBox2는 IP주소가 입력되는 TextBox이니 박스의 이름에 주의하세요.
            if (isConnected == true)
                return;
            client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client_socket.Connect(new IPEndPoint(IPAddress.Parse(textBox2.Text), 8000));
            listBox1.Items.Add(String.Format("소켓 연결이 되었습니다 {0}", client_socket.RemoteEndPoint.ToString()));
            isConnected = true;
            //아래 두개는 do_receive 함수를 위한 쓰레드입니다.
            //쓰레드가 있어야만 연결된다고 해야할까요.
            Thread listen_thread = new Thread(do_receive);
            listen_thread.Start();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        void do_receive()
        {
            while (isConnected)
            {
                while (true)
                {
                    byte[] bytes = new byte[1024];
                    int bytesRec = client_socket.Receive(bytes);
                    data += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<eof>") > -1)
                        break;
                }
                data = data.Substring(0, data.Length - 5);
                Invoke((MethodInvoker)delegate
                {
                    listBox1.Items.Add(data);
                }
                );
                data = "";
            }
        }
    }
}