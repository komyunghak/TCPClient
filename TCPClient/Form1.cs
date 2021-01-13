using SimpleTcp;
using System;
using System.Text;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace TCPClient
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

        SimpleTcpClient client;

        private void btnSend_Click(object sender, EventArgs e)
        {
            string insertQuery = "INSERT INTO member_tb(IP,Message) VALUES('" + txtIP.Text + "'," + txtMessage.Text + ")";
            //member_tb2테이블에 IP,Message값을 INSERT한다.

            connection.Open();
            MySqlCommand command = new MySqlCommand(insertQuery, connection);

            try//예외 처리
            {
                // 만약에 내가처리한 Mysql에 정상적으로 들어갔다면 메세지를 보여주라는 뜻이다
                if (command.ExecuteNonQuery() == 1)
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
            
            if (client.IsConnected)
            {
                if (!string.IsNullOrEmpty(txtMessage.Text))
                {
                    client.Send(txtMessage.Text);
                    txtInfo.Text += $"Me: {txtMessage.Text}{Environment.NewLine}";
                    txtMessage.Text = string.Empty;
                    string hexString = "8E2"; int num = Int32.Parse(hexString, System.Globalization.NumberStyles.HexNumber); Console.WriteLine(num);
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client.Connect();
                //클라이언트가 연결
                btnSend.Enabled = true;
                btnConnect.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client = new(txtIP.Text);
            //클라이언트 생성
            client.Events.Connected += Events_Connected;
            //연결이 설정되면 호출할 이벤트
            client.Events.DataReceived += Events_DataReceived;
            //서버에서 바이트 데이터를 사용할 수 있게 되었을 때 호출할 이벤트.
            client.Events.Disconnected += Events_Disconnected;
            //연결이 끊어지면 호출할 이벤트
            btnSend.Enabled = false;
        }

        private void Events_Disconnected(object sender, ClientDisconnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"Server disconnected.{Environment.NewLine}";
                //클라이언트 연결이 끊긴다면
            });
        }

        private void Events_DataReceived(object sender, DataReceivedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"Server: {Encoding.UTF8.GetString(e.Data)}{Environment.NewLine}";
                //바이트 데이터를 Encoding하여 한글로표기
            });
        }

        public byte[] ConvertByteArray(String strHex)
        {
            int iLength = strHex.Length;
            byte[] bytes = new byte[iLength / 2];

            for (int i = 0; i < iLength; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(strHex.Substring(i, 2), 16);
            }
            return bytes;
        }

        private void Events_Connected(object sender, ClientConnectedEventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                txtInfo.Text += $"Server connected.{Environment.NewLine}";
                //클라이언트가 연결
            });
        }
    }
}
