using MySql.Data.MySqlClient;
//Mysql 라이브러리
using SimpleTcp;
//SimpleTcp 라이브러리
using System;
using System.Text;
using System.Windows.Forms;

namespace TCPServer
{
    public partial class Form1 : Form
    {

        MySqlConnection connection =
        new MySqlConnection("Server=localhost;Database=member1;Uid=root;Pwd=2002;");
        //DB연동


        public Form1()
        {
            InitializeComponent();
        }

        SimpleTcpServer server;
        //SimpleTcp 라이브러리 사용.

        private void btnStart_Click(object sender, EventArgs e)
        {
            server.Start();
            //서버를 시작한다
            txtInfo.Text += $"Startring...{Environment.NewLine}";
            //서버시작시 txtInfo에 나오는 문구
            btnStart.Enabled = false;
            //Start버튼이 상호작용에 false값을준다.
            btnSend.Enabled = true;
            //Send버튼이 상호작용에 true값을준다.
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnSend.Enabled = false;
            server = new SimpleTcpServer(txtIP. Text);
            //서버생성
            server.Events.ClientConnected += Events_ClientConnected;
            //클라이언트가 연결될 떄 호출하는 이벤트
            server.Events.ClientDisconnected += Events_ClientDisconnected;
            //클라이언트가 끊어졌을떄 호출하는 이벤트
            server.Events.DataReceived += Events_DataReceived;
            //클라이언트에서 바이트 데이터를 사용할 수 있게 되었을 때 호출하는 이벤트
        }

        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            //대리자역할
            {
                txtInfo.Text += $"{e.IpPort}: {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
                //바이트 데이터를 Encoding하여 한글로표기
            });
        }

        private void Events_ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"{e.IpPort} disconnected.{Environment.NewLine}";
                //클라이언트 연결이 끊긴다면
                lstClientIP.Items.Remove(e.IpPort);
                //지정한 개체를 제거한다.
            });
        }

        private void Events_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"{e.IpPort} connected.{Environment.NewLine}";
                //클라이언트가 연결
                lstClientIP.Items.Add(e.IpPort);
                //지정한 개체를 추가한다.
            });

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string insertQuery = "INSERT INTO member_tb2(IP,Message) VALUES('" + txtIP.Text + "'," + txtMessage.Text + ")";
            //member_tb2테이블에 IP,Message값을 INSERT한다.

            connection.Open();
            MySqlCommand command = new MySqlCommand(insertQuery, connection);

            try
            //예외 처리
            {
                if (command.ExecuteNonQuery() == 1)
                //Mysql에 정상적으로 들어갔다면 메세지를 보여줘라
                {
                    MessageBox.Show("정상적으로 갔다");
                }
                else
                {
                    MessageBox.Show("비정상 이당");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            connection.Close();


            if (server.IsListening)
            //서버연결수신
            {
                if (!string.IsNullOrEmpty(txtMessage.Text) && lstClientIP.SelectedItem != null)
                //메시지 확인 및 목록 상자에서 클라이언트 IP 선택
                {
                    server.Send(lstClientIP.SelectedItem.ToString(), txtMessage.Text);
                    txtInfo.Text += $"Server: {txtMessage.Text}{Environment.NewLine}";
                    txtMessage.Text = string.Empty;
                    string hexString = "8E2"; int num = Int32.Parse(hexString, System.Globalization.NumberStyles.HexNumber); Console.WriteLine(num);
                    //supersimpletcp 라이브러리 참조
                }
            }
        }

        private void lstClientIP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
